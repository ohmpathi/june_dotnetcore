using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleAppDemo.Database
{
    public class ContractEmployee
    {
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual Employee employee { get; set; }
        public int ContractPeriodYrs { get; set; }
    }
}
