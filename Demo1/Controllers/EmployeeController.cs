using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo1.SchoolDBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private SchoolDBContext dbContext;

        public EmployeeController(SchoolDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(dbContext.Employee.Select(e =>
            new
            {
                EmployeeId = e.Id,
                EmployeeName = e.Name,
                Department = e.Department.Name
            }));
        }

        [HttpGet]
        [Route("departments")]
        public IActionResult Get2()
        {
            return Ok(dbContext.Department.Select(d =>
            new
            {
                DepartmentId = d.Id,
                DepartmentName = d.Name,
                Employees = d.Employee.ToList()
            }));
        }

    }
}