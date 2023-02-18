using AutoMapper;
using KupacMicroservice.Data;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.Entities;
using KupacMicroservice.model.FizickoLice;
using KupacMicroservice.model.PravnoLice;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace KupacMicroservice.Controllers
{

    /// <summary>
    /// kontroler za pravna lica
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/pravnolice")]
    public class PravnoLiceController : ControllerBase
    {

        private readonly IPravnoLiceRepository _pravnoLiceRepository;
        private readonly IMapper _mapper;

        public PravnoLiceController(IMapper mapper, IPravnoLiceRepository pravnoLiceRepository)
        {
            _mapper = mapper;
            _pravnoLiceRepository = pravnoLiceRepository;

        }


        /// <summary>
        ///     Vraća sva pravna lica
        /// </summary>
        /// <returns>Lista pravnih lica</returns>
        /// <response code="200">Vraća listu pravnih lica</response>
        /// <response code="204">Nije pronadjeno nijedno pravno lice</response>
        /// <response code="500">Greška prilikom vraćanja liste pravnih lica</response>
        ///// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PravnoLiceDto>>> GetAllPravnoLice()
        {
            try
            {
                var pravnalica = await _pravnoLiceRepository.GetAllPravnoLice();

                if (pravnalica == null || pravnalica.Count == 0)
                {
                    
                    return NoContent();
                }
                

                return Ok(_mapper.Map<List<PravnoLiceDto>>(pravnalica));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Greška prilikom vraćanja liste pravnih lica.");
            }
        }

        /// <summary>
        ///     Vraća jedno pravno lice na osnovu ID-a
        /// </summary>
        /// <param name="PravnoliceId">ID pravnog lica</param>
        /// <returns>Pravno lice</returns>
        /// <response code="200">Vraća traženo pravno lice</response>
        /// <response code="404">Nije pronadjeno pravno lice za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja pravnog lica</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpGet("{pravnoliceId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PravnoLiceDto>> GetPravnoLiceById(Guid pravnoliceId)
        {
            try
            {
                var pravnolice = await _pravnoLiceRepository.GetPravnoLiceById(pravnoliceId);

                if (pravnolice == null)
                {
                    
                    return NotFound();
                }

                

                return Ok(_mapper.Map<PravnoLiceDto>(pravnolice));
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom vraćanja pravnog lica sa id-jem {pravnoliceId}.");
            }
        }

        /// <summary>
        ///     Kreira novo pravno lice
        /// </summary>
        /// <param name="PravnoLiceDto">Model pravnog lica </param>
        /// <returns>Dokument</returns>
        /// <response code="201">Vraća kreirano pravno lice</response>
        /// <response code="500">Greška prilikom kreiranjapravnog lica</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreatePravnoLice([FromBody] CreatePravnoLiceDto pravnoliceDto)
        {
            try
            {
                var pravnolice = _mapper.Map<PravnoLice>(pravnoliceDto);

                await _pravnoLiceRepository.CreatePravnoLice(pravnolice);


                return CreatedAtAction(
                    "GetPravnoLiceById",
                    new { pravnoliceId = pravnolice.PravnoliceId },
                    _mapper.Map<PravnoLiceConfirmation>(pravnolice)
                );
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja pravnog lica.");
            }
        }

        /// <summary>
        /// Izmena pravnog lica
        /// </summary>
        /// <param name="pravnolice">Model pravnog lica</param>
        /// <returns>Potvrda o izmeni pravnog lica</returns>
        /// <response code="200">Izmenjen pravnog lica</response>
        /// <response code="404">Nije pronađeno nijedno pravno lice za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene pravnog lica </response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpPut("{pravnoliceId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PravnoLiceDto>> UpdatePravnoLice(UpdatePravnoLiceDto pravnolice)
        {
            try
            {
                var staroplice = await _pravnoLiceRepository.GetPravnoLiceById(pravnolice.PravnoliceId);

                if (staroplice == null)
                {
                   
                    return NotFound();
                }
                

                PravnoLice novoplice = _mapper.Map<PravnoLice>(pravnolice);

                _mapper.Map(novoplice, staroplice);
                await _pravnoLiceRepository.SaveChangesAsync();
              

                return Ok(_mapper.Map<PravnoLiceDto>(staroplice));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene pravnog lica.");
            }
        }


        /// <summary>
        ///     Brisanje pravnog lica na osnovu ID-a
        /// </summary>
        /// <param name="PravnoliceId">ID pravnog lica</param>
        /// <response code="204">Pravno lice je uspešno obrisano</response>
        /// <response code="404">Nije pronadjeno pravno lice za uneti ID</response>
        /// <response code="500">Greška prilikom brisanja pravnog lica</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpDelete("{pravnoliceId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePravnoLice(Guid pravnoliceId)
        {
            try
            {
                var pravnolice = await _pravnoLiceRepository.GetPravnoLiceById(pravnoliceId);

                if (pravnolice == null)
                {
                   
                    return NotFound();
                }

                await _pravnoLiceRepository.DeletePravnoLice(pravnoliceId);


                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom brisanja pravnog lica sa id-jem {pravnoliceId}.");
            }
        }


        /// <summary>
        ///     Vraća opcije za rad sa pravnim licima
        /// </summary>
        /// <response code="200">Vraća listu opcija u header-u</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetFizickoLicasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }




    }
}
