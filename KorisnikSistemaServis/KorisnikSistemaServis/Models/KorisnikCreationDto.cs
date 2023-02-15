using System.ComponentModel.DataAnnotations;

namespace KorisnikSistemaServis.Models
{
    public class KorisnikCreationDto 
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        [MaxLength(17)]
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        public TipoviKorisnika TipKorisnika { get; set; }

        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Ime == Prezime)
            {
                yield return new ValidationResult("Ime i prezime korisnika ne mogu biti jednaki.");
            }
        }*/
    }
}
