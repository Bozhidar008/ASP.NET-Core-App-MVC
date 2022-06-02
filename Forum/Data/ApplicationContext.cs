using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        protected void Seed(ModelBuilder modelBuilder)
        {

            var users = new List<User>();
            users.Add(new User
            {
                Id = 1,
                FirstName = "Georgi",
                LastName = "Georgiev",
                UserName = "Gosho",
                Email = "georgi.georgiev@mail.com",
                IsAdmin = true,
                IsBlocked = false

            });

            users.Add(new User
            {
                Id = 2,
                FirstName = "Petar",
                LastName = "Petrov",
                UserName = "pesho",
                Email = "petar.petrov@mail.com",
                IsAdmin = false,
                IsBlocked = false

            });
            modelBuilder.Entity<User>().HasData(users);
            //configure relations

            var posts = new List<Post>();
            posts.Add(new Post
            {
                Id = 1,
                Title = "FirstPost",
                Content = "SomeContent",
                UserId = 1


            });
            posts.Add(new Post
            {
                Id = 2,
                Title = "SecondPost",
                Content = "AgainSomeContent",
                UserId = 2


            });
            posts.Add(new Post
            {
                Id = 3,
                Title = "ThirdPost",
                Content = "SomeSomeContent",
                UserId = 1
            });
            modelBuilder.Entity<Post>().HasData(posts);

            var comments = new List<Comment>();
            comments.Add(new Comment
            {
                Id = 1,
                Content = "First comment",
                UserId = 1,
                PostId = 1


            });
            comments.Add(new Comment
            {
                Id = 2,
                Content = "Second comment",
                UserId = 2,
                PostId = 2

            });
            comments.Add(new Comment
            {
                Id = 3,
                Content = "Third comment",
                UserId = 1,
                PostId = 3
            });
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)             //should add UserId???   
                .OnDelete(DeleteBehavior.NoAction); //??? Cascade

            modelBuilder.Entity<Comment>()
                .HasOne(u => u.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>()
                .HasOne(p => p.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(p => p.PostId)     //should add UserId???  
                .OnDelete(DeleteBehavior.NoAction);//??? Cascade

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>().HasData(comments);

            var likes = new List<Like>();
            likes.Add(new Like
            {
                Id = 1,
                UserId = 1,
                PostId = 1
            });
            likes.Add(new Like
            {
                Id = 2,
                UserId = 2,
                PostId = 2
            });
            likes.Add(new Like
            {
                Id = 3,
                UserId = 1,
                PostId = 3
            });

            modelBuilder.Entity<Like>().HasData(likes);
        }
    }
}
