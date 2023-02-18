using PrijavaJnService.ServiceCalls.Mocks;

namespace PrijavaJnService.Models.PrijavaJn
{
    /// <summary>
    /// Dto za prijavuJn
    /// </summary>
    public class PrijavaJnDto
    {
        /// <summary>
        /// Id prijaveJn
        /// </summary>
        public Guid PrijavaId { get; set; }

        /// <summary>
        /// Broj prijave
        /// </summary>
        public string BrojPrijave { get; set; }

        /// <summary>
        /// Datum prijave
        /// </summary>
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

        public KupacDto Kupac { get; set; }
    }
}
