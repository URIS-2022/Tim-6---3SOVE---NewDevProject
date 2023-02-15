global using Microsoft.EntityFrameworkCore;
using MikroservisKomsija.Models;

namespace MikroservisKomsija.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=PROJEKATdb;Trusted_Connection=true;TrustServerCertificate=true;");
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KomisijaClan>().HasKey(kclan => new { kclan.IDKomsije, kclan.IDClan });
        }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Komisija> Koms { get; set; }

        public DbSet<KomisijaClan> KCs { get; set; }
    }
}
