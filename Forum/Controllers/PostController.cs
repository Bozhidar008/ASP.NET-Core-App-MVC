using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApplication1.Services;
using WebApplication1.Dtos;
using WebApplication1.Models.Mappers;
using WebApplication1.Controllers.Helpers;
using WebApplication1.Exceptions;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {

        private readonly IPostsService postsService;
        private readonly PostMapper mapper;
        private readonly AuthorizationHelper authorizationHelper;
        public PostController(IPostsService postsService, AuthorizationHelper authorizationHelper, PostMapper postMapper)
        {
            this.postsService = postsService;
            this.mapper = postMapper;
            this.authorizationHelper = authorizationHelper;
        }

        [HttpGet("")]
        public IActionResult GetInfo()
        {
            List<PostResponseDto> result = this.postsService
                .Get().Select(b => new PostResponseDto(b)).ToList(); 

            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("")]
        public IActionResult Create([FromHeader] string authorization, [FromBody] PostRequestDto dto)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(authorization);
                var post = this.mapper.ConvertToModel(dto);
                post.UserId = user.Id;
                var cPost = this.postsService.Create(post, user);
                var createdPost = new PostResponseDto(cPost);
                return this.StatusCode(StatusCodes.Status201Created, createdPost);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }

        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, [FromHeader] string authorization, [FromBody] PostRequestDto dto)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(authorization);
                var postToUpdate = this.mapper.ConvertToModel(dto);
                postToUpdate.Id = id;
                var uPost = this.postsService.Update(id, postToUpdate, user);
                var updatedPost = new PostResponseDto(uPost);
                return this.StatusCode(StatusCodes.Status200OK, updatedPost);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string authorization)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(authorization);
                this.postsService.Delete(id, user);
                return this.StatusCode(StatusCodes.Status200OK);
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
