using Microsoft.AspNetCore.Mvc;
using nadmetanje_microserviceBLL.Common;
using nadmetanje_microserviceBLL.DTOs.Etapa;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataOut;
using nadmetanje_microserviceBLL.Services.Interfaces;
using nadmetanje_microserviceDLL.Model;

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

        [HttpPost("setTipNadmetanjaDefault/{TipNadmetanja}")]
        public ActionResult<ResponsePackageNoData> SetTipNadmetanjaDefault(TipNadmetanja? TipNadmetanja)
        {
            return Ok(_nadmetanjeService.SetTipNadmetanjaDefault(TipNadmetanja));
        }
        
        [HttpPost("setCenaPoHektaruNadmetanjaDefault/{dataIn}")]
        public ActionResult<ResponsePackageNoData> SetCenaPoHektaruNadmetanjaDefault(double? dataIn)
        {
            return Ok(_nadmetanjeService.SetCenaPoHektaruNadmetanjaDefault(dataIn));
        }
        
        [HttpPost("setDuzinaZakupaNadmetanjaDefault/{dataIn}")]
        public ActionResult<ResponsePackageNoData> SetDuzinaZakupaNadmetanjaDefault(int? dataIn)
        {
            return Ok(_nadmetanjeService.SetDuzinaZakupaNadmetanjaDefault(dataIn));
        }

        [HttpPost("setVrednostJavnogNadmetanja")]
        public async Task<ActionResult<ResponsePackageNoData>> SetVrednostJavnogNadmetanja([FromBody]VrednostJavnogNadmetanjaDataIn dataIn)
        {
            return Ok(await _nadmetanjeService.SetVrednostJavnogNadmetanja(dataIn));
        }

        [HttpGet("getVrednostJavnogNadmetanja/{id}")]
        public async Task<ActionResult<ResponsePackage<double>>> GetVrednostJavnogNadmetanja(Guid id)
        {
            return Ok(await _nadmetanjeService.GetVrednostJavnogNadmetanja(id));
        }

        [HttpPost("CreateEtapaAndConnectToNadmetanja")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> CreateEtapaAndConnectToNadmetanja([FromBody] CreateEtapaAndConnectToNadmetanjaDataIn dataIn)
        {
            return Ok(await _nadmetanjeService.CreateEtapaAndConnectToNadmetanja(dataIn));
        }

        [HttpPost("setStatusNadmetanja")]
        public async Task<ActionResult<ResponsePackageNoData>> SetStatusNadmetanja([FromBody] SetTipNadmetanjaDataIn<StatusNadmetanja> dataIn)
        {
            return Ok(await _nadmetanjeService.SetStatusNadmetanja(dataIn));
        }

        [HttpPost("setStatusDrugiKrugNadmetanja")]
        public async Task<ActionResult<ResponsePackageNoData>> SetStatusDrugiKrugNadmetanja([FromBody] SetTipNadmetanjaDataIn<StatusDrugiKrug> dataIn)
        {
            return Ok(await _nadmetanjeService.SetStatusDrugiKrugNadmetanja(dataIn));
        }

        [HttpPost("setKrugNadmetanja")]
        public async Task<ActionResult<ResponsePackageNoData>> SetKrugNadmetanja([FromBody] SetTipNadmetanjaDataIn<KrugNadmetanja> dataIn)
        {
            return Ok(await _nadmetanjeService.SetKrugNadmetanja(dataIn));
        }

        [HttpGet("getAllByStatusNadmetanja/{dataIn}")]
        public async Task<ActionResult<ResponsePackage<List<Nadmetanje>>>> GetAllByStatusNadmetanja(StatusNadmetanja dataIn)
        {
            return Ok(await _nadmetanjeService.GetAllByStatusNadmetanja(dataIn));
        }

        [HttpGet("getAllByStatusDrugiKrugAsync/{dataIn}")]
        public async Task<ActionResult<ResponsePackage<List<Nadmetanje>>>> GetAllByStatusDrugiKrugAsync(StatusDrugiKrug dataIn)
        {
            return Ok(await _nadmetanjeService.GetAllByStatusDrugiKrugAsync(dataIn));
        }

        [HttpGet("getAllByKrugNadmetanjaAsync/{dataIn}")]
        public async Task<ActionResult<ResponsePackage<List<Nadmetanje>>>> GetAllByStatusDrugiKrugAsync(KrugNadmetanja dataIn)
        {
            return Ok(await _nadmetanjeService.GetAllByKrugNadmetanjaAsync(dataIn));
        }

        public async Task<ActionResult<ResponsePackageNoData>> PokretanjeDrugogKruga()
        {
            return Ok(await _nadmetanjeService.PokretanjeDrugogKruga());
        }
    }
}
