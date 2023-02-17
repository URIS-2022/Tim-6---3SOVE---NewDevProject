using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;



namespace PrijavaJnService.Entities.DataContext
{
    public class PrijavaJnContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PrijavaJnContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<PrijavaJn> PrijavaJn { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PrijavaJnDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrijavaJn>()
               .HasKey(p => p.PrijavaId);

            modelBuilder.Entity<PrijavaJn>()
             .HasData(
             new
             {
                 PrijavaId = Guid.Parse("3040da81-b4b5-47bd-a47c-f1474341f162"),
                 BrojPrijave = "B22",
                 DatumPrijave = DateTime.Now,
                 MestoPrijave = "Mesto 1",
                 SatPrijave = DateTime.Now.ToString("HH:mm"),
                 ZatvorenaPonuda = true,
                 DokFizickaLica = "prijava za fizicka lica",
                 DokPravnaLica = "prijava za pravna lica obrazac 4",
             },
             new
             {
                 PrijavaId = Guid.Parse("a370bc58-2cb2-4d8d-9cfb-b444841aeb80"),
                 BrojPrijave = "B255",
                 DatumPrijave = DateTime.Now,
                 MestoPrijave = "Mesto 2",
                 SatPrijave = DateTime.Now.ToString("HH:mm"),
                 ZatvorenaPonuda = false,
                 DokFizickaLica = "prijava, odjava",
                 DokPravnaLica = "prijava za pravna lica",
             });

        }
    }
}
