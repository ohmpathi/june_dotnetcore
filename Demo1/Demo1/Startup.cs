using System.Collections.Generic;
using System.IO;
using Demo1.Database;
using Demo1.SchoolDBModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo1
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
            ILoggerFactory loggerFactory = LoggerFactory.Create(config => config.AddDebug());

            services.AddControllers();

            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
                options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection"));
            });
            services.AddDbContext<SchoolDBContext>(options =>
            {
                options.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
                options.UseSqlServer(Configuration.GetConnectionString("SchoolDBConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            //app.UseDefaultFiles();
            /////// default.htm
            /////// default.html
            /////// index.htm
            /////// index.html
            //app.UseDefaultFiles(new DefaultFilesOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath = "/StaticFiles",
            //    DefaultFileNames = new List<string> { "home.html" }
            //});

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});

            //app.UseDirectoryBrowser();
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});


            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true
            });

            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                RequestPath = "/StaticFiles"
            };
            options.DefaultFilesOptions.DefaultFileNames = new List<string> { "home.html" };
            app.UseFileServer(options);


            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World From 1st Middleware!" + Environment.NewLine);
            //    await next();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World From 2nd Middleware" + Environment.NewLine);
            //});


            app.UseResponseWrapperMiddleware();
            //app.UseMiddleware<ResponseWrapperMiddleware>();


            app.Map("/api/employee", (app) =>
            {
                app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("response for person route");
                });
            });

            app.Map("/level1", level1App =>
            {
                level1App.Map("/level2a", level2AApp =>
                {
                    // "/level1/level2a" processing
                });
                level1App.Map("/level2b", level2BApp =>
                {
                    // "/level1/level2b" processing
                });
            });

            //CustomAuthorization(app);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void CustomAuthorization(IApplicationBuilder app)
        {
            app.MapWhen((httpContext) =>
            {
                var header = httpContext.Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(header)) return true;

                return header != "mysecret";

            }, (app) =>
            {
                app.Run(async (context) =>
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("You are not authorized to view this content");
                });
            });
        }
    }
}
