using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikroservisKomsija.Services.KomisijaClanService;
using MikroservisKomsija.Models;

namespace MikroservisKomsija.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces("aplication/json", "aplication/xml")]
    public class KomisijaClanController : ControllerBase
    {
        private readonly IKomisijaClan _kcR;
        public KomisijaClanController(IKomisijaClan kcR)
        {
            _kcR = kcR;
        }
        /// <summary>
        /// Vraca sve komsijije i clanove
        /// </summary>
        /// <returns> Lista clanova</returns>
        /// <response code="200">Vraca listu </response>
        /// <response code="404">Nije pronadjeno nista</response>
        [Authorize(Roles = "Superuser,Administrator,Menadzer")]
        [HttpGet]
        public async Task<ActionResult<List<KomisijaClan>>> GetAllKCs()
        {
            return await _kcR.GetAllKCs();
        }
        /// <summary>
        /// Vraca  clana i komisiju za proslijedjeni ID
        /// </summary>
        /// <returns> Lista clanova i komisija</returns>
        /// <response code="200">Vraca clana i komisiju</response>
        /// <response code="404">Nije pronadjena ni jedan clan i komisija</response>
        [Authorize(Roles = "Superuser,Administrator,Menadzer")]
        [HttpGet("{id}")]
      
        public async Task<ActionResult<KomisijaClan>> GetSingleKC(Guid IDKomsije, int IDClan)
        {
            var result = await _kcR.GetSingleKC(IDKomsije, IDClan);
            if (result is null)
                return NotFound("Nije pronadjen");
            return Ok(result);
        }
        /// <summary>
        /// Kreira novog clana i komsije
        /// </summary>
        /// <returns>Potvrda o kreiranju </returns>
        /// <response code="201">Vraća kreiranog clana i komisije</response>
        /// <response code="500">Desila se greška prilikom unosa </response>
        [Authorize(Roles = "Superuser,Administrator")]
        [HttpPost]
        public async Task<ActionResult<List<KomisijaClan>>> AddKC(KomisijaClan komisijaclan)
        {
            var result = await _kcR.AddKC(komisijaclan);
            return Ok(result);
        }
        /// <summary>
        /// Izmjena clana i komisije
        /// </summary>
        /// <returns>Potvrda o izmjeni</returns>
        /// <response code="200">Izmijenjeno</response>
        /// <response code="404">Nije pronađen nista sa zadatim  ID</response>
        /// <response code="500">Serverska greška tokom izmjene</response>
        [Authorize(Roles = "Superuser,Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<List<KomisijaClan>>> UpdateKC(Guid IDKomsije, int IDClan, KomisijaClan request)
        {
            var result = await _kcR.UpdateKC(IDKomsije, IDClan, request);
            if (result is null)
                return NotFound("Nije pronadjen");
            return Ok(result);
        }
        /// <summary>
        /// Brisanje clana i komisije na osnovu ID-ijeva
        /// </summary>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204"> Uspješno obrisano</response>
        /// <response code="404">Nije pronađen clan i komisija za prosledjen ID</response>
        /// <response code="500">Serverska greška tokom brisanja </response>
        [Authorize(Roles = "Superuser,Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<KomisijaClan>>> DeleteKC(Guid IDKomsije, int IDClan)
        {
            var result = await _kcR.DeleteKC(IDKomsije, IDClan);
            if (result is null)
                return NotFound("Nije pronadjen");
            return Ok(result);
        }
    }
}
