namespace PrijavaJnService.Entities.Confirmations
{
    public class PrijavaJnConfirmation
    {
        public Guid PrijavaId { get; set; }
        public string BrojPrijave { get; set; }
        public DateTime DatumPrijave { get; set; }
        public string MestoPrijave { get; set; }
    }
}
