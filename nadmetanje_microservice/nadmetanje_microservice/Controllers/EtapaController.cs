using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataOut;
using nadmetanje_microserviceBLL.Services.Interfaces;

namespace nadmetanje_microserviceWebApp.Controllers
{
    public class EtapaController : BaseController
    {
        private readonly IEtapaService _etapaService;
        public EtapaController(IEtapaService etapaService)
        {
            _etapaService= etapaService;
        }

        /// <summary>
        /// Vraća sve etape.
        /// </summary>
        /// <returns>Lista etapa</returns>
        /// <response code="200">Vraća listu etapa</response>
        /// <response code="404">Nije pronađeno nijedna etapa</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getAllEtapas")]
        public async Task<ActionResult<List<EtapaDataOut>>> GetAllEtapas()
        {
            return Ok(await _etapaService.GetAllAsync());
        }

        /// <summary>
        /// Vraća sve etape na osnovu licitacija idija.
        /// </summary>
        /// <param name="licitacijaId">ID licitacije</param>
        /// <returns>Lista etapa</returns>
        /// <response code="200">Vraća listu etapa sadrzanih na odredjenoj licitaciji</response>
        /// <response code="404">Nije pronađeno nijedno etapa na specificiranoj licitaciji</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getAllEtapasByLicitacijaId/{licitacijaId}")]
        public async Task<ActionResult<List<EtapaDataOut>>> GetAllEtapasByLicitacijaId(Guid licitacijaId)
        {
            return Ok(await _etapaService.GetAllByLicitacijaIdAsync(licitacijaId));
        }

        /// <summary>
        /// Kreira novu etapu ili izmjenjuje staru u zavisnosti da li je prosledjen id etape.
        /// </summary>
        /// <param name="dataIn">Ulazni model etape</param>
        /// <returns>Potvrdu o kreiranoj ili izmjenjenoj etapi.</returns>
        /// <remarks>
        /// Primer zahtjeva za kreiranje nove etape\
        /// POST /api/etapa/save \
        /// {\
        ///"id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        ///"licitacijaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        ///"datum": "2023-02-18T00:02:22.245Z",\
        ///"vremePocetka": "00:00:00",\
        ///"vremeZavrsetka": "00:00:00"\
        /// }\
        /// </remarks>
        /// <response code="200">Vraća poruku o uspjesnom kreiranju/izmjeni etape.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja etape.</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("save")]
        public async Task<ActionResult<List<EtapaDataOut>>> Save([FromBody] EtapaSaveDataIn dataIn)
        {
            return Ok(await _etapaService.Save(dataIn));
        }

        /// <summary>
        /// Vraća jednu etapu na osnovu ID-ja etape.
        /// </summary>
        /// <param name="etapaId">ID etape</param>
        /// <returns>etapu</returns>
        /// <response code="200">Vraća traženu etapu</response>
        /// <response code="404">Nije pronađena nijedna etapa sa specificiranim IDijem</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getEtapaById/{etapaId}")]
        public async Task<ActionResult<List<EtapaDataOut>>> GetEtapaById(Guid etapaId)
        {
            return Ok(await _etapaService.GetByIdAsync(etapaId));
        }

        /// <summary>
        /// Vrši brisanje jedne etape sistema na osnovu ID-ja etape.
        /// </summary>
        /// <param name="etapaId">ID etape</param>
        /// <returns>Poruka o uspjesnom brisanju etape ili o gresci.</returns>
        /// <response code="204">Etapa uspješno obrisana</response>
        /// <response code="404">Nije pronađena etapa za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja etape</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpDelete("remove/{etapaId}")]
        public async Task<ActionResult<List<EtapaDataOut>>> Remove(Guid etapaId)
        {
            return Ok(await _etapaService.Remove(etapaId));
        }
    }
}
