using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Social.Areas.Identity.Data;
using Social.Data;

[assembly: HostingStartup(typeof(Social.Areas.Identity.IdentityHostingStartup))]
namespace Social.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SocialIDContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SocialIDContext")));

                services.AddDefaultIdentity<SocialUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SocialIDContext>();
            });
        }
    }
}