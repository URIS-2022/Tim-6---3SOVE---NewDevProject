using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MikroservisKomsija.Services.KomisijaSerive;

namespace MikroservisKomsija.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Superuser")]
    [Route("api/[controller]")]
    [ApiController]
    public class KomisijaController : ControllerBase
    {
        private static List<Komisija> koms = new List<Komisija>
        {
            new Komisija
            {
                ImeKomisije="xx",
                Ovlascenje="xx",
                OznakaKomisije="xx"
            },
            new Komisija
            {
                ImeKomisije="cc",
                Ovlascenje="cc",
                OznakaKomisije="cc"
            }
        };
        private readonly IKomisija _komisijaR;
        public KomisijaController(IKomisija komisijaR)
        {
            _komisijaR = komisijaR;
        }

        [HttpGet]
        [Authorize(Roles = "Menadzer")]
        public async Task<ActionResult<List<Komisija>>> GetAllKoms()
        {
            return await _komisijaR.GetAllKoms();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Menadzer")]
        public async Task<ActionResult<Komisija>> GetSingleKom(Guid id)
        {
            var result = await _komisijaR.GetSingleKom(id);
            if (result is null)
                return NotFound("Komisija nije pronadjena");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Komisija>>> AddKom(Komisija komisija)
        {
            var result = await _komisijaR.AddKom(komisija);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Komisija>>> UpdateKom(Guid id, Komisija request)
        {
            var result = await _komisijaR.UpdateKom(id, request);
            if (result is null)
                return NotFound("Komisija nije pronadjena");
            return Ok(result);
        }

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
