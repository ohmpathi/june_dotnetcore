using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Database
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<StudentCourse> StudentCourse { get; set; }
    }
}
