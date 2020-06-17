using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleAppDemo.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleAppDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDbContext dbContext = new MyDbContext();

            //dbContext.Courses.Add(new Course { Name = "EF Core" });
            //dbContext.SaveChanges();

            //var id = dbContext.Courses.Find(6);
            //id.Name = "EF Core 3.1";
            //dbContext.SaveChanges();

            try
            {
                using (var trans = dbContext.Database.BeginTransaction())
                {
                    dbContext.Students.Add(new Student { Name = "XYZ 2" });
                    dbContext.SaveChanges();

                    var courses = dbContext.Courses.FromSqlRaw("select Id, name, isAvailable, createdDate, updatedDate from Courses").ToList();

                    trans.Commit();
                }
            }
            catch
            {
                Console.WriteLine("Transaction failed");
            }
        }
    }
}
