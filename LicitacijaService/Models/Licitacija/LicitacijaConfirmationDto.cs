namespace LicitacijaService.Models.Licitacija
{
    /// <summary>
    /// Confirmation Dto za licitaciju
    /// </summary>
    public class LicitacijaConfirmationDto
    {
        /// <summary>
        /// Broj licitacije
        /// </summary>
        public int BrojLicitacije { get; set; }

        /// <summary>
        /// Godina licitacije
        /// </summary>
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
        public int KorakCeneLicitacije { get; set; }
    }
}
