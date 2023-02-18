using Microsoft.EntityFrameworkCore;
using ms_zalba.Entities;
using ms_zalba.Entities.ZalbaE;
using ms_zalba.Entities.UgovorOZakupuE;


namespace ms_zalba.Database
{
    public class AuctionLandAPIContext : DbContext
    {
        public AuctionLandAPIContext(DbContextOptions<AuctionLandAPIContext> options) : base(options)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=AuctionLandZalbaDB;Trusted_Connection=true;TrustServerCertificate=True");
        }
        /*public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter()
                : base(dateOnly =>
                        dateOnly.ToDateTime(TimeOnly.MinValue),
                    dateTime => DateOnly.FromDateTime(dateTime))
            { }
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {

            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            base.ConfigureConventions(builder);

        }*/
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zalba>()
                .Property(u => u.statusZalbe)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<Zalba>()
                .Property(u => u.radnjaNaOsnovuZalbe)
                .HasConversion<string>()
            .HasMaxLength(50);

        }
        public DbSet<TipZalbe> TipZalbes { get; set; }
        public DbSet<Zalba> Zalbas { get; set; }
        public DbSet<UgovorOZakupu> UgovorOZakupus { get; set; }
    }
}
