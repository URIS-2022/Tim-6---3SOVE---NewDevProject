using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikroservisPrijavaNaLicitaciju.Services.PrijavaNaLicitacijuService;

namespace MikroservisPrijavaNaLicitaciju.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrijavaNaLicitacijuController : ControllerBase
    {
        
        private readonly IPrijavaNaLicitaciju _plicR;
       
        public PrijavaNaLicitacijuController(IPrijavaNaLicitaciju plicR)
        {
            _plicR = plicR;
        }
        /// <summary>
        /// Vraca sve prijave na licitaciju
        /// </summary>
        /// <returns> Lista prijava na licitaciju</returns>
        /// <response code="200">Vraca listu prijava na licitaciju</response>
        /// <response code="404">Nije pronadjena ni jedna prijava na licitaciju</response>

        [Authorize(Roles = "Superuser,Administrator,OperaterNadmetanja,Menadzer")]
        [HttpGet]

        public async Task<ActionResult<List<PrijavaNaLicitaciju>>> GetAllPLics()
        {
            return await _plicR.GetAllPLics();
        }

        /// <summary>
        /// Vraca  prijav na licitaciju za proslijedjeni ID
        /// </summary>
        /// <returns> Lista prijava na licitaciju</returns>
        /// <response code="200">Vraca  prijavu na licitaciju</response>
        /// <response code="404">Nije pronadjena ni jedna prijava na licitaciju</response>

        [Authorize(Roles = "Superuser,Administrator,OperaterNadmetanja,Menadzer")]
        [HttpGet("{id}")]

        public async Task<ActionResult<PrijavaNaLicitaciju>> GetSinglePLic(Guid id)
        {
            var result = await _plicR.GetSinglePLic(id);
            if (result is null)
                return NotFound("Prijava nije pronadjen");
            return Ok(result);
        }
        /// <summary>
        /// Kreira novu prijavu na licitaciju
        /// </summary>
        /// <remarks>
        /// Primjer zahtjeva za kreiranje nove prijave na licitacije \
        /// POST /api/PrijavaNaLicitaciju \
        ///   "idPlic": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///    "datumPrijave": "string",
        ///    "tipPrijave": "string",
        ///   "iznosDepozita": "string"
        /// </remarks>
        /// <returns>Potvrda o kreiranju prijave na licitacije</returns>
        /// <response code="201">Vraća kreiranu prijavu na licitaciju</response>
        /// <response code="500">Desila se greška prilikom unosa nove licitacije</response>

        [Authorize(Roles = "Superuser,Administrator,OperaterNadmetanja,Operater")]
        [HttpPost]

        public async Task<ActionResult<List<PrijavaNaLicitaciju>>> AddPLic(PrijavaNaLicitaciju prijavanalicitaciju)
        {
            var result = await _plicR.AddPLic(prijavanalicitaciju);
            return Ok(result);
        }
        /// <summary>
        /// Izmjena prijave na licitacije
        /// </summary>
        /// <returns>Potvrda o izmjeni  prijavi na licitacije</returns>
        /// <response code="200">Izmijenjena prijava na licitaciju</response>
        /// <response code="404">Nije pronađena prijava na licitacija za unjeti ID</response>
        /// <response code="500">Serverska greška tokom izmene licitacije</response>


        [Authorize(Roles = "Superuser,Administrator,OperaterNadmetanja")]
        [HttpPut("{id}")]
        public async Task<ActionResult<List<PrijavaNaLicitaciju>>> UpdatePLic(Guid id, PrijavaNaLicitaciju request)
        {
            var result = await _plicR.UpdatePLic(id, request);
            if (result is null)
                return NotFound("Prijava nije pronadjen");
            return Ok(result);
        }
        /// <summary>
        /// Brisanje prijave na licitaciju na osnovu ID-a
        /// </summary>
        /// <param name="id">ID prijave na licitacije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Prijava na icitaciju je uspješno obrisana</response>
        /// <response code="404">Nije pronađena prijava na licitaciju za unjeti ID</response>
        /// <response code="500">Serverska greška tokom brisanja </response>


        [Authorize(Roles = "Superuser,Administrator,OperaterNadmetanja")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PrijavaNaLicitaciju>>> DeletePLic(Guid id)
        {
            var result = await _plicR.DeletePLic(id);
            if (result is null)
                return NotFound("Prijava nije pronadjen");
            return Ok(result);
        }
    }
}
