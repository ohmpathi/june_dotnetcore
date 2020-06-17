using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {

        }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<PersonAddress> PersonAddress { get; set; }
        //public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB; Database=MyDb; Integrated Security=true");
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

            //modelBuilder.Entity<StudentCourse>()
            //   .HasOne(pt => pt.Course)
            //   .WithMany(p => p.StudentCourses)
            //   .HasForeignKey(pt => pt.CourseId);

            //modelBuilder.Entity<StudentCourse>()
            //    .HasOne(pt => pt.Student)
            //    .WithMany(t => t.StudentCourses)
            //    .HasForeignKey(pt => pt.StudentId);
        }
    }
}