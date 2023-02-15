using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikroservisKomsija.Services.KomisijaClanService;

namespace MikroservisKomsija.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Superuser")]
    [Route("api/[controller]")]
    [ApiController]
    public class KomisijaClanController : ControllerBase
    {
        private readonly IKomisijaClan _kcR;
        public KomisijaClanController(IKomisijaClan kcR)
        {
            _kcR = kcR;
        }

        [HttpGet]
        [Authorize(Roles = "Menadzer")]
        public async Task<ActionResult<List<KomisijaClan>>> GetAllKCs()
        {
            return await _kcR.GetAllKCs();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Menadzer")]
        public async Task<ActionResult<KomisijaClan>> GetSingleKC(Guid IDKomsije, int IDClan)
        {
            var result = await _kcR.GetSingleKC(IDKomsije, IDClan);
            if (result is null)
                return NotFound("Nije pronadjen");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<KomisijaClan>>> AddKC(KomisijaClan komisijaclan)
        {
            var result = await _kcR.AddKC(komisijaclan);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<KomisijaClan>>> UpdateKC(Guid IDKomsije, int IDClan, KomisijaClan request)
        {
            var result = await _kcR.UpdateKC(IDKomsije, IDClan, request);
            if (result is null)
                return NotFound("Nije pronadjen");
            return Ok(result);
        }

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
