using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social.Areas.Identity.Data;

namespace Social.Data
{
    public class SocialIDContext : IdentityDbContext<SocialUser>
    {
        
        public DbSet<SocialUser> SocialUsers { get; set; }
        public DbSet<LogIn> LogIns { get; set; }

        public SocialIDContext(DbContextOptions<SocialIDContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
