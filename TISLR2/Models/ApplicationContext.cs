using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TISLR2.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :
        base(options)
        {
            Database.EnsureCreated();
        }
    }
}
