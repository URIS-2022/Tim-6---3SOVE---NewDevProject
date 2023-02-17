using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MikroservisKomsija.Services.KomisijaSerive;
using MikroservisKomsija.Models;

namespace MikroservisKomsija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("aplication/json", "aplication/xml")]
    public class KomisijaController : ControllerBase
    {
        
        private readonly IKomisija _komisijaR;
        public KomisijaController(IKomisija komisijaR)
        {
            _komisijaR = komisijaR;
        }
        /// <summary>
        /// Vraca sve komisije
        /// </summary>
        /// <returns> Lista komisija</returns>
        /// <response code="200">Vraca listu komsija</response>
        /// <response code="404">Nije pronadjena ni jedan komisija</response>
        [Authorize(Roles = "Superuser,Administrator,Menadzer")]
        [HttpGet]
        public async Task<ActionResult<List<Komisija>>> GetAllKoms()
        {
            return await _komisijaR.GetAllKoms();
        }
        /// <summary>
        /// Vraca  komsiju za proslijedjeni ID
        /// </summary>
        /// <returns> Lista komisija</returns>
        /// <response code="200">Vraca komsiju</response>
        /// <response code="404">Nije pronadjena ni jedan komisija</response>
        [Authorize(Roles = "Superuser,Administrator,Menadzer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Komisija>> GetSingleKom(Guid id)
        {
            var result = await _komisijaR.GetSingleKom(id);
            if (result is null)
                return NotFound("Komisija nije pronadjena");
            return Ok(result);
        }
        /// <summary>
        /// Kreira nove komisije
        /// </summary>
        /// <remarks>
        /// Primjer zahtjeva za kreiranje nove komisije \
        /// POST /api/Komisija \
        ///   "idKomsije": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///  "imeKomisije": "string",
        /// "ovlascenje": "string",
        /// "oznakaKomisije": "string"
        /// </remarks>
        /// <returns>Potvrda o kreiranju komisije</returns>
        /// <response code="201">Vraća kreiranu komisiju</response>
        /// <response code="500">Desila se greška prilikom unosa nove komisije</response>
        [Authorize(Roles = "Superuser,Administrator")]
        [HttpPost]
        public async Task<ActionResult<List<Komisija>>> AddKom(Komisija komisija)
        {
            var result = await _komisijaR.AddKom(komisija);
            return Ok(result);
        }
        /// <summary>
        /// Izmjena komisije
        /// </summary>
        /// <returns>Potvrda o izmjeni  komisije</returns>
        /// <response code="200">Izmijenjena komisija</response>
        /// <response code="404">Nije pronađena komisija za unjeti ID</response>
        /// <response code="500">Serverska greška tokom izmjene</response>
        [Authorize(Roles = "Superuser,Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Komisija>>> UpdateKom(Guid id, Komisija request)
        {
            var result = await _komisijaR.UpdateKom(id, request);
            if (result is null)
                return NotFound("Komisija nije pronadjena");
            return Ok(result);
        }
        /// <summary>
        /// Brisanje komisije na osnovu ID-a
        /// </summary>
        /// <param name="id">ID komisije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Komisija je uspješno obrisana</response>
        /// <response code="404">Nije pronađena komisja za unjeti ID</response>
        /// <response code="500">Serverska greška tokom brisanja </response>
        [Authorize(Roles = "Superuser,Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Komisija>>> DeleteKom(Guid id)
        {
            var result = await _komisijaR.DeleteKom(id);
            if (result is null)
                return NotFound("Komisija nije pronadjena");
            return Ok(result);
        }
    }
}
