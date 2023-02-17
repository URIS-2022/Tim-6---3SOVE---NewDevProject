using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicitacijaService.Entities
{
    /// <summary>
    /// Predstavlja licitaciju
    /// </summary>
    public class Licitacija
    {
        /// <summary>
        /// ID licitacije
        /// </summary>
        [Key]
        public Guid LicitacijaId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Broj licitacije
        /// </summary>
        [Required]
        public int BrojLicitacije { get; set; }

        /// <summary>
        /// Godina licitacije
        /// </summary>

        [Required]
        public int GodinaLicitacije { get; set; }

        /// <summary>
        /// Ogranicenje licitacije
        /// </summary>
        public int OgranicenjeLicitacije { get; set; }

        /// <summary>
        /// Rok licitacije
        /// </summary>
        public DateTime RokLicitacije { get; set; }

        /// <summary>
        /// Korak cene licitacije
        /// </summary>

        [Required]
        public int KorakCeneLicitacije { get; set; }

        //[ForeignKey("ProgramEntitet")]
        //public Guid ProgramId { get; set; }

        /// <summary>
        /// ID programa - strani kljuc 
        /// </summary>
        public ProgramEntitet ProgramEntitet { get; set; }
    }
}
