using AutoMapper;
using LicitacijaService.Data.Interfaces;
using LicitacijaService.Entities;
using LicitacijaService.Entities.Confirmations;
using LicitacijaService.Models.Licitacija;
using LicitacijaService.Models.ProgramEntitet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicitacijaService.Controllers
{
    /// <summary>
    /// Kontroler za licitaciju
    /// </summary>
    [ApiController]
    [Route("api/licitacija")]
    [Produces("application/json", "application/xml")]
    public class LicitacijaController : ControllerBase
    {
        private readonly ILicitacijaRepository _licitacijaRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Konstruktor kontrolera licitacije - DI
        /// </summary>
        /// <param name="licitacijaRepository">Repo licitacije</param>
        /// <param name="linkGenerator">Link generator za create zahtev</param>
        /// <param name="mapper">AutoMapper</param>
        public LicitacijaController(ILicitacijaRepository licitacijaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            _licitacijaRepository = licitacijaRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        /// <summary>
        /// Vraća sve licitacije
        /// </summary>
        /// <returns>Lista licitacija</returns>
        /// <response code="200">Vraća listu licitacija</response>
        /// <response code="404">Nije pronađena nijedna licitacija</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser, Menadzer, OperaterNadmetanja, PrvaKomisija")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<LicitacijaDto>>> GetAllLicitacija()
        {
            var licitacije = await _licitacijaRepository.GetAllLicitacija();

            if (licitacije == null || licitacije.Count == 0)
            {
                return NoContent();
            }
            
            return Ok(_mapper.Map<IEnumerable<LicitacijaDto>>(licitacije));
        }

        /// <summary>
        /// Vraća jednu licitaciju na osnovu ID-a
        /// </summary>
        /// <param name="licitacijaId">ID licitacije</param>
        /// <returns>Licitacija</returns>
        /// <response code="200">Vraća traženu licitaciju</response>
        /// <response code="404">Nije pronađena licitacija za uneti ID</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser, Menadzer, OperaterNadmetanja, PrvaKomisija")]
        [HttpGet("{licitacijaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<LicitacijaDto>>> GetLicitacija(Guid licitacijaId)
        {
            var licitacija = await _licitacijaRepository.GetLicitacijaById(licitacijaId);

            if (licitacija == null)
            {
                return NotFound();
            }

            var licitacijaDto = _mapper.Map<LicitacijaDto>(licitacija);
            return Ok(licitacijaDto);
        }

        /// <summary>
        /// Kreira novu licitaciju
        /// </summary>
        /// <param name="licitacija">Model licitacija</param>
        /// <remarks>
        /// Primer zahteva za kreiranje nove licitacije \
        /// POST /api/licitacija \
        /// {   
        ///     "brojLicitacije": 0,\
        ///     "godinaLicitacije": 0,\
        ///     "ogranicenjeLicitacije": 0,\
        ///     "rokLicitacije": "2023-02-14T21:35:04.492Z",\
        ///     "korakCeneLicitacije": 0,\
        ///    "programEntitetProgramId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"\
        ///}
        /// </remarks>
        /// <returns>Potvrda o kreiranju licitacije</returns>
        /// <response code="201">Vraća kreiranu licitaciju</response>
        /// <response code="500">Desila se greška prilikom unosa nove licitacije</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanja, PrvaKomisija")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LicitacijaConfirmationDto>> CreateLicitacija([FromBody] LicitacijaCreationDto licitacija)
        {
            try
            {
               LicitacijaConfirmation createdLicitacija = await _licitacijaRepository.CreateLicitacija(_mapper.Map<Licitacija>(licitacija));
               
                await _licitacijaRepository.SaveChangesAsync();

                string location = _linkGenerator.GetPathByAction("GetLicitacija", "Licitacija", new { licitacijaId = createdLicitacija.LicitacijaId });
                return Created(location, _mapper.Map<LicitacijaConfirmationDto>(createdLicitacija));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Modifikacija  licitacije
        /// </summary>
        /// <param name="licitacijaId">ID licitacije</param>
        /// <param name="licitacija">Model licitacije</param>
        /// <returns>Potvrda o modifikaciji licitacije</returns>
        /// <response code="200">Izmenjena licitacija</response>
        /// <response code="400">Desila se greška prilikom unosa istih podataka za licitaciju</response>
        /// <response code="404">Nije pronađena licitacija za uneti ID</response>
        /// <response code="500">Serverska greška tokom modifikacije licitacija</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser,  OperaterNadmetanja, PrvaKomisija")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{licitacijaId}")]
        public async Task<ActionResult<LicitacijaUpdateDto>> UpdateLicitacija(Guid licitacijaId, [FromBody] LicitacijaUpdateDto licitacija)
        {
            try
            {
                var licitacijaUpdate = await _licitacijaRepository.GetLicitacijaById(licitacijaId);

                if (licitacijaUpdate == null)
                {
                    return NotFound();
                }

                _mapper.Map(licitacija, licitacijaUpdate);

                await _licitacijaRepository.UpdateLicitacija(_mapper.Map<Licitacija>(licitacija));
                return Ok(licitacija);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Brisanje licitacije na osnovu ID-a
        /// </summary>
        /// <param name="licitacijaId">ID licitacije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Licitacija je uspešno obrisana</response>
        /// <response code="404">Nije pronađena licitacija za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja licitacije</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser,  OperaterNadmetanja, PrvaKomisija")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{licitacijaId}")]
        public async Task<ActionResult> DeleteLicitacija(Guid licitacijaId)
        {
            try
            {
                var licitacija = await _licitacijaRepository.GetLicitacijaById(licitacijaId);

                if (licitacija == null)
                {
                    return NotFound();
                }

                await _licitacijaRepository.DeleteLicitacija(licitacijaId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa licitacijama
        /// </summary>
        /// <returns></returns>

        [Authorize(Roles = "Administrator, Superuser,  OperaterNadmetanja, PrvaKomisija")]
        [HttpOptions]
        public IActionResult GetLicitacijaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


       [HttpGet("maksimalnaPovrsina/{licitacijaId}")]
        public async Task<ActionResult<int>> GetMaksimalnaPovrsina(Guid licitacijaId)
        {
            var licitacija = await _licitacijaRepository.GetLicitacijaById(licitacijaId);
            if (licitacija == null)
            {
                return NotFound();
            }
            return licitacija.ProgramEntitet.MaksimalnaPovrsina;
        }
       

    }
}
