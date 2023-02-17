using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace LicitacijaService.Entities.DataContext
{
    public class LicitacijaContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public LicitacijaContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<ProgramEntitet> ProgramEntitet { get; set; }
        public DbSet<Licitacija> Licitacija{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("LicitacijaDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgramEntitet>()
              .HasKey(p => p.ProgramId);

            modelBuilder.Entity<Licitacija>()
              .HasKey(p => p.LicitacijaId);

            modelBuilder.Entity<ProgramEntitet>()
                .HasData(new
                {
                    ProgramId = Guid.Parse("31511a3e-60de-4d24-80c4-00947314092d"),
                    MaksimalnaPovrsina = 200,
                    KrugLicitacije =  1
                },
                new
                {
                    ProgramId = Guid.Parse("6942fde3-13e0-4832-bfbf-8a9a10572b70"),
                    MaksimalnaPovrsina = 300,
                    KrugLicitacije = 1
                },
                new
                {
                    ProgramId = Guid.Parse("58b116a2-d458-4f72-abe8-be6135ece89e"),
                    MaksimalnaPovrsina = 500,
                    KrugLicitacije = 2
                });

            modelBuilder.Entity<Licitacija>()
                .HasData(new
                {
                    LicitacijaId = Guid.Parse("98992fb8-8ba8-4586-a7a3-6e2b5472897b"),
                    BrojLicitacije = 42,
                    GodinaLicitacije = 2012,
                    OgranicenjeLicitacije = 12,
                    RokLicitacije = DateTime.Now.AddDays(-500),
                    KorakCeneLicitacije = 3,
                    ProgramEntitetProgramId = Guid.Parse("31511a3e-60de-4d24-80c4-00947314092d")
                },
                new
                {
                    LicitacijaId = Guid.Parse("9d70d72c-7f57-4323-9eb7-1444ef348220"),
                    BrojLicitacije = 42,
                    GodinaLicitacije = 2012,
                    OgranicenjeLicitacije = 12,
                    RokLicitacije = DateTime.Now.AddDays(-500),
                    KorakCeneLicitacije = 3,
                    ProgramEntitetProgramId = Guid.Parse("6942fde3-13e0-4832-bfbf-8a9a10572b70")
                },
                new
                {
                    LicitacijaId = Guid.Parse("684c392b-7871-4d1c-a93d-9d8661adf9e9"),
                    BrojLicitacije = 42,
                    GodinaLicitacije = 2012,
                    OgranicenjeLicitacije = 12,
                    RokLicitacije = DateTime.Now.AddDays(-500),
                    KorakCeneLicitacije = 3,
                    ProgramEntitetProgramId = Guid.Parse("58b116a2-d458-4f72-abe8-be6135ece89e")
                });
        }
    }
}
