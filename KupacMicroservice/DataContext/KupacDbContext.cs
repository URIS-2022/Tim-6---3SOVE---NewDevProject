using KupacMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;


namespace KupacMicroservice.DataContext
{

    public class KupacDbContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public KupacDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("KupacDb"));
        }     
       
        public DbSet<Kupac> Kupci { get; set; }
        public DbSet<FizickoLice> FizickaLica { get; set; }
        public DbSet<PravnoLice> PravnaLica { get; set; }
        public DbSet<Liciter> Liciteri { get; set; }
        public DbSet<OvlascenoLice> OvlascenaLica { get; set; }



        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder Builder)
        {

            Builder.Entity<Kupac>().
               HasKey(t => t.KupacId);

            Builder.Entity<FizickoLice>().
                HasKey(t => new { t.KupacId, t.FizickoliceId });

            Builder.Entity<PravnoLice>()
                .HasKey(t => new { t.KupacId, t.PravnoliceId});


            Builder.Entity<OvlascenoLice>().HasKey(t => new { t.KupacId, t.LiciterId });

            Builder.Entity<OvlascenoLice>()
                .HasOne(p => p.Kupac)
                .WithMany()
                .HasForeignKey(p => p.KupacId);

            Builder.Entity<OvlascenoLice>()
                .HasOne(p => p.Liciter)
                .WithMany()
                .HasForeignKey(p => p.LiciterId);



            Builder.Entity<Kupac>()
               .HasData(
               new Kupac
               {

                   KupacId = Guid.Parse("16A17928-85A4-43A0-A4F1-A48A0788BDFB"),
                   AdresaKupac = "Nis",
                   OstvarenaPovrsina = 600.00,
                   ImaZabranu = false,
                   DatumPocetkaZabrane = DateTime.Parse("2019-11-15T09:00:00"),
                   DuzinaTrajanjaZabraneGod = 2,
                   BrojTelefona1 = "0617290600",
                   BrojTelefona2 = "0647089023",
                   Email = "skijac@gmail.com",
                   BrojRacuna = "902 ‑ 11501 ‑ 97",
                   IznosUplata = "123908",
                   Prioritet = "Vlasnik sistema za navodnjavanje"

               },
               new Kupac
               {

                   KupacId = Guid.Parse("32C2D78F-ADC0-41BF-83F4-B7F4E13DA966"),
                   AdresaKupac = "Beocin",
                   OstvarenaPovrsina = 700.00,
                   ImaZabranu = false,
                   DatumPocetkaZabrane = DateTime.Parse("2018-11-15T09:00:00"),
                   DuzinaTrajanjaZabraneGod = 3,
                   BrojTelefona1 = "0637240699",
                   BrojTelefona2 = "0617569013",
                   Email = "toplana@gmail.com",
                   BrojRacuna = "908 ‑ 10501 ‑ 97",
                   IznosUplata = "220000",
                   Prioritet = "Vlasnik zemljista koje se granici sa zemljistem koje se daje u zakup"

               },
               new Kupac
               {


                   KupacId = Guid.Parse("4F602E62-FA2F-425D-80C5-56D3FE1FC6D4"),
                   AdresaKupac = "Beocin",
                   OstvarenaPovrsina = 700.00,
                   ImaZabranu = true,
                   DatumPocetkaZabrane = DateTime.Parse("2019-11-15T09:00:00"),
                   DuzinaTrajanjaZabraneGod = 3,
                   BrojTelefona1 = "0637240699",
                   BrojTelefona2 = "0617569013",
                   Email = "biohemija@gmail.com",
                   BrojRacuna = "908 ‑ 10501 ‑ 97",
                   IznosUplata = "220000",
                   Prioritet = "Vlasnik zemljista koje je najblize zemljistu koje se daje u zakup"


               },
               new Kupac
               {


                   KupacId = Guid.Parse("E65BADA3-210E-4FAD-9D4E-14CB41CC1AE1"),
                   AdresaKupac = "Zrenjanin",
                   OstvarenaPovrsina = 1000.00,
                   ImaZabranu = true,
                   DatumPocetkaZabrane = DateTime.Parse("2017-11-15T09:00:00"),
                   DuzinaTrajanjaZabraneGod = 4,
                   BrojTelefona1 = "0697230791",
                   BrojTelefona2 = "0617499014",
                   Email = "gradskauprava@gmail.com",
                   BrojRacuna = "903 ‑ 12601 ‑ 97",
                   IznosUplata = "340000",
                   Prioritet = "Poljoprivrednik koji je upisan u registar"

               });


            Builder.Entity<FizickoLice>()
                .HasData(
                new FizickoLice
                {
                    FizickoliceId = Guid.Parse("8E914117-DE7E-41BD-86B4-C529FA278817"),
                    KupacId = Guid.Parse("16A17928-85A4-43A0-A4F1-A48A0788BDFB"),
                    Ime = "Petar",
                    Prezime = "Petrovic",
                    JMBG = "2906000855002",
                    AdresaFizickoLice = "Novi Sad"


                },
                new FizickoLice
                {


                    FizickoliceId = Guid.Parse("16FD3722-E965-494F-9DE6-FC4046D49F31"),
                    KupacId = Guid.Parse("4F602E62-FA2F-425D-80C5-56D3FE1FC6D4"),
                    Ime = "Marko",
                    Prezime = "Markovic",
                    JMBG = "1202966156142",
                    AdresaFizickoLice = "Subotica"

                });



            Builder.Entity<PravnoLice>()
                .HasData(
                new PravnoLice
                {
                    PravnoliceId = Guid.Parse("4422D9D6-B4E5-4470-A00F-520DCA57118B"),
                    KupacId = Guid.Parse("32C2D78F-ADC0-41BF-83F4-B7F4E13DA966"),
                    Naziv = "Toplana",
                    MaticniBroj = "12545690",
                    Faks = "+1-213-9856512",
                    KontaktOsobaIme = "Dragan",
                    KontaktOsobaPrezime = "Draganovic",
                    AdresaPravnoLice = "Beograd"

                },
                new PravnoLice
                {


                    PravnoliceId = Guid.Parse("594B1F58-522C-4314-9072-7553EABB72B6"),
                    KupacId = Guid.Parse("E65BADA3-210E-4FAD-9D4E-14CB41CC1AE1"),
                    Naziv = "Biohemija",
                    MaticniBroj = "56790123",
                    Faks = "+1-208-9946522",
                    KontaktOsobaIme = "Dejan",
                    KontaktOsobaPrezime = "Lukic",
                    AdresaPravnoLice = "Kragujevac"
                });


            Builder.Entity<Liciter>()
              .HasData(
              new Liciter
              {
                  LiciterId = Guid.Parse("B76BC440-2E4F-468D-B5E4-7529767CF9BE"),
                  ImeLiciter = "Luka",
                  PrezimeLiciter = "Lukic",
                  JmbgLiciter = "0711090293012",
                  Brojpasosa = "NO0000801",
                  Drzavastranac = "ozankazastranudrzavu",
                  AdresaLiciter = "Mesto"

              },
              new Liciter
              {

                  LiciterId = Guid.Parse("895AA525-1762-4981-8D82-8874B0022A49"),
                  ImeLiciter = "Nikola",
                  PrezimeLiciter = "Nikolic",
                  JmbgLiciter = "1731494293013",
                  Brojpasosa = "NO000001",
                  Drzavastranac = "ozankazastranudrzavu",
                  AdresaLiciter = "Mesto2"

              });


            Builder.Entity<OvlascenoLice>()
               .HasData(
               new
               {
                   KupacId = Guid.Parse("16A17928-85A4-43A0-A4F1-A48A0788BDFB"),
                   LiciterId = Guid.Parse("B76BC440-2E4F-468D-B5E4-7529767CF9BE")
               },
               new
               {
                   KupacId = Guid.Parse("32C2D78F-ADC0-41BF-83F4-B7F4E13DA966"),
                   LiciterId = Guid.Parse("B76BC440-2E4F-468D-B5E4-7529767CF9BE")
               },
               new
               {
                   KupacId = Guid.Parse("32C2D78F-ADC0-41BF-83F4-B7F4E13DA966"),
                   LiciterId = Guid.Parse("895AA525-1762-4981-8D82-8874B0022A49")
               });




















        }

    }
}
