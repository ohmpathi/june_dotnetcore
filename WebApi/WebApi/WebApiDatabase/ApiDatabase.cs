using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.WebApiDatabase
{
    public class ApiDatabase: DbContext
    {
        public ApiDatabase(DbContextOptions<ApiDatabase> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
