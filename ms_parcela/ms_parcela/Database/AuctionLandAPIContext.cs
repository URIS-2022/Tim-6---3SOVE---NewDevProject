global using Microsoft.EntityFrameworkCore;
using ms_parcela.Entities;
using ms_parcela.Entities.DeoParceleE;
using ms_parcela.Entities.ParcelaE;



namespace ms_parcela.Database
{
    public class AuctionLandAPIContext : DbContext
    {
        public AuctionLandAPIContext(DbContextOptions<AuctionLandAPIContext> options) : base(options)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=AuctionLandParcelaDB;Trusted_Connection=true;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KatastarskaOpstina>()
                .Property(u => u.nazivKatastarskeOpstine)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<KatastarskaOpstina>().HasData(new
            {
                brojKatastarskeOpstine = Guid.NewGuid(),
                nazivKatastarskeOpstine = opstine.Cantavir
            }, new
            {
                brojKatastarskeOpstine = Guid.NewGuid(),
                nazivKatastarskeOpstine = opstine.Bajmok
            });

            modelBuilder.Entity<Zona>()
               .Property(u => u.brojZona)
               .HasConversion<string>()
            .HasMaxLength(50);

            modelBuilder.Entity<Zona>().HasData(new
            {
                zonaID = Guid.NewGuid(),
                brojZona = brojevi_zona.zona3
            }, new
            {
                zonaID = Guid.NewGuid(),
                brojZona = brojevi_zona.zona4
            });

            modelBuilder.Entity<DeoParcele>().HasData(new
            {
                deoParceleID = Guid.NewGuid(),
                redniBrojDelaParcele = 3,
                povrsinaDelaParcele = 200,
                brojParcele = 1,
                brojKatastraskaOpstina = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),

            },
            new
            {
                deoParceleID = Guid.NewGuid(),
                redniBrojDelaParcele = 4,
                povrsinaDelaParcele = 200,
                brojParcele = 1,
                brojKatastraskaOpstina = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
            });
            

            modelBuilder.Entity<Parcela>().
               HasKey(KoBrp => new { KoBrp.brojKatastraskaOpstina, KoBrp.brojParcele });

            modelBuilder.Entity<Parcela>()
               .Property(u => u.kultura)
               .HasConversion<string>()
               .HasMaxLength(50);
            modelBuilder.Entity<Parcela>()
               .Property(u => u.klasa)
               .HasConversion<string>()
               .HasMaxLength(50);
            modelBuilder.Entity<Parcela>()
               .Property(u => u.obradivost)
               .HasConversion<string>()
               .HasMaxLength(50);
            modelBuilder.Entity<Parcela>()
               .Property(u => u.oblikSvojine)
               .HasConversion<string>()
               .HasMaxLength(50);
            modelBuilder.Entity<Parcela>()
               .Property(u => u.odvodnjavanje)
               .HasConversion<string>()
               .HasMaxLength(50);

            


        }
        public DbSet<KatastarskaOpstina> KatastarskaOpstinas { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<DeoParcele> DeoParceles { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }






    }
}
