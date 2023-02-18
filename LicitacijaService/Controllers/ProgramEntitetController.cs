using AutoMapper;
using LicitacijaService.Data.Interfaces;
using LicitacijaService.Entities;
using LicitacijaService.Models.ProgramEntitet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicitacijaService.Controllers
{
    /// <summary>
    /// Kontroler za program
    /// </summary>
    [ApiController]
    [Route("api/programEntitet")]
    [Produces("application/json", "application/xml")]
    public class ProgramEntitetController : ControllerBase
    {
        private readonly IProgramEntitetRepository _programEntitetRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Konstruktor kontrolera programa - DI
        /// </summary>
        /// <param name="programEntitetRepository">Repo programa</param>
        /// <param name="linkGenerator">Link generator za create zahtev</param>
        /// <param name="mapper">AutoMapper</param>
        public ProgramEntitetController(IProgramEntitetRepository programEntitetRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            _programEntitetRepository = programEntitetRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        /// <summary>
        /// Vraća sve programe
        /// </summary>
        /// <returns>Lista programa</returns>
        /// <response code="200">Vraća listu programa</response>
        /// <response code="404">Nije pronađen nijedan program</response>

        [Authorize(Roles = "Administrator, Superuser, Menadzer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<List<ProgramEntitetDto>>> GetAllProgramEntitet()
        {
            var programEntiteti = await _programEntitetRepository.GetAllProgramEntitet();

            if (programEntiteti == null || programEntiteti.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<ProgramEntitetDto>>(programEntiteti));
        }

        /// <summary>
        /// Vraća jedan program na osnovu ID-a
        /// </summary>
        /// <param name="programId">ID programa</param>
        /// <returns>ProgramEntitet</returns>
        /// <response code="200">Vraća traženi program</response>
        /// <response code="404">Nije pronađen program za uneti ID</response>
        ///
        [Authorize(Roles = "Administrator, Superuser, Menadzer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{programId}")]
        public async Task<ActionResult<ProgramEntitetDto>> GetProgramEntitet(Guid programId)
        {
            var programEntitet = await _programEntitetRepository.GetProgramEntitetById(programId);

            if (programEntitet == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProgramEntitetDto>(programEntitet));
        }

        /// <summary>
        /// Kreira novi program
        /// </summary>
        /// <param name="programEntitet">Model programa</param>
        /// <remarks>
        /// Primer zahteva za kreiranje novog programa \
        /// POST /api/licitacija \
        /// {   
        ///      "maksimalnaPovrsina": 0,\
        ///      "krugLicitacije": 0\
        ///}
        /// </remarks>
        /// <returns>Potvrda o kreiranju programa</returns>
        /// <response code="201">Vraća kreiran program</response>
        /// <response code="500">Desila se greška prilikom unosa novog programa</response>
        /// 

        [Authorize(Roles = "Administrator, Superuser")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<ProgramEntitetCreationDto>> CreateProgramEntitet([FromBody] ProgramEntitetCreationDto programEntitet)
        {
            try
            {
                ProgramEntitet createdProgramEntitet = await _programEntitetRepository.CreateProgramEntitet(_mapper.Map<ProgramEntitet>(programEntitet));

                string location = _linkGenerator.GetPathByAction("GetProgramEntitet", "ProgramEntitet", new { programId = createdProgramEntitet.ProgramId });
                
                return Created(location, _mapper.Map<ProgramEntitetCreationDto>(createdProgramEntitet));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create ProgramEntitet error");
            }
        }
        /// <summary>
        /// Modifikacija  programa
        /// </summary>
        /// <param name="programId">ID programa</param>
        /// <param name="programEntitet">Model programa</param>
        /// <returns>Potvrda o modifikaciji programa</returns>
        /// <response code="200">Izmenjen program</response>
        /// <response code="400">Desila se greška prilikom unosa istih podataka za program</response>
        /// <response code="404">Nije pronađen program za uneti ID</response>
        /// <response code="500">Serverska greška tokom modifikacije programa</response>
        ///

        [Authorize(Roles = "Administrator, Superuser")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{programId}")]
        public async Task<ActionResult<ProgramEntitetUpdateDto>> UpdateProgramEntitet(Guid programId, [FromBody] ProgramEntitetUpdateDto programEntitet)
        {
            try
            {
                var programEntitetUpdate = await _programEntitetRepository.GetProgramEntitetById(programId);

                if (programEntitetUpdate == null)
                {
                    return NotFound();
                }

                _mapper.Map(programEntitet, programEntitetUpdate);

                await _programEntitetRepository.UpdateProgramEntitet(_mapper.Map<ProgramEntitet>(programEntitet));

                return Ok(programEntitet);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update ProgramEntitet error");
            }
        }
        /// <summary>
        /// Brisanje programa na osnovu ID-a
        /// </summary>
        /// <param name="programId">ID programa</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Program je uspešno obrisan</response>
        /// <response code="404">Nije pronađen program za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja programa</response>
        /// 
        [Authorize(Roles = "Administrator, Superuser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{programId}")]
        public async Task<ActionResult> DeleteProgramEntitet(Guid programId)
        {
            try
            {
                var programEntitet = await _programEntitetRepository.GetProgramEntitetById(programId);

                if (programEntitet == null)
                {
                    return NotFound();
                }

                await _programEntitetRepository.DeleteProgramEntitet(programId);

                return NoContent();
            }
            catch (Exception ex)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, "Delete ProgramEntitet error");
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa programima
        /// </summary>
        /// <returns></returns>

        [Authorize(Roles = "Administrator, Superuser")]
        [HttpOptions]
        public IActionResult GetProgramEntitetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
