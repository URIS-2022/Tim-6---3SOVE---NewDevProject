using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrijavaJnService.Entities
{
    /// <summary>
    /// Predstavlja prijavuJn
    /// </summary>
    public class PrijavaJn
    {
        /// <summary>
        /// ID prijaveJn
        /// </summary>
        [Key]
        public Guid PrijavaId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Broj prijave
        /// </summary>

        [Required]
        public string BrojPrijave { get; set; }

        /// <summary>
        /// Datum prijave
        /// </summary>

        [Required]
        public DateTime DatumPrijave { get; set; }

        /// <summary>
        /// Mesto prijave
        /// </summary>
        public string MestoPrijave { get; set; }

        /// <summary>
        /// Sat prijave
        /// </summary>
        public string SatPrijave { get; set; }

        /// <summary>
        /// Zatvorena ponuda
        /// </summary>
        public bool ZatvorenaPonuda { get; set; }

        /// <summary>
        /// Dokumentacija fizicka lica
        /// </summary>
        public string DokFizickaLica { get; set; }

        /// <summary>
        /// Dokumentacija pravna lica
        /// </summary>
        public string DokPravnaLica { get; set; }

        public Guid? KupacId { get; set; }
    }
}
