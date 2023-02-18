using AutoMapper;
using DokumentMicroservice.Data;
using DokumentMicroservice.Data.Interfaces;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Models;
using DokumentMicroservice.Models.Oglas;
using DokumentMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Data;


namespace DokumentMicroservice.Controllers
{
    /// <summary>
    /// kontroler za dokumente
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/dokument")]
    public class DokumentController : ControllerBase
    {

        private readonly IDokumentRepository _dokumentRepository;
        private readonly IMapper _mapper;
       

        public DokumentController(IMapper mapper, IDokumentRepository dokumentRepository)
        {
            _mapper = mapper;
            _dokumentRepository = dokumentRepository;
           
          
        }
        /// <summary>
        ///     Vraća sve dokumente
        /// </summary>
        /// <returns>Lista dokumenata</returns>
        /// <response code="200">Vraća listu dokumenata</response>
        /// <response code="204">Nije pronadjen nijedan dokument</response>
        /// <response code="500">Greška prilikom vraćanja liste dokumenata</response>
        ///// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<DokumentDto>>> GetAllDokument()
        {
            try
            {
                var dokumenti = await _dokumentRepository.GetAllDokument();

                if (dokumenti == null || dokumenti.Count == 0)
                {
                    
                    return NoContent();
                }

                //var dokumentiDto = new List<Komisija>();
                //string url = _configuration["Services:MikroservisKomisija"];

                return Ok(_mapper.Map<List<DokumentDto>>(dokumenti));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Greška prilikom vraćanja liste dokumenata.");
            }
        }

        /// <summary>
        ///     Vraća jedan dokument na osnovu ID-a
        /// </summary>
        /// <param name="dokumentId">ID dokumenta</param>
        /// <returns>Dokument</returns>
        /// <response code="200">Vraća traženi dokument</response>
        /// <response code="404">Nije pronadjen dokument za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja dokumenta</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpGet("{dokumentId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DokumentDto>> GetDokumentById(Guid dokumentId)
        {
            try
            {
                var document = await _dokumentRepository.GetDokumentById(dokumentId);

                if (document == null)
                {
                    
                    return NotFound();
                }

                

                return Ok(_mapper.Map<DokumentDto>(document));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom vraćanja dokumenta sa id-jem {dokumentId}.");
            }
        }

        /// <summary>
        ///     Kreira novi dokument
        /// </summary>
        /// <param name="dokumentDto">Model dokumenta</param>
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
        public async Task<IActionResult> CreateDokument([FromBody] CreateDokumentDto dokumentDto)
        {
            try
            {
                var document = _mapper.Map<Dokument>(dokumentDto);

                await _dokumentRepository.CreateDokument(document);
                

                return CreatedAtAction(
                    "GetDokumentById",
                    new { dokumentId = document.DokumentId },
                    _mapper.Map<DokumentConfirmation>(document)
                );
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja dokumenta.");
            }
        }


        /// <summary>
        /// Izmena dokumenta
        /// </summary>
        /// <param name="dokument">Model dokumenta</param>
        /// <returns>Potvrda o izmeni dokumenta</returns>
        /// <response code="200">Izmenjen dokument</response>
        /// <response code="404">Nije pronađen dokument za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene </response>
        [Authorize(Roles = "Administrator, Superuser, PrvaKomisija")]
        [HttpPut("{dokumentId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DokumentDto>> UpdateDokument(UpdateDokumentDto dokument)
        {
            try
            {
                var staridok = await _dokumentRepository.GetDokumentById(dokument.DokumentId);

                if (staridok == null)
                {
                   
                    return NotFound();
                }
                

                Dokument novidok = _mapper.Map<Dokument>(dokument);

                _mapper.Map(novidok, staridok);
                await _dokumentRepository.SaveChangesAsync();
               

                return Ok(_mapper.Map<DokumentDto>(staridok));
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene ovlašćenog lica.");
            }
        }



        /// <summary>
        ///     Brisanje dokumenta na osnovu ID-a
        /// </summary>
        /// <param name="dokumentId">ID dokumenta</param>
        /// <response code="204">Dokument je uspešno obrisan</response>
        /// <response code="404">Nije pronadjen dokument za uneti ID</response>
        /// <response code="500">Greška prilikom brisanja dokumenta</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, PrvaKomisija")]
        [HttpDelete("{dokumentId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteDokument(Guid dokumentId)
        {
            try
            {
                var document = await _dokumentRepository.GetDokumentById(dokumentId);

                if (document == null)
                {
                    
                    return NotFound();
                }

                await _dokumentRepository.DeleteDokument(dokumentId);
                

                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom brisanja dokumenta sa id-jem {dokumentId}.");
            }
        }




        /// <summary>
        ///     Vraća opcije za rad sa dokumentima
        /// </summary>
        /// <response code="200">Vraća listu opcija u header-u</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetDokumentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
