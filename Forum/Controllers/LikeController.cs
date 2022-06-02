using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Controllers.Helpers;
using WebApplication1.Dtos;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Like")]
    public class LikeController : Controller
    {
        private readonly ILikesService likeService;
        private readonly AuthorizationHelper authorizationHelper;

        public LikeController(ILikesService likeService, AuthorizationHelper authorizationHelper)
        {
            this.likeService = likeService;
            this.authorizationHelper = authorizationHelper;
        }

        //no dtos needed???
        [HttpGet("")]
        public IActionResult GetAllLikes()
        {
            List<Like> result = this.likeService.GetAllLikes().Select(l => new Like()).ToList();// is it correct to use new Like ??
            return this.StatusCode(StatusCodes.Status200OK, result);
        }
        // do i need get by ID???
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {                
                var like = this.likeService.GetLikeById(id);
                return this.StatusCode(StatusCodes.Status200OK, like);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Create([FromHeader] string username, [FromBody] Like like)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(username);
                like.UserId = user.Id;
                var cLike = this.likeService.Create(like, user);
                //var createdLike = cLike;
                return this.StatusCode(StatusCodes.Status200OK, cLike);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromHeader] string username, [FromBody] Like like)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(username);
                like.Id = id;
                var cLike = this.likeService.Update(id, like, user);
                return this.StatusCode(StatusCodes.Status200OK, cLike);
            }
            catch(UnAuthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string username)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(username);
                this.likeService.Delete(id, user);
                return this.StatusCode(StatusCodes.Status200OK);//need to return deleted entity???
            }
            catch (UnAuthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
    }
}
