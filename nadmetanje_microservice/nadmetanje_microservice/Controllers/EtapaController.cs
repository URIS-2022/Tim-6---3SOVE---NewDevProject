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

        [HttpGet("getAllEtapas")]
        public async Task<ActionResult<List<EtapaDataOut>>> GetAllEtapas()
        {
            return Ok(await _etapaService.GetAllAsync());
        }

        [HttpGet("getAllEtapasByLicitacijaId/{licitacijaId}")]
        public async Task<ActionResult<List<EtapaDataOut>>> GetAllEtapasByLicitacijaId(Guid licitacijaId)
        {
            return Ok(await _etapaService.GetAllByLicitacijaIdAsync(licitacijaId));
        }

        [HttpPost("save")]
        public async Task<ActionResult<List<EtapaDataOut>>> Save([FromBody] EtapaSaveDataIn dataIn)
        {
            return Ok(await _etapaService.Save(dataIn));
        }

        [HttpDelete("getEtapaById/{etapaId}")]
        public async Task<ActionResult<List<EtapaDataOut>>> getEtapaById(Guid etapaId)
        {
            return Ok(await _etapaService.GetByIdAsync(etapaId));
        }

        [HttpDelete("remove/{etapaId}")]
        public async Task<ActionResult<List<EtapaDataOut>>> Remove(Guid etapaId)
        {
            return Ok(await _etapaService.Remove(etapaId));
        }
    }
}
