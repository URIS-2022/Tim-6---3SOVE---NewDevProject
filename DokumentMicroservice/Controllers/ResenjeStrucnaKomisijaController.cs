
using AutoMapper;
using DokumentMicroservice.Data;
using DokumentMicroservice.Data.Interfaces;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Models;
using DokumentMicroservice.Models.PredlogPlanaProjekta;
using DokumentMicroservice.Models.ResenjeStrucnaKomisija;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DokumentMicroservice.Controllers
{

    /// <summary>
    /// kontroler za resenja strucne komisije
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/resenjeStrucnaKomisija")]
    public class ResenjeStrucnaKomisijaController : ControllerBase
    {

        private readonly IResenjeStrucnaKomisijaRepository _resenjeStrucnaKomisijaRepository;
        private readonly IMapper _mapper;


        public ResenjeStrucnaKomisijaController(IMapper mapper, IResenjeStrucnaKomisijaRepository resenjeStrucnaKomisijaRepository)
        {
            _mapper = mapper;
            _resenjeStrucnaKomisijaRepository = resenjeStrucnaKomisijaRepository;

        }


        /// <summary>
        ///     Vraća sva resenja strucne komisije
        /// </summary>
        /// <returns>Lista resenje strucne komisije</returns>
        /// <response code="200">Vraća listu resenja strucne komisije</response>
        /// <response code="204">Nije pronadjeno nijedano resenje strucne komisije</response>
        /// <response code="500">Greška prilikom vraćanja liste resenja strucne komisije</response>
        //// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ResenjeStrucnaKomisijaDto>>> GetAllResenjeStrucnaKomisija()
        {
            try
            {
                var resenja = await _resenjeStrucnaKomisijaRepository.GetAllResenjeStrucnaKomisija();

                if (resenja == null || resenja.Count == 0)
                {
                   
                    return NoContent();
                }

                return Ok(_mapper.Map<List<ResenjeStrucnaKomisijaDto>>(resenja));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Greška prilikom vraćanja liste resenja.");
            }
        }


        /// <summary>
        ///     Vraća jedno resenje strucne komisije na osnovu ID-a
        /// </summary>
        /// <param name="resenjeId">ID resenja strucne komisije</param>
        /// <returns>Resenje</returns>
        /// <response code="200">Vraća traženo resenje strucne komisije</response>
        /// <response code="404">Nije pronadjeno resenje strucne komisije za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja resenja strucne komisije</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpGet("{resenjeId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ResenjeStrucnaKomisijaDto>> GetResenjeStrucnaKomisijaById(Guid resenjeId)
        {
            try
            {
                var resenje = await _resenjeStrucnaKomisijaRepository.GetResenjeStrucnaKomisijaById(resenjeId);

                if (resenje == null)
                {
                    
                    return NotFound();
                }

                return Ok(_mapper.Map<ResenjeStrucnaKomisijaDto>(resenje));
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom vraćanja resenja strucne komisije sa id-jem {resenjeId}.");
            }
        }




        /// <summary>
        /// Izmena resenja strucne komisije
        /// </summary>
        /// <param name="dokument">Model resenja strucne komisije</param>
        /// <returns>Potvrda o izmeni resenja strucne komisije</returns>
        /// <response code="200">Izmenjeno resenje strucne komisije</response>
        /// <response code="404">Nije pronađeno resenje za uneti ID resenja strucne komisije</response>
        /// <response code="500">Serverska greška tokom izmene resenja strucne komisije </response>
        [Authorize(Roles = "Administrator, Superuser,  PrvaKomisija")]
        [HttpPut("{resenjeId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResenjeStrucnaKomisijaDto>> UpdateResenjeStrucnaKomisija(UpdateResenjeStrucnaKomisijaDto resenje)
        {
            try
            {
                var staroresenje = await _resenjeStrucnaKomisijaRepository.GetResenjeStrucnaKomisijaById(resenje.ResenjeId);

                if (staroresenje == null)
                {
                   
                    return NotFound();
                }
                

                ResenjeStrucnaKomisija novoresenje = _mapper.Map<ResenjeStrucnaKomisija>(resenje);

                _mapper.Map(novoresenje, staroresenje);
                await _resenjeStrucnaKomisijaRepository.SaveChangesAsync();
                

                return Ok(_mapper.Map<ResenjeStrucnaKomisijaDto>(staroresenje));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene resenja strucne komisije.");
            }
        }



        /// <summary>
        ///     Kreira novo resenje strucne komisije
        /// </summary>
        /// <param name="resenjeDto">Model resenja strucne komisije</param>
        /// <returns>Dokument</returns>
        /// <response code="201">Vraća kreirano resenje strucne komisije</response>
        /// <response code="500">Greška prilikom kreiranja resenja strucne komisije</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, PrvaKomisija")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateResenjeStrucnaKomisija([FromBody] CreateResenjeStrucnaKomisijaDto resenjeStrucnaKomisijaDto)
        {
            try
            {
                var resenje = _mapper.Map<ResenjeStrucnaKomisija>(resenjeStrucnaKomisijaDto);

                await _resenjeStrucnaKomisijaRepository.CreateResenjeStrucnaKomisija(resenje);

                return CreatedAtAction(
                    "GetResenjeStrucnaKomisijaById",
                    new { resenjeId = resenje.ResenjeId },
                    _mapper.Map<ResenjeStrucnaKomisijaConfirmation>(resenje)
                );
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja resenja.");
            }
        }



        /// <summary>
        ///     Brisanje resenja strucne komisije na osnovu ID-a
        /// </summary>
        /// <param name="resenjeId">ID resenja strucne komisije</param>
        /// <response code="204">Resenje je uspešno obrisano resenje strucne komisije</response>
        /// <response code="404">Nije pronadjeno resenje za uneti ID strucne komisije</response>
        /// <response code="500">Greška prilikom brisanja resenja strucne komisije</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, PrvaKomisija")]
        [HttpDelete("{resenjeId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteResenjeStrucnaKomisija(Guid resenjeId)
        {
            try
            {
                var resenje = await _resenjeStrucnaKomisijaRepository.GetResenjeStrucnaKomisijaById(resenjeId);

                if (resenje == null)
                {
                   
                    return NotFound();
                }

                await _resenjeStrucnaKomisijaRepository.DeleteResenjeStrucnaKomisija(resenjeId);

                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom brisanja resenja sa id-jem {resenjeId}.");
            }
        }


        /// <summary>
        ///     Vraća opcije za rad sa resenjima strucne komisije
        /// </summary>
        /// <response code="200">Vraća listu opcija u header-u</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetResenjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


















    }

























}
