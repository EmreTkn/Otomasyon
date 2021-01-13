using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.data.Concrete.EfCore;
using NKUOtomayon.WebUI.EmailServices;
using NKUOtomayon.WebUI.Helpers;
using NKUOtomayon.WebUI.Identity;
using shopapp.webui.EmailServices;

namespace NKUOtomayon.WebUI
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=nkuDb"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));

            services.Configure<IdentityOptions>(options =>
            {
                // password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

                // Lockout                
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".NKUOtomasyon.Security.Cookie",
                    //security for cookies
                    SameSite = SameSiteMode.Strict
                };
            });

            services.AddScoped<IStudentRepository, EfCoreStudentRepository>();
            services.AddScoped<ILessonRepository, EfCoreLessonRepository>();
            services.AddScoped<ITeacherRepository, EfCoreTeacherRepository>();
            services.AddScoped<ISemesterRepository, EfCoreSemesterRepository>();
            services.AddScoped<IStudyProgramRepository, EfCoreStudyProgramRepository>();
            services.AddScoped<IGradesRepository, EfCoreGradesRepository>();
            services.AddScoped<IPhotoRepository, EfCorePhotoRepository>();
            services.AddScoped<IPdfFileRepository, EfCorePdfFileRepository>();





            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
                new SmtpEmailSender(
                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    _configuration["EmailSender:UserName"],
                    _configuration["EmailSender:Password"])
            );

            services.AddControllersWithViews();
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
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = "/modules"
            });
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "lessonlist",
                    pattern: "home/lesson/{id?}",
                    defaults: new { controller = "Home", action = "GetLessonsByStudent" }
                );
                endpoints.MapControllerRoute(
                    name: "adminuseredit",
                    pattern: "admin/user/{id?}",
                    defaults: new { controller = "Admin", action = "UserEdit" }
                );
                endpoints.MapControllerRoute(
                    name: "adminusers",
                    pattern: "admin/user/list",
                    defaults: new { controller = "Admin", action = "UserList" }
                );
                endpoints.MapControllerRoute(
                    name: "adminroleedit",
                    pattern: "admin/role/{id?}",
                    defaults: new { controller = "Admin", action = "RoleEdit" }
                );
                endpoints.MapControllerRoute(
                    name:"adminroles",
                    pattern:"admin/role/list",
                    defaults:new {controller="Admin",action="RoleList"}
                    );
                endpoints.MapControllerRoute(
                    name: "adminrolecreate",
                    pattern: "admin/role/create",
                    defaults: new { controller = "Admin", action = "CreateRole" }
                );

                endpoints.MapControllerRoute(
                        name: "resetpassword",
                        pattern: "account/forgotpassword",
                        defaults: new { controller = "Account", action = "ForgotPassword" }
                    );
                  
                    endpoints.MapControllerRoute(
                        name: "exapmle",
                        pattern: "account/example",
                        defaults: new { controller = "Account", action = "Example" }
                    );

                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                });

        }
    }
}
