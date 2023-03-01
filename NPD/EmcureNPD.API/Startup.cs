using EmcureNPD.API.Filter;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.API.Middlewares;
using EmcureNPD.Business.Core.Resolver;
using EmcureNPD.Resource;
using EmcureNPD.Utility;
using EmcureNPD.Utility.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace EmcureNPD.API
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
            DatabaseConnection.NPDDatabaseConnection = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });
            services.AddResource();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Configuration["Swagger:title"], Version = Configuration["Swagger:version"] });
                c.AddSecurityDefinition(Configuration["Swagger:Bearer"], new OpenApiSecurityScheme()
                {
                    Description = Configuration["Swagger:description"],
                    Name = Configuration["Swagger:name"],
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                     },
                      new string[] { }
                   }
                });
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc(Configuration["Swagger:version"], new OpenApiInfo
            //    {
            //        Title = Configuration["Swagger:title"],
            //        Version = Configuration["Swagger:version"],
            //    });
            //});

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IResponseHandler<>), typeof(ResponseHandler<>));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //--<set upload large files> ---
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
            //--</set upload large files> ---

            //File Upload and Download
            services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads")));

            services.ContainerResolver();

            services.AddCors();
            services.AddDistributedMemoryCache();
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(Configuration["Swagger:swaggerurl"], Configuration["Swagger:swaggertitle"]);
                });
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmcureNPD.API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            var allowedOrigins = Configuration.GetSection("AllowedOrigins").Value.Split(",");

            app.UseCors(builder => builder
                                    .WithOrigins(allowedOrigins)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
            );

            app.UseHttpsRedirection();

            app.UseRequestLocalization();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            //.UseMiddleware<ExceptionMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}