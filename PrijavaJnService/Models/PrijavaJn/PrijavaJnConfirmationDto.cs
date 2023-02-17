namespace PrijavaJnService.Models.PrijavaJn
{
    /// <summary>
    /// Confirmation Dto za prijavuJn
    /// </summary>
    public class PrijavaJnConfirmationDto
    {
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
    }
}
