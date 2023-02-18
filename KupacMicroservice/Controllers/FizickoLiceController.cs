using AutoMapper;
using KupacMicroservice.Data;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.Entities;
using KupacMicroservice.model.FizickoLice;
using KupacMicroservice.model.Kupac;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace KupacMicroservice.Controllers
{

    /// <summary>
    /// kontroler za fizicka lica
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/fizickolice")]
    public class FizickoLiceController : ControllerBase
    {

        private readonly IFizickoLiceRepository _fizickoLiceRepository;
        private readonly IMapper _mapper;

        public FizickoLiceController(IMapper mapper, IFizickoLiceRepository fizickoLiceRepository)
        {
            _mapper = mapper;
            _fizickoLiceRepository = fizickoLiceRepository;

        }

        /// <summary>
        ///     Vraća sva fizicka lica
        /// </summary>
        /// <returns>Lista fizickih lica</returns>
        /// <response code="200">Vraća listu fizickih lica</response>
        /// <response code="204">Nije pronadjeno nijedno fizicko lice</response>
        /// <response code="500">Greška prilikom vraćanja liste fizickih lica</response>
        ///// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<FizickoLiceDto>>> GetAllFizickoLice()
        {
            try
            {
                var fizickalica = await _fizickoLiceRepository.GetAllFizickoLice();

                if (fizickalica == null || fizickalica.Count == 0)
                {
                   
                    return NoContent();
                }
               

                return Ok(_mapper.Map<List<FizickoLiceDto>>(fizickalica));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Greška prilikom vraćanja liste fizickih lica.");
            }
        }

        /// <summary>
        ///     Vraća jedno fizicko lice na osnovu ID-a
        /// </summary>
        /// <param name="FizickoliceId">ID fizickog lica</param>
        /// <returns>Dokument</returns>
        /// <response code="200">Vraća traženo fizicko lice</response>
        /// <response code="404">Nije pronadjeno nijedno fizicko lice za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja fizickog lica</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpGet("{fizickoliceId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<FizickoLiceDto>> GetFizickoLiceById(Guid fizickoliceId)
        {
            try
            {
                var fizickolice = await _fizickoLiceRepository.GetFizickoLiceById(fizickoliceId);

                if (fizickolice == null)
                {
                   
                    return NotFound();
                }

              
                return Ok(_mapper.Map<FizickoLiceDto>(fizickolice));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom vraćanja kupca sa id-jem {fizickoliceId}.");
            }
        }

        /// <summary>
        ///     Kreira novo fizicko lice
        /// </summary>
        /// <param name="fizickoliceDto">Model fizickog lica</param>
        /// <returns>Dokument</returns>
        /// <response code="201">Vraća kreirano fizicko lice</response>
        /// <response code="500">Greška prilikom kreiranja fizickog lica</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateFizickoLice([FromBody] CreateFizickoLiceDto fizickoliceDto)
        {
            try
            {
                var fizickolice = _mapper.Map<FizickoLice>(fizickoliceDto);

                await _fizickoLiceRepository.CreateFizickoLice(fizickolice);


                return CreatedAtAction(
                    "GetFizickoLiceById",
                    new { fizickoliceId = fizickolice.FizickoliceId },
                    _mapper.Map<FizickoLiceConfirmation>(fizickolice)
                );
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja fizickog lica.");
            }
        }

        /// <summary>
        /// Izmena fizickog lica
        /// </summary>
        /// <param name="fizickolice">Model fizickog lica</param>
        /// <returns>Potvrda o izmeni fizickog lica</returns>
        /// <response code="200">Izmenjeno fizicko lice</response>
        /// <response code="404">Nije pronađeno fizicko lice za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene fizickog lica</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpPut("{fizickoliceId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FizickoLiceDto>> UpdateFizickoLice(UpdateFizickoLiceDto fizickolice)
        {
            try
            {
                var staroflice = await _fizickoLiceRepository.GetFizickoLiceById(fizickolice.FizickoliceId);

                if (staroflice == null)
                {
                    
                    return NotFound();
                }
                

                FizickoLice novoflice = _mapper.Map<FizickoLice>(fizickolice);

                _mapper.Map(novoflice, staroflice);
                await _fizickoLiceRepository.SaveChangesAsync();
               

                return Ok(_mapper.Map<FizickoLiceDto>(staroflice));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene fizickog lica.");
            }
        }


        /// <summary>
        ///     Brisanje fizickog lica na osnovu ID-a
        /// </summary>
        /// <param name="id">ID fizickog lica</param>
        /// <response code="204">Fizicko lice je uspešno obrisano</response>
        /// <response code="404">Nije pronadjeno nijedno fizicko lice za uneti ID</response>
        /// <response code="500">Greška prilikom brisanja fizickog lica</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpDelete("{fizickoliceId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteFizickoLice(Guid fizickoliceId)
        {
            try
            {
                var fizickolice = await _fizickoLiceRepository.GetFizickoLiceById(fizickoliceId);

                if (fizickolice == null)
                {
                   
                    return NotFound();
                }

                await _fizickoLiceRepository.DeleteFizickoLice(fizickoliceId);


               

                return NoContent();
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom brisanja fizickog lica sa id-jem {fizickoliceId}.");
            }
        }


        /// <summary>
        ///     Vraća opcije za rad sa fizickim licima
        /// </summary>
        /// <response code="200">Vraća listu opcija u header-u</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
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
