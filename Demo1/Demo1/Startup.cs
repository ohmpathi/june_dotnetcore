using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Demo1.Database;
using Demo1.SchoolDBModels;
//using Demo1.SchoolDBModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
