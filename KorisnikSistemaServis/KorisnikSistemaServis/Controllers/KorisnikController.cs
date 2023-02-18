using AutoMapper;
using KorisnikSistemaServis.Data;
using KorisnikSistemaServis.Entities;
using KorisnikSistemaServis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace KorisnikSistemaServis.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/korisnici")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KorisnikController(IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.korisnikRepository = korisnikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }
        /// <summary>
        /// Vraća sve korisnike (i za zadati filter korisnickoIme).
        /// </summary>
        /// <param name="korisnickoIme">korisnickoIme = "sanja123"</param>
        /// <returns>Lista korisnika</returns>
        /// <response code="200">Vraća listu korisnika</response>
        /// <response code="404">Nije pronađen nijedan korisnik</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KorisnikDto>> GetAllKorisnik(string korisnickoIme = null)
        {
            var korisnici = korisnikRepository.GetAllKorisnik(korisnickoIme);
            if (korisnici == null || korisnici.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KorisnikDto>>(korisnici));
        }
        /// <summary>
        /// Vraća jednog korisnika na osnovu ID-ja korisnika.
        /// </summary>
        /// <param name="korisnikId">ID korisnika</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženog korisnika</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{korisnikId}")]
        public ActionResult<KorisnikDto> GetKorisnikById(Guid korisnikId)
        {
            var user = korisnikRepository.GetKorisnikById(korisnikId);
            if (user == null)
            {
                return NotFound("Korisnik sa proslijeđenim id-em nije pronađen.");
            }
            return Ok(mapper.Map<KorisnikDto>(user));
        }
        /// <summary>
        /// Kreira novog korisnika.
        /// </summary>
        /// <param name="korisnik">Model korisnika</param>
        /// <returns>Potvrdu o kreiranom korisniku.</returns>
        /// <remarks>
        /// Primer zahtjeva za kreiranje novog korisnika \
        /// POST /api/korisnici \
        /// {     \
        ///     "ime":"Sanja", \
        ///     "prezime": "Tica", \
        ///     "korisnickoIme": "sanja123", \
        ///     "lozinka": "test123", \
        ///     "tipKorisnika": 0 (administrator), \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiranog korisnika</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja korisnika</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KorisnikConfirmationDtocs> CreateKorisnik([FromBody] KorisnikCreationDto korisnik)
        {
            try
            {
                string? lozinka = korisnik.Lozinka;
                string lozinka2 = BCrypt.Net.BCrypt.HashPassword(lozinka);
                korisnik.Lozinka = lozinka2;
                Korisnik user = mapper.Map<Korisnik>(korisnik);
                KorisnikConfirmation confirmation = korisnikRepository.CreateKorisnik(user);
                korisnikRepository.SaveChanges();
                string? location = linkGenerator.GetPathByAction("GetKorisnikById", "Korisnik", new { korisnikId = confirmation.KorisnikId });
                return Created(location,user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }
        /// <summary>
        /// Vrši brisanje jednog korisnika sistema na osnovu ID-ja korisnika.
        /// </summary>
        /// <param name="korisnikId">ID korisnika</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Korisnik uspješno obrisan</response>
        /// <response code="404">Nije pronađen korisnik za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja korisnika</response>
        [HttpDelete("{korisnikId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKorisnik(Guid korisnikId)
        {
            try
            {
                var korisnikModel = korisnikRepository.GetKorisnikById(korisnikId);
                if (korisnikModel == null)
                {
                    return NotFound("Korisnik sa proslijeđenim id-em nije pronađen.");
                }
                korisnikRepository.DeleteKorisnik(korisnikId);
                korisnikRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting user.");
            }
        }
        /// <summary>
        /// Ažurira jednog korisnika.
        /// </summary>
        /// <param name="korisnik">Model korisnika koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj prijavi.</returns>
        /// <response code="200">Vraća ažuriranog korisnika</response>
        /// <response code="400">Korisnik koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja korisnika</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KorisnikDto> UpdateKorisnik(KorisnikUpdateDto korisnik)
        {
            try
            {
            var oldUser = korisnikRepository.GetKorisnikById(korisnik.KorisnikId);
            if (oldUser == null)
            {
                return NotFound("Korisnik sa proslijeđenim id-em nije pronađen.");
            }
            if (!String.Equals(oldUser.Lozinka, korisnik.Lozinka))
            {
                korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(korisnik.Lozinka);
            }
            string? lozinka = korisnik.Lozinka;
            string novaLozinka = BCrypt.Net.BCrypt.HashPassword(lozinka);
            Korisnik korisnikEntity = mapper.Map<Korisnik>(korisnik);
            mapper.Map(korisnikEntity, oldUser);
            korisnikRepository.SaveChanges();
            return Ok(mapper.Map<KorisnikDto>(oldUser));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        /// <summary>
        /// Vraća opcije za rad sa korisnicima.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetKorisnikOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
