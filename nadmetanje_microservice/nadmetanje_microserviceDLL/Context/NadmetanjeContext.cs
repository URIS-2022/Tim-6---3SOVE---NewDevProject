using Microsoft.EntityFrameworkCore;
using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceDLL.Context
{
    public class NadmetanjeContext : DbContext
    {
        public NadmetanjeContext(DbContextOptions<NadmetanjeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Etapa> Etape { get; set; }
        public virtual DbSet<Nadmetanje> Nadmetanja { get; set; }

    }
}
