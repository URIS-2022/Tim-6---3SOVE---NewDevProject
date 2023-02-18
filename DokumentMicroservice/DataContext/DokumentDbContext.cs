using AutoMapper;
using DokumentMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


namespace DokumentMicroservice.DataContext
{
    public class DokumentDbContext : DbContext
    {

        private readonly IConfiguration _configuration;
       

        public DokumentDbContext(DbContextOptions <DokumentDbContext>options, IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DocumentDb"));
        }


        public DbSet<Dokument> Dokumenti { get; set; }
        public DbSet<Oglas> Oglasi { get; set; }
        public DbSet<PredlogPlanaProjekta> PredloziPlanaProjekta { get; set; }
        public DbSet<ResenjeStrucnaKomisija> Resenjastrucnakomisija { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Dokument>().
                HasKey(t => t.DokumentId);

            modelBuilder.Entity<Oglas>()
                .HasKey(t => new { t.DokumentId, t.OglasId });

            modelBuilder.Entity<PredlogPlanaProjekta>().
                HasKey(t => new { t.DokumentId, t.PredlogId});

            modelBuilder.Entity<ResenjeStrucnaKomisija>().
                HasKey(t => new { t.DokumentId, t.ResenjeId});


            modelBuilder.Entity<Dokument>()
               .HasData(
                new Dokument
               {
                   DokumentId = Guid.Parse("9E2474B3-CAF5-4123-944D-11B4AB186B6B"),
                   Sablon = "sablondokument1",
                   DatumDonosenjaDokumenta = DateTime.Parse("2022-11-15T09:00:00")
               }
               , new Dokument
               {

                   DokumentId = Guid.Parse("E4BE32F2-DA5E-47D5-9FBF-B860EB1D79B3"),
                   Sablon = "sablondokument1",
                   DatumDonosenjaDokumenta = DateTime.Parse("2022-11-15T09:00:00")

               }
               , new Dokument
               {

                   DokumentId = Guid.Parse("C8E97D45-4BFC-4DCA-BA07-20D1E93049D6"),
                   Sablon = "sablondokument9",
                   DatumDonosenjaDokumenta = DateTime.Parse("2022-11-15T09:00:00")


               }
               , new Dokument
               {
                   DokumentId = Guid.Parse("9A2D9EA6-D264-494A-88F8-51808A1C3196"),
                   Sablon = "sablondokument3",
                   DatumDonosenjaDokumenta = DateTime.Parse("2019-11-15T09:00:00")

               }
               , new Dokument
               {

                   DokumentId = Guid.Parse("D762B24F-2730-427F-9789-3D840A5F7E39"),
                   Sablon = "sablondokument4",
                   DatumDonosenjaDokumenta = DateTime.Parse("2018-11-15T09:00:00")

               }
               , new Dokument
               {

                   DokumentId = Guid.Parse("418AF1D2-483F-4461-8D82-31B257527A4F"),
                   Sablon = "sablondokument9",
                   DatumDonosenjaDokumenta = DateTime.Parse("2013-11-15T09:00:00")

               });



            modelBuilder.Entity<Oglas>()
                 .HasData(
                 new Oglas
                 {
                     OglasId = Guid.Parse("011C616C-14DB-4BC3-A9C9-C2F642B22F73"),
                     DokumentId = Guid.Parse("9A2D9EA6-D264-494A-88F8-51808A1C3196"),
                     BrNadmetanjaJmbg = "2906000855008",
                     BrNadmetanjaMaticniBrojPreduzeca = "24905678",
                     zalbaID = Guid.Parse("946D1FEE-3863-4FB5-B76B-1970FBB4894E")

                 }
                 , new Oglas
                 {

                     OglasId = Guid.Parse("9151A095-2890-49C8-A3AC-F5813B92FFAC"),
                     DokumentId = Guid.Parse("9E2474B3-CAF5-4123-944D-11B4AB186B6B"),
                     BrNadmetanjaJmbg = "2906000855002",
                     BrNadmetanjaMaticniBrojPreduzeca = "23905678",
                     zalbaID = Guid.Parse("7D5213E5-F1BB-4226-B034-AE061C672780")


                 });
                 
            
            
            modelBuilder.Entity<PredlogPlanaProjekta>()
                 .HasData(
                 new PredlogPlanaProjekta
                 {
                     PredlogId = Guid.Parse("4FF44338-8D0B-415A-8B44-DFCE3D4C311D"),
                     DokumentId = Guid.Parse("E4BE32F2-DA5E-47D5-9FBF-B860EB1D79B3"),
                     ZavodniBr = "PSPG-1/2022",
                     DatumPredlog = DateTime.Now

                 }
                 , new PredlogPlanaProjekta
                 {

                     PredlogId = Guid.Parse("B708754F-B5E9-481B-8898-CA3682107E9C"),
                     DokumentId = Guid.Parse("D762B24F-2730-427F-9789-3D840A5F7E39"),
                     ZavodniBr = "PSPG-5/2022",
                     DatumPredlog = DateTime.Now


                 });
            

            modelBuilder.Entity<ResenjeStrucnaKomisija>()
                .HasData(
                new ResenjeStrucnaKomisija
                {
                    ResenjeId = Guid.Parse("22B74319-EDB5-4945-B792-76D0B1B81D88"),
                    DokumentId = Guid.Parse("C8E97D45-4BFC-4DCA-BA07-20D1E93049D6"),
                    Zavodnibr = "PSPG-2/2022",
                    DatumResenje = DateTime.Now,
                    ImeClanaKomisije = "Marko",
                    PrezClanaKomisije = "Markovic",
                    PredsednikKomisije = "PredPetarPetrovic"

                }
                , new ResenjeStrucnaKomisija
                {

                    ResenjeId = Guid.Parse("E3EAB479-B0AA-4161-BBC9-D2281F43F332"),
                    DokumentId = Guid.Parse("418AF1D2-483F-4461-8D82-31B257527A4F"),
                    Zavodnibr = "PSPG-9/2022",
                    DatumResenje = DateTime.Now,
                    ImeClanaKomisije = "Luka",
                    PrezClanaKomisije = "Markovic",
                    PredsednikKomisije = "PredDraganDraganovic"

                });

           
        }
    }
}