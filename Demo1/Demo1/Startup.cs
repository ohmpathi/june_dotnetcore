using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo1.Database;
using Demo1.SchoolDBModels;
//using Demo1.SchoolDBModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}