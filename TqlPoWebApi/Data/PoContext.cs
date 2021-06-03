using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TqlPoWebApi.Models;

namespace TqlPoWebApi.Data
{
    public class PoContext : DbContext
    {
        public PoContext (DbContextOptions<PoContext> options)
            : base(options)
        {
        }

        public DbSet<TqlPoWebApi.Models.Employee> Employee { get; set; }
        public DbSet<PO> POs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>(e =>
            {
                e.HasIndex(p => p.Login).IsUnique();
            });
        }
    }
}
