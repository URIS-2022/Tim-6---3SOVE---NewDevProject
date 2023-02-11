using Microsoft.AspNetCore.Mvc;
using nadmetanje_microserviceBLL.DTOs.Etapa;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataOut;
using nadmetanje_microserviceBLL.Services.Interfaces;

namespace nadmetanje_microserviceWebApp.Controllers
{
    public class NadmetanjeController : BaseController
    {
        private readonly INadmetanjeService _nadmetanjeService;
        public NadmetanjeController(INadmetanjeService nadmetanjeService)
        {
            _nadmetanjeService = nadmetanjeService;
        }

        [HttpGet("getAllNadmetanja")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> GetAllNadmetanjes()
        {
            return Ok(await _nadmetanjeService.GetAllAsync());
        }

        [HttpGet("getAllNadmetanjaByEtapaId/{etapaId}")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> GetAllNadmetanjaByEtapaId(Guid etapaId)
        {
            return Ok(await _nadmetanjeService.GetAllByEtapaIdAsync(etapaId));
        }

        [HttpPost("save")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> Save([FromBody] NadmetanjeDataIn dataIn)
        {
            return Ok(await _nadmetanjeService.Save(dataIn));
        }

        [HttpDelete("getNadmetanjeById/{NadmetanjeId}")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> GetNadmetanjeById(Guid NadmetanjeId)
        {
            return Ok(await _nadmetanjeService.GetByIdAsync(NadmetanjeId));
        }

        [HttpDelete("remove/{NadmetanjeId}")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> Remove(Guid NadmetanjeId)
        {
            return Ok(await _nadmetanjeService.Remove(NadmetanjeId));
        }
        
        [HttpGet("getTipoviForOptions")]
        public ActionResult<List<DictionaryItem<string>>> GetTipoviForOptions()
        {
            return Ok(_nadmetanjeService.GetTipoviForOptions());
        }
        
        [HttpGet("getStatusiForOptions")]
        public ActionResult<List<DictionaryItem<string>>> GetStatusiForOptions()
        {
            return Ok(_nadmetanjeService.GetStatusiForOptions());
        }

        [HttpGet("getKrugForOptions")]
        public ActionResult<List<DictionaryItem<string>>> GetKrugForOptions()
        {
            return Ok(_nadmetanjeService.GetKrugForOptions());
        }

        [HttpGet("getStatusiDrugiKrugForOptions")]
        public ActionResult<List<DictionaryItem<string>>> GetStatusiDrugiKrugForOptions()
        {
            return Ok(_nadmetanjeService.GetStatusiDrugiKrugForOptions());
        }

    }
}
