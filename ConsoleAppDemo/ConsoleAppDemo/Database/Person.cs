using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Database
{
    [Table("PeopleMaster")]
    public class Person
    {
        [Key]
        public int PersonNumber { get; set; }
        
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }

        public virtual PersonAddress PersonAddress { get; set; }
        //public virtual List<PersonAddress> DeliveryAddresses { get; set; }

    }
}
