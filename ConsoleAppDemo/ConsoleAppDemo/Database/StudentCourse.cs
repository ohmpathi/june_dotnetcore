using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Database
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        //[ForeignKey("Student")]
        public Student Student { get; set; }

        public int CourseId { get; set; }
        //[ForeignKey("Course")]
        public Course Course { get; set; }
    }
}
