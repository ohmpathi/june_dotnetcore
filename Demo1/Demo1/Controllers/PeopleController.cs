using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Demo1.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demo1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private MyDbContext dbContext { get; set; }
        private IConfiguration Configuration { get; set; }

        public PeopleController(MyDbContext dbContext, IConfiguration Configuration)
        {
            this.dbContext = dbContext;
            this.Configuration = Configuration;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Name = "Project name",
                People = "Assigned people"
            });

            //return Ok(dbContext.People.Select(p => new
            //{
            //    p.Name,
            //    ProjectName = p.Project.Name
            //}));

            //return Ok(Configuration.GetSection("ConfigProperty").Value);
        }

        [HttpGet]
        [Route("students")]
        public IActionResult Get2()
        {
            var x = dbContext.Students.First();

            return Ok(new
            {
                x.Name,
                studentCourse = x.StudentCourse.Select(sc => new
                {
                    sc.Course.Name
                })
            });
        }


        [HttpGet]
        [Route("proc")]
        public IActionResult Get3()
        {
            dbContext.Projects.FromSqlRaw("");
            dbContext.Database.ExecuteSqlRaw("exec proc_nae");

            string conn = dbContext.Database.GetDbConnection().ConnectionString;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("exec USP_Sample", conn);

            //DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            sqlDataAdapter.Fill(ds);

            return Ok();
        }
    }
}