using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers.Helpers;
using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Models.Mappers;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly AuthorizationHelper authorizationHelper;
        private readonly UserMapper mapper;

        // A new instance of the controller is created for every request
        public UserController(IUsersService usersService, AuthorizationHelper authorizationHelper, UserMapper mapper)
        {
            this.usersService = usersService;
            this.authorizationHelper = authorizationHelper;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetInfo([FromQuery] UserQueryParameters userParams)
        {
            var result = this.usersService.Get(userParams).Select(u => new UserResponseDto(u));
            return this.StatusCode(StatusCodes.Status200OK, result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = this.usersService.GetById(id);

                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Create([FromHeader] string authorization, [FromBody] UserRequestDto dto)
        {
            try
            {
                this.authorizationHelper.TryGetUser(authorization);
                if (usersService.HasUser(dto.UserName))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Username already in use");
                }
                var user = this.mapper.ConvertToModel(dto);
                var createdUser = this.usersService.Create(user);
                return this.StatusCode(StatusCodes.Status201Created, createdUser);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromHeader] string authorization, [FromBody] UserRequestDto dto)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(authorization);
                var userToUpdate = this.mapper.ConvertToModel(dto);
                userToUpdate.Id = id;
                var updatedUser = this.usersService.Update(id, userToUpdate, user);
                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (ArgumentException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.usersService.Delete(id);
                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

       
    }
}
