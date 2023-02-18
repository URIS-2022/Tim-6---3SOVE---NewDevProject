using KorisnikSistemaServis.Models;

namespace KorisnikSistemaServis.Entities
{
    public class KorisnikConfirmation
    {
        public Guid KorisnikId { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public TipoviKorisnika TipKorisnika { get; set; }
    }
}
