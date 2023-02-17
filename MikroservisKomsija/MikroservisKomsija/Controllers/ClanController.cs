using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MikroservisKomsija.Services.ClanService;
using MikroservisKomsija.Models;
using static MikroservisKomsija.Startup;

namespace MikroservisKomsija.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Produces("aplication/json","aplication/xml")]
    public class ClanController : ControllerBase
    { 
        private readonly IClan _clanR;
        public ClanController(IClan clanR)
        {
            _clanR = clanR;
        }
        /// <summary>
        /// Vraca sve clanove
        /// </summary>
        /// <returns> Lista clanova</returns>
        /// <response code="200">Vraca listu clanova</response>
        /// <response code="404">Nije pronadjena ni jedan clan</response>

        [Authorize(Roles = "Superuser,Administrator,Menadzer")]
        [HttpGet]
        public async Task<ActionResult<List<Clan>>> GetAllClans()
        {
            return await _clanR.GetAllClans();
        }
        /// <summary>
        /// Vraca  clana za proslijedjeni ID
        /// </summary>
        /// <returns> Lista clanova</returns>
        /// <response code="200">Vraca clana</response>
        /// <response code="404">Nije pronadjena ni jedan clan</response>

        [Authorize(Roles = "Superuser,Administrator,Menadzer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Clan>> GetSingleClan(int id)
        {
            var result = await _clanR.GetSingleClan(id);
            if (result is null)
                return NotFound("Clan nije pronadjen");
            return Ok(result);
        }
        /// <summary>
        /// Kreira novog clana
        /// </summary>
        /// <remarks>
        /// Primjer zahtjeva za kreiranje novog clana \
        /// POST /api/Clan \
        ///   "idClan": 0,
        /// "imeClana": "string",
        /// "prezimeClana": "string",
        /// "mjesto": "string",
        /// "datumRodjenja": "string"
        /// </remarks>
        /// <returns>Potvrda o kreiranju clana</returns>
        /// <response code="201">Vraća kreiranog clana</response>
        /// <response code="500">Desila se greška prilikom unosa novog clana</response>

        [HttpPost]
        [Authorize(Roles = "Superuser,Administrator")]
        public async Task<ActionResult<List<Clan>>> AddClan (Clan clan)
        {
            var result = await _clanR.AddClan(clan);
            return Ok(result);
        }
        /// <summary>
        /// Izmjena clana
        /// </summary>
        /// <returns>Potvrda o izmjeni  clana</returns>
        /// <response code="200">Izmijenjen clan</response>
        /// <response code="404">Nije pronađen clan za unjeti ID</response>
        /// <response code="500">Serverska greška tokom izmjene</response>

        [Authorize(Roles = "Superuser,Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Clan>>> UpdateClan(int id, Clan request)
        {
            var result = await _clanR.UpdateClan(id, request);
            if (result is null)
                return NotFound("Clan nije pronadjen");
            return Ok(result);
        }
        /// <summary>
        /// Brisanje clana na osnovu ID-a
        /// </summary>
        /// <param name="id">ID clana</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Clan je uspješno obrisana</response>
        /// <response code="404">Nije pronađen clan za unjeti ID</response>
        /// <response code="500">Serverska greška tokom brisanja </response>

        [Authorize(Roles = "Superuser,Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Clan>>> DeleteClan(int id)
        {
            var result= await _clanR.DeleteClan(id);
            if (result is null)
                return NotFound("Clan nije pronadjen");
            return Ok(result);
        }
    }
}
