using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.Services;
using WoltDataAccess.DAL;
using WoltDataAccess.Repositories.Implementations;
using WoltDataAccess.Repositories.Interfaces;
using WoltEntity.Entities;

namespace WoltApp
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
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]);
            });
            services.AddScoped<LayoutServices>();
            services.AddHttpContextAccessor();
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(IdentityOptions =>
            {
                IdentityOptions.Password.RequiredLength = 8;
                IdentityOptions.Password.RequireNonAlphanumeric = true;
                IdentityOptions.Password.RequireLowercase = true;
                IdentityOptions.Password.RequireLowercase = true;
                IdentityOptions.Password.RequireDigit = true;

                IdentityOptions.User.RequireUniqueEmail = true;

                IdentityOptions.Lockout.MaxFailedAccessAttempts = 3;
                IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                IdentityOptions.Lockout.AllowedForNewUsers = true;

                IdentityOptions.SignIn.RequireConfirmedEmail = true;
            });
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
