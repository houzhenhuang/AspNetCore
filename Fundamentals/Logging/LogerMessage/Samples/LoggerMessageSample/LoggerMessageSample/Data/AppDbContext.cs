using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerMessageSample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }
    }
}
