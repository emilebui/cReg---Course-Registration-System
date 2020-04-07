using AngleSharp;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace cRegis.Tests.IntegrationTest.Infrastructure
{
    public class FakeStartup
    {
        public FakeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContextTest>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                        options =>
                        {
                            options.AccessDeniedPath = "/Auth/AccessDenied";
                            options.LoginPath = "/Auth/Index";
                        });

            services.AddAuthorization();
            services.AddControllersWithViews();

            //injecting custom Core services
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEnrollService, EnrollService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IWishlistService, WishlistService>();

            //injecting custom Web services
            services.AddScoped<IViewModelService, ViewModelService>();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                    pattern: "{controller=Auth}/{action=index}/{id?}");
            });
        }
    }
}
