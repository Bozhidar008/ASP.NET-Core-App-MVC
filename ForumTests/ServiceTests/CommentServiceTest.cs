using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Services;
using WEbApplication1Tests.Models;

namespace WEbApplication1Tests.ServicesTests
{
    [TestClass]
    public class CommentServiceTest : BaseTest
    {
        
        private DbContextOptions<ApplicationContext> options;
        List<User> users;
        List<Comment> comments;
        List<Post> posts;
        List<Like> likes;

        [TestInitialize]
        public void Initialize()
        {
            this.options = GetOptions();
            var userModels = new UserTestModels();
            var postModels = new PostTestModels();
            var commentModels = new CommentTestModels();
            var likesModels = new LikeTestModels();
            using (var arrangeContext = new ApplicationContext(this.options))
            {
                
                users = userModels.GetUsers();
                posts = postModels.GetPosts(comments, likes);
                comments = commentModels.GetComments(posts);
                likes = likesModels.GetLikes(users, posts);

                arrangeContext.Users.AddRange(users);
                arrangeContext.Likes.AddRange(likes); 
                arrangeContext.Posts.AddRange(posts);
                arrangeContext.Comments.AddRange(comments);
                arrangeContext.SaveChanges();
            }
        }
        
        public CommentDTO GetCommentDTO()
        {
            return new CommentDTO
            {
                Author = //???
                Content = String.Empty,

            };
        }
        */
        
        [TestMethod]
        public void Return_Correct_When_ParamsAreValid()
        {
            //Arrange
            var expectedDto = GetCommentDTO();
            //Act&Assert

            using(var assertContext = new ApplicationContext(this.options))
            {
                //need to use serviceRepository??
                var sut = new CommentsService(assertContext);
                var actualDto = sut.GetCommentById(1);

                Assert.IsNotNull(actualDto);
                //Assert.AreEqual(expectedDto, actualDto);
                Assert.AreEqual(expectedDto.PostId, actualDto.PostId);

            }
        }
        
    }
}
