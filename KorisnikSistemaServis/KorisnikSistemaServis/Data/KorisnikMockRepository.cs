using KorisnikSistemaServis.Entities;
using KorisnikSistemaServis.Models;

namespace KorisnikSistemaServis.Data
{
    public class KorisnikMockRepository
    {
        public static List<Korisnik> Korisnici { get; set; } = new List<Korisnik>();

        public KorisnikMockRepository()
        {
            FillData();
        }

        public void FillData()
        {
            Korisnici.AddRange(new List<Korisnik>
            {
                new Korisnik
                {
                    KorisnikId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Ime = "Sanja",
                    Prezime = "Tica",
                    KorisnickoIme = "sanjat",
                    Lozinka = "test123",
                    TipKorisnika = TipoviKorisnika.Administrator
                },
                new Korisnik
                {
                    KorisnikId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    Ime = "Marko",
                    Prezime = "Markovic",
                    KorisnickoIme = "markomar",
                    Lozinka = "marko",
                    TipKorisnika = TipoviKorisnika.Superuser
                }
            });

        }

        public KorisnikConfirmation CreateKorisnik(Korisnik korisnik)
        {
            korisnik.KorisnikId = Guid.NewGuid();
            Korisnici.Add(korisnik);
            Korisnik user = GetKorisnikById(korisnik.KorisnikId);
            return new KorisnikConfirmation
            {
                KorisnikId = user.KorisnikId,
                Ime = user.Ime,
                Prezime = user.Prezime,
                TipKorisnika = user.TipKorisnika
            };
        }

        public void DeleteKorisnik(Guid korisnikId)
        {
            Korisnici.Remove(Korisnici.FirstOrDefault(k => k.KorisnikId == korisnikId));
        }

        public Korisnik GetKorisnikById(Guid korisnikId)
        {
            return Korisnici.FirstOrDefault(k => k.KorisnikId == korisnikId);
        }

        public List<Korisnik> GetAllKorisnik(string korisnickoIme = null)
        {
            return (from k in Korisnici
                    where string.IsNullOrEmpty(korisnickoIme) || k.KorisnickoIme == korisnickoIme
                    select k).ToList();
        }

        public void UpdateKorisnik(Korisnik korisnik)
        {
            Korisnik user = GetKorisnikById(korisnik.KorisnikId);

            user.KorisnikId = korisnik.KorisnikId;
            user.Ime = korisnik.Ime;
            user.Prezime = korisnik.Prezime;
            user.KorisnickoIme = korisnik.KorisnickoIme;
            user.Lozinka = korisnik.Lozinka;
            user.TipKorisnika = korisnik.TipKorisnika;
        }
        public bool SaveChanges()
        {
            return true;
        }
    }
}
