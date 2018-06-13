using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskME.Core.Interfaces;
using AskME.Core.RepositoryInterface;
using AskME.Core.ServiceInterface;
using AskME.Core.Services;
using AskME.Infrastructure.Common;
using AskME.Infrastructure.Data;
using AskME.Infrastructure.FileService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AskME.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {
            // MsSqlServer
            services.AddDbContext<AskMEDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MsSqlServerDevelopment")));
            // IOC
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // UserInfo
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IUserInfoService, UserInfoService>();

            // Tag or Topic
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();

            // Question 
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionService, QuestionService>();

            // Blog
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();

            // Outher
            services.AddSingleton<IUUID, UUID>();
            services.AddSingleton<IUploadImg, UploadImg>();

            // Cookie Auth
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/user/login");
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/user/login");
                options.Cookie.Name = "AuthCookie";
            });
            // MVC
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            // Cookie Auth
            app.UseAuthentication();
            // MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}
