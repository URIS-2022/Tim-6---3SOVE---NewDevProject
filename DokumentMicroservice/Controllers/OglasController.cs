using AutoMapper;
using DokumentMicroservice.Data;
using DokumentMicroservice.Data.Interfaces;
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Models;
using DokumentMicroservice.Models.Oglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Routing;
using DokumentMicroservice.Models.PredlogPlanaProjekta;
using DokumentMicroservice.Services.Mock;
using DokumentMicroservice.Services;

namespace DokumentMicroservice.Controllers
{

    /// <summary>
    /// kontroler za oglase
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/oglas")]
    public class OglasController : ControllerBase
    {

        private readonly IOglasRepository _oglasRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IServiceCall<ZalbaDto> _mikroservisZalba;

        public OglasController(IMapper mapper, IOglasRepository oglasRepository, IConfiguration configuration, IServiceCall<ZalbaDto> mikroservisZalba)
        {
            _mapper = mapper;
            _oglasRepository = oglasRepository;
            _configuration = configuration;
            _mikroservisZalba = mikroservisZalba;


        }

        /// <summary>
        ///     Vraća sve oglase
        /// </summary>
        /// <returns>Lista oglasa</returns>
        /// <response code="200">Vraća listu oglasa</response>
        /// <response code="204">Nije pronadjen nijedan oglas</response>
        /// <response code="500">Greška prilikom vraćanja liste oglasa</response>
        ///// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<OglasDto>>> GetAllOglas()
        {
            try
            {
                var oglasi = await _oglasRepository.GetAllOglas();

                if (oglasi == null || oglasi.Count == 0)
                {
                    
                    return NoContent();
                }
                
                var oglasiDto = new List<OglasDto>();
                string url = _configuration["Services:MikroservisZalba"];
                foreach(var oglas in oglasi)
                {
                    var oglasDto = _mapper.Map<OglasDto>(oglas);
                    if (oglas.zalbaID is not null)
                    {
                        var zalbaDto = await _mikroservisZalba.SendGetRequestAsync(url + oglas.zalbaID);
                        if(zalbaDto is not null)
                        {
                            oglasDto.Zalba = zalbaDto.Naziv + ", "
                                             + zalbaDto.Obrazlozenje + ",";
                                             
                                                
                        }


                    }
                    oglasiDto.Add(oglasDto);
                }



                return Ok(oglasiDto);
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Greška prilikom vraćanja liste oglasa.");
            }
        }

        /// <summary>
        ///     Vraća jedan oglas na osnovu ID-a
        /// </summary>
        /// <param name="id">ID  oglasa </param>
        /// <returns>Dokument</returns>
        /// <response code="200">Vraća traženi oglas</response>
        /// <response code="404">Nije pronadjen oglas za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja oglasa</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpGet("{oglasId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OglasDto>> GetOglasById(Guid oglasId)
        {
            try
            {
                var oglasi = await _oglasRepository.GetOglasById(oglasId);

                if (oglasi == null)
                {
                    
                    return NotFound();
                }

               

                return Ok(_mapper.Map<OglasDto>(oglasi));
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom vraćanja dokumenta sa id-jem {oglasId}.");
            }
        }

        /// <summary>
        ///     Kreira novi oglas
        /// </summary>
        /// <param name="oglasDto">Model oglasa</param>
        /// <returns>Dokument</returns>
        /// <response code="201">Vraća kreirani oglas</response>
        /// <response code="500">Greška prilikom kreiranja oglasa</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, PrvaKomisija")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateOglas([FromBody] CreateOglasDto oglasDto)
        {
            try
            {
                var oglas = _mapper.Map<Oglas>(oglasDto);

                await _oglasRepository.CreateOglas(oglas);


                return CreatedAtAction(
                    "GetOglasById",
                    new { oglasId = oglas.OglasId },
                    _mapper.Map<OglasConfirmation>(oglas)
                );
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja oglasa.");
            }
        }


        /// <summary>
        /// Izmena oglasa
        /// </summary>
        /// <param name="oglas">Model oglasa</param>
        /// <returns>Potvrda o izmeni oglasa</returns>
        /// <response code="200">Izmenjen oglas</response>
        /// <response code="404">Nije pronađen oglas za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene oglasa </response>
        [Authorize(Roles = "Administrator, Superuser,  PrvaKomisija,Manager, OperaterNadmetanja")]
        [HttpPut("{oglasId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OglasDto>> UpdateOglas(UpdateOglasDto oglas)
        {
            try
            {
                var starioglas = await _oglasRepository.GetOglasById(oglas.OglasId);

                if (starioglas == null)
                {

                    return NotFound();
                }


                Oglas novioglas = _mapper.Map<Oglas>(oglas);

                _mapper.Map(novioglas, starioglas);
                await _oglasRepository.SaveChangesAsync();


                return Ok(_mapper.Map<OglasDto>(starioglas));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom izmene oglasa.");
            }
        }




        /// <summary>
        ///     Brisanje oglasa na osnovu ID-a
        /// </summary>
        /// <param name="oglasId">ID oglasa</param>
        /// <response code="204">Oglas je uspešno obrisan</response>
        /// <response code="404">Nije pronadjen oglas za uneti ID</response>
        /// <response code="500">Greška prilikom brisanja oglasa</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, PrvaKomisija")]
        [HttpDelete("{oglasId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteDokument(Guid oglasId)
        {
            try
            {
                var oglasi = await _oglasRepository.GetOglasById(oglasId);

                if (oglasi == null)
                {
                    
                    return NotFound();
                }

                await _oglasRepository.DeleteOglas(oglasId);


     
                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Greška prilikom brisanja oglasa sa id-jem {oglasId}.");
            }
        }





        /// <summary>
        ///     Vraća opcije za rad sa oglasima
        /// </summary>
        /// <response code="200">Vraća listu opcija u header-u</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [Authorize(Roles = "Administrator, Superuser, Menadzer, PrvaKomisija")]
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetOglasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }






    }
}
