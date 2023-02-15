using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikroservisPrijavaNaLicitaciju.Services.PrijavaNaLicitacijuService;

namespace MikroservisPrijavaNaLicitaciju.Controllers
{
    [Authorize(Roles ="OperaterNadmetanja")]
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Superuser")]
    [Route("api/[controller]")]
    [ApiController]
    public class PrijavaNaLicitacijuController : ControllerBase
    {
        
        private readonly IPrijavaNaLicitaciju _plicR;
       
        public PrijavaNaLicitacijuController(IPrijavaNaLicitaciju plicR)
        {
            _plicR = plicR;
        }

        [HttpGet]
        [Authorize(Roles = "Menadzer")]
        public async Task<ActionResult<List<PrijavaNaLicitaciju>>> GetAllPLics()
        {
            return await _plicR.GetAllPLics();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Menadzer")]
        public async Task<ActionResult<PrijavaNaLicitaciju>> GetSinglePLic(Guid id)
        {
            var result = await _plicR.GetSinglePLic(id);
            if (result is null)
                return NotFound("Prijava nije pronadjen");
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Operater")]
        public async Task<ActionResult<List<PrijavaNaLicitaciju>>> AddPLic(PrijavaNaLicitaciju prijavanalicitaciju)
        {
            var result = await _plicR.AddPLic(prijavanalicitaciju);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<PrijavaNaLicitaciju>>> UpdatePLic(Guid id, PrijavaNaLicitaciju request)
        {
            var result = await _plicR.UpdatePLic(id, request);
            if (result is null)
                return NotFound("Prijava nije pronadjen");
            return Ok(result);
        }

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
