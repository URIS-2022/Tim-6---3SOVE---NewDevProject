namespace LicitacijaService.Entities.Confirmations
{
    public class LicitacijaConfirmation
    {
        public Guid LicitacijaId { get; set; }
        public int BrojLicitacije { get; set; }
        public int GodinaLicitacije { get; set; }
        public DateTime DatumLicitacije { get; set; }
        public int OgranicenjeLicitacije { get; set; }
        public DateTime RokLicitacije { get; set; }
        public int KorakCeneLicitacije { get; set; }

    }
}
