using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Controllers.Helpers;
using WebApplication1.Dtos;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Models.Mappers;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Comment")]
    public class CommentController : Controller
    {
        private readonly ICommentsService commentService;
        private readonly CommentMapper mapper;
        private readonly AuthorizationHelper authorizationHelper;

        public CommentController(ICommentsService commentService, CommentMapper mapper, AuthorizationHelper authorizationHelper)
        {
            this.commentService = commentService;
            this.mapper = mapper;
            this.authorizationHelper = authorizationHelper;
        }

        [HttpGet("/CommentsByUserId")]
        public IActionResult GetAllComments(int userId)
        {            
            List<CommentInfoDto> result = this.commentService.GetAllCommentsForPost(userId).Select(c => new CommentInfoDto()).ToList();

            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("/CommentsByPostId")]
        public IActionResult GetPostComments(int postId) 
        {
            IEnumerable<CommentDto> comments = commentService.GetPostComments(postId);
            return StatusCode(StatusCodes.Status200OK, comments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var comment = this.commentService.GetCommentById(id);
                return this.StatusCode(StatusCodes.Status200OK, comment);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]// new ("") ??
        public IActionResult Create([FromHeader] string username ,[FromBody]CommentRequestDto dto) 
        {
            try
            {                
                var user = this.authorizationHelper.TryGetUser(username);  
                var comment = this.mapper.ConvertToModel(dto);
                comment.UserId = user.Id;
                var cComment = this.commentService.Create(comment, user);
                var createdComment = new CommentResponseDto(cComment);
                return this.StatusCode(StatusCodes.Status201Created, createdComment);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromHeader] string username, [FromBody] CommentRequestDto dto)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(username);
                var commentToUpdate = this.mapper.ConvertToModel(dto);
                commentToUpdate.Id = id;
                var cComment = this.commentService.UpdateComment(id,commentToUpdate, user);
                var updatedComment = new CommentResponseDto(cComment);
                return this.StatusCode(StatusCodes.Status200OK, updatedComment);
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
                this.commentService.DeleteComment(id, user);
                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (UnAuthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch(EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }            
        }
    }
}
