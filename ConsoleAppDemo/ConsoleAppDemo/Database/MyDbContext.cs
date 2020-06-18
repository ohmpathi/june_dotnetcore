using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Database
{
    public class MyDbContext : DbContext
    {
        private readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public MyDbContext() { }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory).EnableSensitiveDataLogging()
                .UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB; Database=MyDb; Integrated Security=true");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey("CourseId", "StudentId");

            string locationsStr = File.ReadAllText(@"C:\Users\320040910\source\repos\20june_dotnetcore\ConsoleAppDemo\ConsoleAppDemo\Location.json");
            var data = JObject.Parse(locationsStr);
            var locations = JArray.FromObject(data["Locations"]).Select(l => JsonConvert.DeserializeObject<Location>(l.ToString()));

            modelBuilder.Entity<Location>().HasData(locations);


            //modelBuilder.Entity<Course>().Property(typeof(int), "IsAvailable");
            //modelBuilder.Entity<Course>().Property(typeof(DateTime), "CreatedDate");
            //modelBuilder.Entity<Course>().Property(typeof(DateTime?), "UpdatedDate");

            //modelBuilder.Entity<Course>().HasQueryFilter(e => EF.Property<int>(e, "IsAvailable") == 1);

            modelBuilder.Entity<Course>()
                .Property(e => e.Status)
                //.HasConversion(
                //    v => v.ToString(),
                //    v => (CourseStatus)Enum.Parse(typeof(CourseStatus), v));
                .HasConversion(new EnumToStringConverter<CourseStatus>());
        }

        //public override int SaveChanges()
        //{
        //    foreach(var entry in ChangeTracker.Entries())
        //    {
        //        if(entry.Entity is Course)
        //        {
        //            if(entry.State == EntityState.Added)
        //            {
        //                entry.Property("CreatedDate").CurrentValue = DateTime.Now;
        //            }
        //            else if(entry.State == EntityState.Modified)
        //            {
        //                entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
        //            }
        //        }
        //    }

        //    return base.SaveChanges();
        //}

        //public DbSet<Person> Persons { get; set; }
        //public DbSet<PersonAddress> PersonAddresses { get; set; }

        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<ContractEmployee> ContractEmployees { get; set; }
        //public DbSet<PermanentEmployee> PermanentEmployees { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }

        public DbSet<Location> Locations { get; set; }

    }
}