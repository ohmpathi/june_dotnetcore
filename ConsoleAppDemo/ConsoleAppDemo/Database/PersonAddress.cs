using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Database
{
    public class PersonAddress
    {
        public int PersonAddressId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual Person person { get; set; }
    }
}
