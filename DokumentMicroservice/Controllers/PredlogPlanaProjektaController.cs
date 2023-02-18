using AutoMapper;
using DokumentMicroservice.Data;
using DokumentMicroservice.Data.Interfaces;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Models;
using DokumentMicroservice.Models.PredlogPlanaProjekta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace DokumentMicroservice.Controllers
{
    /// <summary>
    /// kontroler za predlog plana projekta
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/predlogPlanaProjekta")]
    public class PredlogPlanaProjektaController : ControllerBase
    {

        private readonly IPredlogPlanaProjektaRepository _predlogPlanaProjektaRepository;
        private readonly IMapper _mapper;

        public PredlogPlanaProjektaController(IMapper mapper, IPredlogPlanaProjektaRepository predlogPlanaProjektaRepository)
        {
            _mapper = mapper;
            _predlogPlanaProjektaRepository = predlogPlanaProjektaRepository;
        }
        /// <summary>
        ///     Vraća sve predloge plana projekta
        /// </summary>
        /// <returns>Lista predloga plana projekta</returns>
        /// <response code="200">Vraća listu predloga</response>
        /// <response code="204">Nije pronadjen nijedan predlog</response>
        /// <response code="500">Greška prilikom vraćanja liste predloga</response>
        ///// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PredlogPlanaProjektaDto>>> GetAllPredlogPlanaProjekta()
        {
            try
            {
                var predlozi = await _predlogPlanaProjektaRepository.GetAllPredlogPlanaProjekta();

                if (predlozi == null || predlozi.Count == 0)
                {
                    
                    return NoContent();
                }

             
                return Ok(_mapper.Map<List<PredlogPlanaProjektaDto>>(predlozi));
            }
            catch (Exception ex)
            {
          
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Greška prilikom vraćanja liste predloga plana projekta.");
            }
        }

        /// <summary>
        ///     Vraća jedan predlog plana projekta na osnovu ID-a
        /// </summary>
        /// <param name="predlogId">ID dokumenta</param>
        /// <returns>Dokument</returns>
        /// <response code="200">Vraća traženi dokument</response>
        /// <response code="404">Nije pronadjen dokument za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja dokumenta</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpGet("{predlogId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PredlogPlanaProjektaDto>> GetPredlogPlanaProjektaById(Guid predlogId)
        {
            try
            {
                var predlog = await _predlogPlanaProjektaRepository.GetPredlogPlanaProjektaById(predlogId);

                if (predlog == null)
                {
                   
                    return NotFound();
                }

               
                return Ok(_mapper.Map<PredlogPlanaProjektaDto>(predlog));
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom vraćanja predloga sa id-jem {predlogId}.");
            }
        }

        /// <summary>
        ///     Kreira novi predlog plana projekta
        /// </summary>
        /// <param name="redlogPlanaprojektaDto">Model predloga plana projekta</param>
        /// <returns>Dokument</returns>
        /// <response code="201">Vraća kreirani dokument</response>
        /// <response code="500">Greška prilikom kreiranja dokumenta</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, PrvaKomisija")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreatePredlogaPlanaProjekta([FromBody] CreatePredlogPlanaProjektaDto predlogPlanaProjektaDto)
        {
            try
            {
                var predlog = _mapper.Map<PredlogPlanaProjekta>(predlogPlanaProjektaDto);

                await _predlogPlanaProjektaRepository.CreatePredlogPlanaProjekta(predlog);

                return CreatedAtAction(
                    "GetPredlogPlanaProjektaById",
                    new { predlogId = predlog.PredlogId },
                    _mapper.Map<PredlogPlanaProjektaConfirmation>(predlog)
                );
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja predloga.");
            }
        }


        /// <summary>
        /// Izmena predloga plana projekta
        /// </summary>
        /// <param name="predlogPlanaProjekta">Model predloga plana projekta</param>
        /// <returns>Potvrda o izmeni predloga plana projekta</returns>
        /// <response code="200">Izmenjen predlog plana projekta</response>
        /// <response code="404">Nije pronađen predlog plana projekta za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene predloga plana projekta </response>
        [Authorize(Roles = "Administrator, Superuser,  PrvaKomisija")]
        [HttpPut("{predlogId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PredlogPlanaProjektaDto>> UpdatePredlogPlanaProjekta(UpdatePredlogPlanaProjektaDto predlog)
        {
            try
            {
                var staripredlog = await _predlogPlanaProjektaRepository.GetPredlogPlanaProjektaById(predlog.PredlogId);

                if (staripredlog == null)
                {

                    return NotFound();
                }


                PredlogPlanaProjekta novipredlog = _mapper.Map<PredlogPlanaProjekta>(predlog);

                _mapper.Map(novipredlog, staripredlog);
                await _predlogPlanaProjektaRepository.SaveChangesAsync();


                return Ok(_mapper.Map<PredlogPlanaProjektaDto>(staripredlog));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene predloga plana projekta.");
            }
        }



        /// <summary>
        ///     Brisanje predloga plana projekta na osnovu ID-a
        /// </summary>
        /// <param name="predlogId">ID predloga plana projekta</param>
        /// <response code="204">Predlog plana je uspešno obrisan</response>
        /// <response code="404">Nije pronadjen predlog za uneti ID</response>
        /// <response code="500">Greška prilikom brisanja predloga</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, PrvaKomisija")]
        [HttpDelete("{predlogId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePredlogPlanaProjekta(Guid predlogId)
        {
            try
            {
                var predlog = await _predlogPlanaProjektaRepository.GetPredlogPlanaProjektaById(predlogId);

                if (predlog == null)
                {
                    
                    return NotFound();
                }

                await _predlogPlanaProjektaRepository.DeletePredlogPlanaProjekta(predlogId);

                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom brisanja predloga plana projekta sa id-jem {predlogId}.");
            }
        }
       


        /// <summary>
        ///     Vraća opcije za rad sa predlozima plana projekta
        /// </summary>
        /// <response code="200">Vraća listu opcija u header-u</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetPredlogOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }






















    }




}

