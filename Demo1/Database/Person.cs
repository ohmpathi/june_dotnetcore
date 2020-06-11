using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Database
{
    [Table("PeopleMaster")]
    public class Person
    {
        [Key]
        public int PersonNumber { get; set; }
        
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }

        [MinLength(6)]
        public string City { get; set; }

        [Column("DateOfBirth")]
        public DateTime? DOB { get; set; }

        public int addressId { get; set; }

        [ForeignKey("addressId")]
        public virtual PersonAddress personAddress { get; set; }
    }
}
