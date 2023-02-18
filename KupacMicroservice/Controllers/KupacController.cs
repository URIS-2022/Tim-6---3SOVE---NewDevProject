using AutoMapper;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.model.Kupac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
namespace KupacMicroservice.Controllers
{


    /// <summary>
    /// kontroler za kupce
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/kupac")]
    public class KupacController : ControllerBase
    {

        private readonly IKupacRepository _kupacRepository;
        private readonly IMapper _mapper;

        public KupacController(IMapper mapper, IKupacRepository kupacRepository)
        {
            _mapper = mapper;
            _kupacRepository = kupacRepository;

        }

        /// <summary>
        ///     Vraća sve kupce
        /// </summary>
        /// <returns>Lista kupaca</returns>
        /// <response code="200">Vraća listu kupaca</response>
        /// <response code="204">Nije pronadjen nijedan kupac</response>
        /// <response code="500">Greška prilikom vraćanja liste kupaca</response>
        ///// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<KupacDto>>> GetAllKupac()
        {
            try
            {
                var kupci = await _kupacRepository.GetAllKupac();

                if (kupci == null || kupci.Count == 0)
                {
                   
                    return NoContent();
                }
               

                return Ok(_mapper.Map<List<KupacDto>>(kupci));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Greška prilikom vraćanja liste kupaca.");
            }
        }


        /// <summary>
        ///     Vraća jednog kupca na osnovu ID-a
        /// </summary>
        /// <param name="kupacId">ID kupca</param>
        /// <returns>Dokument</returns>
        /// <response code="200">Vraća traženog kupca</response>
        /// <response code="404">Nije pronadjen kupac za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja kupca</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpGet("{kupacId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<KupacDto>> GetKupacById(Guid kupacId)
        {
            try
            {
                var kupac = await _kupacRepository.GetKupacById(kupacId);

                if (kupac == null)
                {
                    
                    return NotFound();
                }

               

                return Ok(_mapper.Map<KupacDto>(kupac));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom vraćanja kupca sa id-jem {kupacId}.");
            }
        }



        /// <summary>
        ///     Kreira novog kupca
        /// </summary>
        /// <param name="kupacDto">Model kupca </param>
        /// <returns>Dokument</returns>
        /// <response code="201">Vraća kreiranog kupca</response>
        /// <response code="500">Greška prilikom kreiranja kupca</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateKupac([FromBody] CreateKupacDto kupacDto)
        {
            try
            {
                var kupac = _mapper.Map<Kupac>(kupacDto);

                await _kupacRepository.CreateKupac(kupac);


                return CreatedAtAction(
                    "GetKupacById",
                    new { kupacId = kupac.KupacId },
                    _mapper.Map<KupacConfirmation>(kupac)
                );
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja kupca.");
            }
        }

        /// <summary>
        /// Izmena kupca
        /// </summary>
        /// <param name="kupac">Model kupca</param>
        /// <returns>Potvrda o izmeni kupca</returns>
        /// <response code="200">Izmenjen kupac</response>
        /// <response code="404">Nije pronađen kupac za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene </response>
        [Authorize(Roles = "Administrator, Superuser,  PrvaKomisija,Manager, OperaterNadmetanja")]
        [HttpPut("{kupacId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<KupacDto>> UpdateKupac(UpdateKupacDto kupac)
        {
            try
            {
                var starikupac = await _kupacRepository.GetKupacById(kupac.KupacId);

                if (starikupac == null)
                {
                    
                    return NotFound();
                }
                

                Kupac novikupac = _mapper.Map<Kupac>(kupac);

                _mapper.Map(novikupac, starikupac);
                await _kupacRepository.SaveChangesAsync();
               

                return Ok(_mapper.Map<KupacDto>(starikupac));
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene kupca.");
            }
        }

        /// <summary>
        ///     Brisanje kupca na osnovu ID-a
        /// </summary>
        /// <param name="id">ID kupca</param>
        /// <response code="204">Kupac je uspešno obrisan</response>
        /// <response code="404">Nije pronadjen kupac za uneti ID</response>
        /// <response code="500">Greška prilikom brisanja kupca</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar")]
        [HttpDelete("{kupacId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteKupac(Guid kupacId)
        {
            try
            {
                var kupac = await _kupacRepository.GetKupacById(kupacId);

                if (kupac == null)
                {
                    
                    return NotFound();
                }

                await _kupacRepository.DeleteKupac(kupacId);


                

                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom brisanja kupca  sa id-jem {kupacId}.");
            }
        }




        /// <summary>
        ///     Vraća opcije za rad sa kupcima
        /// </summary>
        /// <response code="200">Vraća listu opcija u header-u</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Licitant, OperaterNadmetanja, TehnickiSekretar, Menadzer")]
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetKupacOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }








    }
}
