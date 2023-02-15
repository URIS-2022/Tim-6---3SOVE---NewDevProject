namespace KorisnikSistemaServis.AuthHelpers
{
    public interface IAuthRepository
    {
        public AuthToken Authenticate(string korisnickoIme, string lozinka, string tipKorisnika);
    }
}
