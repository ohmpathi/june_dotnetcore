using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConsoleAppDemo.Database
{
    public class PermanentEmployee
    {
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual Employee employee { get; set; }
        public string Priviliges { get; set; }
    }
}
