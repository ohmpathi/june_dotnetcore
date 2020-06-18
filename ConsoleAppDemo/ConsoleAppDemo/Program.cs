using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

            //var data = dbContext.Students.Include("StudentCourses").ToList();

            var data = from s in dbContext.Students
                       join sc in dbContext.StudentCourse on s.Id equals sc.StudentId into scGroup
                       from scg in scGroup.DefaultIfEmpty()
                       join c in dbContext.Courses on scg.CourseId equals c.Id into scGroup2
                       from scg2 in scGroup2.DefaultIfEmpty()
                       select new
                       {
                           StudentName = s.Name,
                           Course = scg2.Name
                       };

            var data2 = from s in dbContext.Students
                        from sc in dbContext.StudentCourse.Where(sc => s.Id == sc.StudentId).DefaultIfEmpty()
                        from c in dbContext.Courses.Where(c => sc.CourseId == c.Id).DefaultIfEmpty()
                        select new
                        {
                            StudentName = s.Name,
                            Course = c.Name
                        };


            foreach (var sc in data)
            {
                Console.WriteLine(sc.StudentName + "\t" + sc.Course);
            }

        }
    }
}
