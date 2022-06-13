using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WEbApplication1Tests.ServicesTests
{
    [TestClass]
    public class UserServiceTest : BaseTest
    {
        
        private DbContextOptions<ApplicationContext> options;
        List<User> users;

        [TestInitialize]
        public void Initialize()
        {
            this.options = GetOptions();
            using(var arrangeContext = new ApplicationContext(this.options))
            {
               users = new List<User>()
               {
                new User
                {
                  Id = 1,
                  FirstName = "Georgi",
                  LastName = "Georgiev",
                  UserName = "Gosho",
                  Email = "georgi.georgiev@mail.com",
                  IsAdmin = true,
                  IsBlocked = false
                },
                new User
                {
                  Id = 2,
                  FirstName = "Petar",
                  LastName = "Petrov",
                  UserName = "pesho",
                  Email = "petar.petrov@mail.com",
                  IsAdmin = false,
                  IsBlocked = false
                }
               };
                arrangeContext.Users.AddRange(users);
                arrangeContext.SaveChanges();
            }
            
        }

        // i need UserResponceDTO??
        private UserDTO GetUserDTO()
        {
            return new UserDTO()
            {
                Id = 2,
                FirstName = "Petar",
                LastName = "Petrov",
                UserName = "pesho",
                Email = "petar.petrov@mail.com",
                IsAdmin = false,
                IsBlocked = false

            };
        }

        [TestMethod]
        public void CreateCorrectUser_When_ParamsAreValid()
        {
            //Arrrange
            var expectedDto = GetUserDTO();

            //Act&Assert
            using (var assertContext = new ApplicationContext(this.options))
            {
                var likeService = new LikesService(assertContext);
                var postService = new PostsService(assertContext);
                var commentService = new CommentsService(assertContext);
                var sut = new UsersService(assertContext, likeService, postService, commentService);

                UserDTO d = sut.Create(expectedDto);

                var actualDto = sut.Get(2);//

                Assert.IsNotNull(actualDto);
                Assert.AreEqual(expectedDto.User.IsAdmin, actualDto.User.IsAdmin);
                // how to check IsAdmin IsBlocked??
                Assert.AreEqual(d.User.IsAdmin, expectedDto.User.IsAdmin);
            }

            public void UpdateCorrectUser_When_ParamsAreValid()
            {
                // Arrange        
                var expectedDto = GetUserDTO();

                // Act & Assert
                using (var assertContext = new ApplicationContext(this.options))
                {
                    var likeService = new LikesService(assertContext);
                    var postService = new PostsService(assertContext);
                    var commentService = new CommentsService(assertContext);
                    var sut = new UsersService(assertContext, likeService, postService, commentService);

                    var d = sut.Update(2, expectedDto);

                    var actualDto = sut.Get(2);

                    Assert.IsNotNull(actualDto);
                    Assert.AreEqual(expectedDto.User.isAdmin, actualDto.User.IsAdmin);
                    Assert.AreEqual(d.UserName, expectedDto.Username);

                }
            }
        }

        [TestMethod]
        public void DeleteCorrectUserPrivilege_When_ParamsAreValid()
        {

            // Act & Assert
            using (var assertContext = new ApplicationContext(this.options))
            {
                var likeService = new LikesService(assertContext);
                var postService = new PostsService(assertContext);
                var commentService = new CommentsService(assertContext);
                var sut = new UsersService(assertContext, likeService, postService, commentService);

                sut.Delete(2);


                Assert.ThrowsException<ArgumentException>(() => { sut.Get(2); });// need to use user[2]??

            }
        }

        [TestMethod]
        public void Create_Should_Throw_When_Username_Exists()
        {
            // Act & Assert
            using (var assertContext = new ApplicationContext(this.options))
            {
                var likeService = new LikesService(assertContext);
                var postService = new PostsService(assertContext);
                var commentService = new CommentsService(assertContext);
                var sut = new UsersService(assertContext, likeService, postService, commentService);
                Assert.ThrowsException<ArgumentException>(() => {
                    sut.Create(new UserDTO()
                    {
                        DisplayName = "display1231233",
                        Email = "email1121233@gmail.com",
                        Password = "Password7!",
                        Username = "username1",
                        IsAdmin = new UserDTO()
                    });
                });
            }
        }

        [TestMethod]
        public void GetByUsername_Should_Throw_If_No_User_Found()
        {
            // Act & Assert
            using (var assertContext = new ApplicationContext(this.options))
            {
                var likeService = new LikesService(assertContext);
                var postService = new PostsService(assertContext);
                var commentService = new CommentsService(assertContext);
                var sut = new UsersService(assertContext, likeService, postService, commentService);
                var user = sut.Get(1);
                //sut.SetProfilePicture(user.Username, "wwwroot/Images/picture.jpg");
                Assert.ThrowsException<ArgumentException>(() => { sut.GetByUsername("asdasddsa"); });
            }
        }

        [TestMethod]
        public void Authenticate_User_Should_Return_Correctly()
        {
            // Act & Assert
            using (var assertContext = new ApplicationContext(this.options))
            {
                var likeService = new LikesService(assertContext);
                var postService = new PostsService(assertContext);
                var commentService = new CommentsService(assertContext);
                var sut = new UsersService(assertContext, likeService, postService, commentService);
                sut.Create(new UserDTO
                {
                    Id = 3,
                    DisplayName = "displayName3",
                    Username = "username3",
                    Email = "email3@gmail.com",
                    Password = "testpassword"
                });
                Assert.AreEqual("displayName3", sut.AuthenticateUser("username3", "admin").Username);
            }
        }
        */
    }
}
