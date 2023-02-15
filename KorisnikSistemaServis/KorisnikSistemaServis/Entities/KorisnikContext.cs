using KorisnikSistemaServis.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace KorisnikSistemaServis.Entities
{
    public class KorisnikContext:DbContext
    {
        private readonly IConfiguration configuration;
        public KorisnikContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Korisnik> Korisnici { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Users"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Korisnik>()
                .HasData(new
                {
                    KorisnikId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Ime = "Sanja",
                    Prezime = "Tica",
                    KorisnickoIme = "sanja123",
                    Lozinka = BCrypt.Net.BCrypt.HashPassword("test123"),
                    TipKorisnika = TipoviKorisnika.Administrator
                });
            builder.Entity<Korisnik>()
                .HasData(new
                {
                    KorisnikId = Guid.Parse("9245fe4a-d402-451c-b9ed-9c1a04247482"),
                    Ime = "Jovana",
                    Prezime = "Jovanovic",
                    KorisnickoIme = "jovanaj",
                    Lozinka = BCrypt.Net.BCrypt.HashPassword("jovanatest"),
                    TipKorisnika = TipoviKorisnika.OperaterNadmetanja
                });
        }
    }
}
