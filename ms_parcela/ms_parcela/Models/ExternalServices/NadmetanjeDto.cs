namespace ms_parcela.Models.ExternalServices
{
    public class NadmetanjeDto
    {
        public Guid Id { get; set; }
        public int Tip { get; set; }
        public int Status { get; set; }
        public double CenaPoHektaru { get; set; }
        public int DuzinaZakupa { get; set; }
        public string? RedniBroj { get; set; }
        public Guid? EtapaId { get; set; }
        public int KrugNadmetanja { get; set; }
        public int? StatusDrugiKrug { get; set; }

        /*public static implicit operator Guid(NadmetanjeDto v)
        {
            throw new NotImplementedException();
        }*/
    }
}
