using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrinitasHunde.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using TrinitasHunde.Services;

namespace TrinitasHunde
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            // Add policies for roles
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperUser",
                    policy => policy.RequireRole("SuperUser"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireRole("Admin", "SuperUser"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Standard",
                    policy => policy.RequireRole("Standard", "Admin", "SuperUser"));
            });

            services.AddTransient<IEmailSender, EmailSender>(i => new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]
                )
            );

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Admin", "Admin");
                })
                .AddRazorPagesOptions(options =>
                 {
                     options.Conventions.AuthorizeFolder("/Standard", "Standard");
                 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            DataSeeder.Seed(userManager, roleManager, Configuration);

            app.UseMvc();
        }
    }
}
