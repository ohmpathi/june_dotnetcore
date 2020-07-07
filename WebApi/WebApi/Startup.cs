using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services;
using WebApi.WebApiDatabase;

namespace WebApi
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
            services.AddControllers();

            services.AddDbContext<ApiDatabase>(options => options.UseSqlServer(Configuration.GetConnectionString("ApiDatabaseConnection")));

            services.AddScoped<UserService>();

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AuthKey").Value);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("ChangeRole", policy => policy.RequireRole("Admin"));
                option.AddPolicy("accesSecret", policy => policy.RequireRole("Admin", "user"));

                option.AddPolicy("CityAccess", policy => policy.RequireAssertion(context =>
                {
                    var city = context.User.Claims.Single(c => c.Type == "City");
                    return city.Value == "New York";
                }));

                //option.AddPolicy("CityAccess", policy => policy.RequireClaim("New York"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    //app.UseDeveloperExceptionPage();
            //}

            //app.UseStatusCodePagesWithReExecute("/api/error");

            //app.UseExceptionHandler("/api/error");
            //app.UseHsts();

            app.UseExceptionHandler(appBuilder);


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void appBuilder(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/html";

                await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                // Use exceptionHandlerPathFeature to process the exception (for example, 
                // logging), but do NOT expose sensitive error information directly to 
                // the client.

                if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                {
                    await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                }

                await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                await context.Response.WriteAsync("</body></html>\r\n");
                await context.Response.WriteAsync(new string(' ', 512)); // IE padding
            });
        }
    }
}
