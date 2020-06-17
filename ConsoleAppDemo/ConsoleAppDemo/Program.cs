using System;
using System.Linq;
using ConsoleAppDemo.Database;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDbContext dbContext = new MyDbContext();

            var students = dbContext.Students;
                //.Include("StudentCourses")
                //.Include("StudentCourses.Course");

            foreach (var s in students)
            {
                foreach (var sc in s.StudentCourses)
                {
                    Console.WriteLine(s.Name + "\t: " + sc.Course.Name + ", ");
                }
            }


            //var data = from student in dbContext.Students
            //           from sc in student.StudentCourses
            //           select new
            //           {
            //               student.Name,
            //               Course = sc.Course.Name
            //           };

            //foreach(var c in data)
            //{
            //    Console.WriteLine(c.Name + "\t: " + c.Course);
            //}
        }
    }
}
