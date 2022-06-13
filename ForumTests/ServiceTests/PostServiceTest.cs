using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WEbApplication1Tests.Models;

namespace WEbApplication1Tests.ServicesTests
{
    public class PostServiceTest : BaseTest
    {
        private DbContextOptions<ApplicationContext> options;
        List<User> users;
        List<Comment> comments;
        List<Like> likes;
        List<Post> posts;

        [TestInitialize]
        public void Initialize()
        {
            this.options = GetOptions();
            var userModels = new UserTestModels();
            var likeModels = new LikeTestModels();
            var postModels = new PostTestModels();
            var commentModels = new CommentTestModels();

            using(var arrangeContext = new ApplicationContext(this.options))
            {
                users = userModels.GetUsers();
                likes = likeModels.GetLikes(users, posts);
                comments = commentModels.GetComments(posts);
                posts = postModels.GetPosts(comments, likes);

                arrangeContext.Users.AddRange(users);
                arrangeContext.Likes.AddRange(likes);
                arrangeContext.Posts.AddRange(posts);
                arrangeContext.Comments.AddRange(comments);
                arrangeContext.SaveChanges();
            }
        }

    }
}
