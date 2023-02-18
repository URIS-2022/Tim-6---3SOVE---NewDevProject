using PrijavaJnService.ServiceCalls.Mocks;
using System.ComponentModel.DataAnnotations;

namespace PrijavaJnService.Models.PrijavaJn
{
    /// <summary>
    /// Creation Dto za prijavuJn
    /// </summary>
    public class PrijavaJnCreationDto
    {
        /// <summary>
        /// Broj prijave
        /// </summary>
        
        [Required(ErrorMessage = "Obavezno je uneti broj prijave.")]
        public string BrojPrijave { get; set; }

        /// <summary>
        /// Datum prijave
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti datum prijave.")]
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
       
        /// <summary>
        /// ID kupca koji podnisi prijavu
        /// </summary>
        public Guid? KupacId { get; set; }

        public KupacDto Kupac { get; set; }
    }
}
