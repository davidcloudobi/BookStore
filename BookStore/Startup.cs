using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Library.Entities;
using Library.Model.DTO;
using Library.Model.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using User = Library.Model.Logic.User;


namespace BookStore
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
            services.AddMvc(setupAction =>
            {
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            });

            services.AddControllers();
            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
                //setupAction.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //setupAction.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            services.AddDbContext<BookStoreDbContext>(opt =>
                {
                    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });


            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", Config =>
                {
                    Config.Cookie.Name = "User.Cookie";
                    Config.LoginPath = "/api/v1.0/Admin/login";
                });
        


        services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("User", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "User API",
                    Version = "1",
                    Description = "Through this api, a user will be granted access to a book",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "support@healthstation.ng",
                        Name = "Solomon",
                        Url = new Uri("https://healthstation.ng/")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("Admin", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Admin API",
                    Version = "1",
                    Description = "Through this api, Admin can oversee all the activities of the application",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "support@healthstation.ng",
                        Name = "Sam",
                        Url = new Uri("https://healthstation.ng/")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            services.AddScoped<IAdmin, Admin>();
            services.AddScoped<IUser, User>();
            services.AddScoped<BookStoreDbContext>();
            services.AddScoped<UserDTO>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {

                setupAction.SwaggerEndpoint("/swagger/User/swagger.json", "Test User");
                setupAction.SwaggerEndpoint("/swagger/Admin/swagger.json", "Test Admin");
                setupAction.RoutePrefix = "";

            });

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
           // app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                 endpoints.MapControllers();
              
            });
        }
    }
}







