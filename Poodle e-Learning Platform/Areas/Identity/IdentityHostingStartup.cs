using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poodle_e_Learning_Platform.Data;

[assembly: HostingStartup(typeof(Poodle_e_Learning_Platform.Areas.Identity.IdentityHostingStartup))]

namespace Poodle_e_Learning_Platform.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            /*
            builder.ConfigureServices((context, services) => {
            services.AddDbContext<ApplicationDbContext>(
                );

                 
            });
            */

        }
    }
}

