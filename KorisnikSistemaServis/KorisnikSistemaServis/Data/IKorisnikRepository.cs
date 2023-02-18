using KorisnikSistemaServis.Entities;

namespace KorisnikSistemaServis.Data
{
    public interface IKorisnikRepository
    {
        List<Korisnik> GetAllKorisnik(string? korisnickoIme = null);
        Korisnik GetKorisnikById(Guid korisnikId);
        Korisnik GetKorisnikByKorisnickoIme(string korisnickoIme);
        KorisnikConfirmation CreateKorisnik(Korisnik korisnik);
        void UpdateKorisnik(Korisnik korisnik);
        void DeleteKorisnik(Guid korisnikId);
        bool SaveChanges();
    }
}
