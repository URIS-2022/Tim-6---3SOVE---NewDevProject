using System.ComponentModel.DataAnnotations;

namespace KorisnikSistemaServis.Models
{
    public class KorisnikUpdateDto
    {
        public Guid KorisnikId { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        [MaxLength(17)]
        public string? KorisnickoIme { get; set; }
        public string? Lozinka { get; set; }
        public TipoviKorisnika TipKorisnika { get; set; }
    }
}
