using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Database
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        [Required]
        public string HashedPassword { get; set; }
    }
}
