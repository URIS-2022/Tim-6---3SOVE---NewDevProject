namespace KorisnikSistemaServis.Models
{
    public class KorisnikConfirmationDtocs
    {
        public Guid KorisnikId { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public TipoviKorisnika TipKorisnika { get; set; }
    }
}
