using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApplication1.Controllers;
using WebApplication1.Controllers.Helpers;
using WebApplication1.Data;
using WebApplication1.Models.Mappers;
using WebApplication1.Repository;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddControllers();


            services.AddDbContext<ApplicationContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("BMConnection")));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<IPostsRepository, PostsRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentsService, CommentsService>();

            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ILikesService, LikesService>();

            services.AddTransient<UserMapper>();
            services.AddTransient<PostMapper>();
            services.AddTransient<CommentMapper>();

            services.AddTransient<AuthorizationHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
