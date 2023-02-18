using AutoMapper;
using KupacMicroservice.Data;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.Entities;
using KupacMicroservice.model.Kupac;
using KupacMicroservice.Model.Liciter;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace KupacMicroservice.Controllers
{

    /// <summary>
    /// kontroler za licitere
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/liciter")]
    public class LiciterController : ControllerBase
    {

        private readonly ILiciterRepository _liciterRepository;
        private readonly IMapper _mapper;

        public LiciterController(IMapper mapper, ILiciterRepository liciterRepository)
        {
            _mapper = mapper;
            _liciterRepository = liciterRepository;

        }

        /// <summary>
        ///     Vraća sve licitere
        /// </summary>
        /// <returns>Lista licitera</returns>
        /// <response code="200">Vraća listu licitera</response>
        /// <response code="204">Nije pronadjen nijedan liciter</response>
        /// <response code="500">Greška prilikom vraćanja liste licitera</response>
        ///// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<LiciterDto>>> GetAllLiciter()
        {
            try
            {
                var liciteri = await _liciterRepository.GetAllLiciter();

                if (liciteri == null || liciteri.Count == 0)
                {
                    
                    return NoContent();
                }
                

                return Ok(_mapper.Map<List<LiciterDto>>(liciteri));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Greška prilikom vraćanja liste licitera.");
            }
        }

        /// <summary>
        ///     Vraća jednog licitera na osnovu ID-a
        /// </summary>
        /// <param name="liciterId">ID licitera</param>
        /// <returns>Dokument</returns>
        /// <response code="200">Vraća traženog kupca</response>
        /// <response code="404">Nije pronadjen kupac za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja kupca</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpGet("{liciterId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LiciterDto>> GetLiciterById(Guid liciterId)
        {
            try
            {
                var liciter = await _liciterRepository.GetLiciterById(liciterId);

                if (liciter == null)
                {
                   
                    return NotFound();
                }

                

                return Ok(_mapper.Map<LiciterDto>(liciter));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom vraćanja licitera sa id-jem {liciterId}.");
            }
        }



        /// <summary>
        ///     Kreira novog licitera
        /// </summary>
        /// <param name="liciterDto">Model licitera </param>
        /// <returns>Liciter</returns>
        /// <response code="201">Vraća kreiranog licitera</response>
        /// <response code="500">Greška prilikom kreiranja licitera</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
         [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateLiciter([FromBody] CreateLiciterDto liciterDto)
        {
            try
            {
                var liciter = _mapper.Map<Liciter>(liciterDto);

                await _liciterRepository.CreateLiciter(liciter);


                return CreatedAtAction(
                    "GetLiciterById",
                    new { liciterId = liciter.LiciterId },
                    _mapper.Map<LiciterConfirmation>(liciter)
                );
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja licitera.");
            }
        }

        /// <summary>
        /// Izmena licitera
        /// </summary>
        /// <param name="liciter">Model licitera</param>
        /// <returns>Potvrda o izmeni licitera</returns>
        /// <response code="200">Izmenjen liciter</response>
        /// <response code="404">Nije pronađen liciter za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene </response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpPut("{liciterId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LiciterDto>> UpdateLiciter(UpdateLiciterDto liciter)
        {
            try
            {
                var starilic = await _liciterRepository.GetLiciterById(liciter.LiciterId);

                if (starilic == null)
                {
                   
                    return NotFound();
                }
               

                Liciter novilic = _mapper.Map<Liciter>(liciter);

                _mapper.Map(novilic, starilic);
                await _liciterRepository.SaveChangesAsync();
               

                return Ok(_mapper.Map<LiciterDto>(starilic));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene licitera.");
            }
        }

        /// <summary>
        ///     Brisanje licitera na osnovu ID-a
        /// </summary>
        /// <param name="Liciterid">ID licitera</param>
        /// <response code="204">liciter je uspešno obrisan</response>
        /// <response code="404">Nije pronadjen liciter za uneti ID</response>
        /// <response code="500">Greška prilikom brisanja licitera</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpDelete("{liciterId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteLiciter(Guid liciterId)
        {
            try
            {
                var liciter = await _liciterRepository.GetLiciterById(liciterId);

                if (liciter == null)
                {
                    
                    return NotFound();
                }

                await _liciterRepository.DeleteLiciter(liciterId);



                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom brisanja licitera sa id-jem {liciterId}.");
            }
        }




        /// <summary>
        ///     Vraća opcije za rad sa liciterima
        /// </summary>
        /// <response code="200">Vraća listu opcija u header-u</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetLiciterOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }











    }
}
