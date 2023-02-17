namespace LicitacijaService.Models.Licitacija
{
    /// <summary>
    /// Dto za licitaciju
    /// </summary>
    public class LicitacijaDto
    {
        /// <summary>
        /// Id licitacije
        /// </summary>
        public Guid LicitacijaId { get; set; }

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

        /// <summary>
        /// Id programa
        /// </summary>
        public Guid ProgramEntitetProgramId { get; set; }

    }
}
