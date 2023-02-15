using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MikroservisKomsija.Services.ClanService;

namespace MikroservisKomsija.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Superuser")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClanController : ControllerBase
    { 
        private readonly IClan _clanR;
        public ClanController(IClan clanR)
        {
            _clanR = clanR;
        }

        [HttpGet]
        [Authorize(Roles = "Menadzer")]
        public async Task<ActionResult<List<Clan>>> GetAllClans()
        {
            return await _clanR.GetAllClans();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Menadzer")]
        public async Task<ActionResult<Clan>> GetSingleClan(int id)
        {
            var result = await _clanR.GetSingleClan(id);
            if (result is null)
                return NotFound("Clan nije pronadjen");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Clan>>> AddClan (Clan clan)
        {
            var result = await _clanR.AddClan(clan);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Clan>>> UpdateClan(int id, Clan request)
        {
            var result = await _clanR.UpdateClan(id, request);
            if (result is null)
                return NotFound("Clan nije pronadjen");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Clan>>> DeleteClan(int id)
        {
            var result= await _clanR.DeleteClan(id);
            if (result is null)
                return NotFound("Clan nije pronadjen");
            return Ok(result);
        }
    }
}
