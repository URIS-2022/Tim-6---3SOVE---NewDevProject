using KorisnikSistemaServis.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KorisnikSistemaServis.Entities
{
    public class Korisnik
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
