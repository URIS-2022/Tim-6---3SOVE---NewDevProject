namespace ms_parcela.Models.DeoParceleModel
{
    public class DeoParceleCreationDto
    {
        public int redniBrojDelaParcele { get; set; }
        public int povrsinaDelaParcele { get; set; }
        public Guid brojKatastraskaOpstina { get; set; }
        public int brojParcele { get; set; }
        public Guid NadmetanjeId { get; set; }
    }
}
