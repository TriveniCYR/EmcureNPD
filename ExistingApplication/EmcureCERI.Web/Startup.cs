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
using EmcureCERI.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using EmcureCERI.Web.Models;
using EmcureCERI.Business.Core;
using EmcureCERI.Web.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using EmcureCERI.Web.Hubs;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Http.Features;
using EmcureCERI.Data.DataAccess;

namespace EmcureCERI.Web
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
            clsEFDBConnection.MyEFDBConnection = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;

            //services.AddDbContext<EmcureCERIDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));
            //this is set for find webapp base URL
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(240);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });            

            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseSqlServer(clsEFDBConnection.MyEFDBConnection));           

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //--<set upload large files> ---
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
            //--</set upload large files> ---



            //Password Strength Setting
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(240);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

            });

            //Seting the Account Login page
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(240);
                options.LoginPath = "/Dashboard/index"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });


            //File Upload and Download
            services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/document")));


            // Add application services.
            services.ContainerResolver();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddCors();
            services.AddMvc();
            //services.AddMvc().AddRazorRuntimeCompilation();
            services.AddDistributedMemoryCache();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddSession();
            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin());
            });


            

            //services.AddSession(options =>
            //{
            //    // Set a short timeout for easy testing.
            //    options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.HttpOnly = true;
            //});



            // For Add Localizer

            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                    return factory.Create("SharedResource", assemblyName.Name);
                };
            });
            
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(
            opts =>
            {
                var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("nl-BE"),
                new CultureInfo("en-GB"),
                new CultureInfo("de-DE"),
                new CultureInfo("es-ES"),
                new CultureInfo("fr-FR")
            };

                opts.DefaultRequestCulture = new RequestCulture("en-GB");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
            });
        }

        


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
        
            //app.UseHttpsRedirection();
            app.UseGanttErrorMiddleware();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            var allowedOrigins = Configuration.GetSection("AllowedOrigins").Value.Split(",");

            app.UseCors(builder => builder
                                    .WithOrigins(allowedOrigins)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
            );

            app.UseAuthentication();

            // Add for Localizer
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseSession();
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/NotificationHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.Use(async (context, next) =>
            {
                string CurrentUserIDSession = context.Session.GetString("CurrentUserID");
                if (!context.Request.Path.Value.Contains("/Account/Login"))
                {
                    if (string.IsNullOrEmpty(CurrentUserIDSession))
                    {
                        var path = $"/Account/Login?ReturnUrl={context.Request.Path}";
                        context.Response.Redirect(path);
                        return;
                    }

                }
                await next();
            });

            //  CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            IdentityResult roleResult;
            //Adding Addmin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Sales");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Sales"));
            }
            //Asign Admin role to the main User here i have given my login id for Admin management
            ApplicationUser user = await UserManager.FindByEmailAsync("admin@emcure.com");

            var User = new ApplicationUser();
            await UserManager.AddToRoleAsync(user, "Admin");

        }
    }
}
