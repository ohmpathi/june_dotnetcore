using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppDemo.Database
{
    [Owned]
    public class PersonAddress
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}
