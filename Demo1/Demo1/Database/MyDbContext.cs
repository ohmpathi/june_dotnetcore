using Demo1.BasicAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }
        //public DbSet<PersonAddress> PersonAddress { get; set; }
        public DbSet<Project> Projects { get; set; }



        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<User>().HasData(new List<User> {
                new User { UserName = "admin", HashedPassword = UserService.ComputeSha256Hash("1234") },
                new User { UserName = "user", HashedPassword = UserService.ComputeSha256Hash("admin") }
            });
        }
    }
}