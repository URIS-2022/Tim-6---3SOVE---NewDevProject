namespace ms_parcela.Models.ParcelaModel
{
    public enum KulturaConfirmDto { njive, vrtovi, vocnjaci, vinogradi, livade, pasnjaci, sume, trstici }
    public enum ObradivostConfirmDto { obradivo, ostalo }
    public class ParcelaConfirmationDto
    {
        public int povrsina { get; set; }
        public KulturaConfirmDto kultura { get; set; }
        public ObradivostConfirmDto obradivost { get; set; }
    }
}
