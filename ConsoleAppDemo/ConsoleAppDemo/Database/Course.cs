using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Database
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<StudentCourse> StudentCourses { get; set; }
        public CourseStatus Status { get; set; }
    }

    public enum CourseStatus
    {
        ContentNotDefined = 1,
        Discontinued,
        Available
    }
}
