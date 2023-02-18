using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrijavaJnService.Data.Interfaces;
using PrijavaJnService.Entities;
using PrijavaJnService.Entities.Confirmations;
using PrijavaJnService.Models.PrijavaJn;
using PrijavaJnService.ServiceCalls;
using PrijavaJnService.ServiceCalls.Mocks;
using System.Text.Json.Serialization;
using Microsoft.Net.Http.Headers;

namespace PrijavaJnService.Controllers
{
    /// <summary>
    /// Kontroler za prijavuJn
    /// </summary>
    [Route("api/prijavaJn")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class PrijavaJnController : ControllerBase
    {
        private readonly IPrijavaJnRepository _prijavaJnRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IServiceCall<KupacDto> _kupacService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Konstruktor kontrolera prijaveJn - DI
        /// </summary>
        /// <param name="prijavaJnRepository">Repo prijaveJn</param>
        /// <param name="linkGenerator">Link generator za create zahtev</param>
        /// <param name="mapper">AutoMapper</param>
        /// <param name="kupacService">KupacService</param>
        /// <param name="configuration">Configuration</param>
        public PrijavaJnController(IPrijavaJnRepository prijavaJnRepository, LinkGenerator linkGenerator, IMapper mapper, IServiceCall<KupacDto> kupacService, IConfiguration configuration)
        {
            _prijavaJnRepository = prijavaJnRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _kupacService = kupacService;
            _configuration = configuration;
        }

        /// <summary>
        /// Vraća sve prijaveJn
        /// </summary>
        /// <returns>Lista prijavaJn</returns>
        /// <response code="200">Vraća listu prijavaJn</response>
        /// <response code="404">Nije pronađena nijedna prijavaJn</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser, Menadzer, OperaterNadmetanja, Licitant")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PrijavaJnDto>>> GetAllPrijavaJn()
        {
            var prijaveJn = await _prijavaJnRepository.GetAllPrijavaJn();

            if (prijaveJn == null || prijaveJn.Count == 0)
            {
                return NoContent();
            }

            var prijaveJnDto = new List<PrijavaJnDto>();
            string url = _configuration["Services:KupacService"];
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            foreach (var prijavaJn in prijaveJn)
            {
                var prijavaJnDto = _mapper.Map<PrijavaJnDto>(prijavaJn);
                if (prijavaJn.KupacId is not null)
                {
                    var kupacDto = await _kupacService.SendGetRequestAsync(url + prijavaJn.KupacId, token);
                    if (kupacDto is not null)
                    {
                        /*prijavaJnDto.Kupac = kupacDto.AdresaKupac + ", "
                                                  + kupacDto.OstvarenaPovrsina + ", "
                                                  + kupacDto.ImaZabranu + ", "
                                                  + kupacDto.DuzinaTrajanjaZabraneGod
                                                  + kupacDto.BrojTelefona1 + ", "
                                                  + kupacDto.BrojTelefona2 + ", "
                                                  + kupacDto.Email + ", "
                                                  + kupacDto.BrojRacuna + ", "
                                                  + kupacDto.IznosUplata + ", "
                                                  + kupacDto.Prioritet + ", "
                                                  ;*/

                        prijavaJnDto.Kupac = kupacDto;
                    }
                }
                prijaveJnDto.Add(prijavaJnDto);
            }

            return Ok(prijaveJnDto);

        }
        /// <summary>
        /// Vraća jednu prijavuJn na osnovu ID-a
        /// </summary>
        /// <param name="prijavaId">ID prijaveJn</param>
        /// <returns>PrijavaJn</returns>
        /// <response code="200">Vraća traženu prijavuJn</response>
        /// <response code="404">Nije pronađena prijavaJn za uneti ID</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser, Menadzer, OperaterNadmetanja, Licitant")]
        [HttpGet("{prijavaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrijavaJnDto>> GetPrijavaJn(Guid prijavaId)
        {
            var prijavaJn = await _prijavaJnRepository.GetPrijavaJnById(prijavaId);

            if (prijavaJn == null)
            {
                return NotFound();
            }
           return Ok(prijavaJn);
        }

        /// <summary>
        /// Kreira novu prijavuJn
        /// </summary>
        /// <param name="prijavaJn">Model prijaveJn</param>
        /// <remarks>
        /// Primer zahteva za kreiranje nove prijaveJn \
        /// POST /api/prijavaJn \
        /// {   
        ///"brojPrijave": "string",\
        /// "datumPrijave": "2023-02-15T20:47:22.602Z",\
        /// "mestoPrijave": "string",\
        /// "satPrijave": "string",\
        /// "zatvorenaPonuda": true,\
        /// "dokFizickaLica": "string",\
        /// "dokPravnaLica": "string"\
        ///}
        /// </remarks>
        /// <returns>Potvrda o kreiranju prijaveJn</returns>
        /// <response code="201">Vraća kreiranu prijavuJn</response>
        /// <response code="500">Desila se greška prilikom unosa nove prijaveJn</response>
        ///
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanja")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PrijavaJnConfirmationDto>> CreatePrijavaJn([FromBody] PrijavaJnCreationDto prijavaJn)
        {
            try
            {
                PrijavaJnConfirmation novaPrijavaJn = await _prijavaJnRepository.CreatePrijavaJn(_mapper.Map<PrijavaJn>(prijavaJn));
                await _prijavaJnRepository.SaveChangesAsync();

                string lokacija = _linkGenerator.GetPathByAction("GetPrijavaJn", "PrijavaJn", new { prijavaId = novaPrijavaJn.PrijavaId });

                return Created(lokacija, _mapper.Map<PrijavaJnConfirmationDto>(novaPrijavaJn));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom unosa prijave");
            }
        }
        /// <summary>
        /// Modifikacija  prijaveJn
        /// </summary>
        /// <param name="prijavaId">ID prijaveJn</param>
        /// <param name="prijavaJn">Model prijaveJn</param>
        /// <returns>Potvrda o modifikaciji prijaveJn</returns>
        /// <response code="200">Izmenjena prijavaJn</response>
        /// <response code="400">Desila se greška prilikom unosa istih podataka za prijavuJn</response>
        /// <response code="404">Nije pronađena prijavaJn za uneti ID</response>
        /// <response code="500">Serverska greška tokom modifikacije prijaveJn</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanja")]
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PrijavaJnDto>> UpdatePrijavaJn(PrijavaJnUpdateDto prijavaJn)
        {
            try
            {
                var staraPrijavaJn = await _prijavaJnRepository.GetPrijavaJnById(prijavaJn.PrijavaId);

                if (staraPrijavaJn == null)
                {
                    return NotFound("Ova prijava ne postoji");
                }

                PrijavaJn novaPrijavaJn = _mapper.Map<PrijavaJn>(prijavaJn);

                _mapper.Map(novaPrijavaJn, staraPrijavaJn);
                await _prijavaJnRepository.UpdatePrijavaJn(novaPrijavaJn);
                await _prijavaJnRepository.SaveChangesAsync();

                
                return Ok(_mapper.Map<PrijavaJnDto>(staraPrijavaJn));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene prijave.");
            }
        }
        /// <summary>
        /// Brisanje prijaveJn na osnovu ID-a
        /// </summary>
        /// <param name="prijavaId">ID prijaveJn</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">PrijavaJn je uspešno obrisana</response>
        /// <response code="404">Nije pronađena prijavaJn za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja prijaveJn</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanja")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{prijavaId}")]
        public async Task<IActionResult> DeletePrijavaJn(Guid prijavaId)
        {
            try
            {
                var prijavaJn = await _prijavaJnRepository.GetPrijavaJnById(prijavaId);

                if (prijavaJn == null)
                {
                    return NotFound("Ova prijava ne postoji");
                }

                await _prijavaJnRepository.DeletePrijavaJn(prijavaId);
                await _prijavaJnRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom brisanja prijave");
            }
        }
        /// <summary>
        /// Vraća opcije za rad sa prijavamaJn
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanja")]
        [HttpOptions]
        public IActionResult GetPrijavaJnOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
