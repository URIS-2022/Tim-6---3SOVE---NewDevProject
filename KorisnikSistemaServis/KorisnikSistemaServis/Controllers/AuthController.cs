using KorisnikSistemaServis.AuthHelpers;
using KorisnikSistemaServis.Data;
using KorisnikSistemaServis.Entities;
using KorisnikSistemaServis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KorisnikSistemaServis.Controllers
{
    [ApiController]
    [Route("api/login")]
    [Produces("application/json","application/xml")]
    public class AuthController : ControllerBase
    {
        private readonly IKorisnikRepository _korisnikRepository;
        private readonly IAuthRepository _authRepository;

        public AuthController(IKorisnikRepository korisnikRepository, IAuthRepository authRepository)
        {
            this._korisnikRepository = korisnikRepository;
            this._authRepository = authRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Authenticate([FromBody]Kredencijali korisnik)
        {
            Korisnik user =  _korisnikRepository.GetKorisnikByKorisnickoIme(korisnik.KorisnickoIme);
            if(user == null)
            {
                return NotFound("Ne postoji korisnik sa datim korisnickim imenom");

            }else if (!BCrypt.Net.BCrypt.Verify(korisnik.Lozinka, user.Lozinka))
            {
                return Unauthorized("Ne poklapaju se lozinka i username.");
            }
            string uloga;
            if((int)user.TipKorisnika == 0)
            {
                uloga = "Administrator";
            } 
            else if((int)user.TipKorisnika == 1)
            {
                uloga = "Superuser";

            } else if ((int)user.TipKorisnika == 2)
            {
                uloga = "Licitant";

            } else if((int)user.TipKorisnika == 3)
            {
                uloga = "Menadzer";

            } else if((int)user.TipKorisnika == 4)
            {
                uloga = "Operater";

            } else if((int)user.TipKorisnika == 5)
            {
                uloga = "OperaterNadmetanja";

            } else if((int)user.TipKorisnika == 6)
            {
                uloga = "TehnickiSekretar";
            } else
            {
                uloga = "PrvaKomisija";
            }
            AuthToken token =  _authRepository.Authenticate(user.KorisnickoIme, user.Lozinka, uloga);
            if(token == null)
            {
                return Unauthorized("Nije generisan token");
            }
            return Ok(token);
        }
    }
}
