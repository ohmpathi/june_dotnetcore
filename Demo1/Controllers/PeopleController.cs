using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo1.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private MyDbContext dbContext { get; set; }
        public PeopleController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(dbContext.People);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var person = dbContext.People.FirstOrDefault(p => p.Id == id);

            if (person != null)
            {
                return Ok(person);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(Person person)
        {
            //person.Id = dbContext.People.Max(p => p.Id) + 1;
            dbContext.People.Add(person);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Person person)
        {
            var p = dbContext.People.FirstOrDefault(p2 => p2.Id == person.Id);
            if (p == null)
            {
                //person.Id = people.Max(p2 => p2.Id) + 1;
                dbContext.People.Add(person);
            }
            else
            {
                p.Name = person.Name;
                p.City = person.City;
                dbContext.People.Attach(p)
                    .State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var p = dbContext.People.FirstOrDefault(p2 => p2.Id == id);
            if (p != null)
            {
                dbContext.People.Remove(p);
            }
            dbContext.SaveChanges();
            return Ok();
        }
    }
}