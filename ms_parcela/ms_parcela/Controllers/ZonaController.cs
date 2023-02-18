using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms_parcela.Data;
using ms_parcela.Entities;
using ms_parcela.Models.ZonaModel;


namespace ms_parcela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class ZonaController : ControllerBase
    {
        private readonly IZonaRepository zonaRepository;
        private readonly IMapper mapper;

        public ZonaController(IZonaRepository zonaRepository,IMapper mapper)
        {
            this.zonaRepository = zonaRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Vraća sve zone u kojima su parcele
        /// </summary>
        /// <returns>Lista zona</returns>
        /// <response code="200">Vraća listu zona</response>
        /// <response code="404">Nije pronađena ni jedna zona</response>
        /// 
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles="Administrator,Superuser,Menadzer,OperaterNadmetanja")]
        public async Task<ActionResult<List<ZonaDto>>> GetAllZona()
        {
            return Ok(mapper.Map<List<ZonaDto>>(await zonaRepository.GetAllZona()));
        }
        /// <summary>
        /// Vraća jednu zonu na osnovu ID-ja
        /// </summary>
        /// <param name="zonaID">ID zone</param>
        /// <returns>Zona</returns>
        /// <response code="200">Vraća traženu zonu</response>
        /// <response code="404">Nije pronađena zona za uneti ID</response>
        [HttpGet]
        [Route("{zonaID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,OperaterNadmetanja")]
        public async Task<ActionResult<ZonaDto>> GetZonaById(Guid zonaID)
        {
            var zona = await zonaRepository.GetZonaById(zonaID);
            if (zona != null)
            {
                return Ok(mapper.Map<ZonaDto>(zona));
            }
            else
            {
                return NotFound("Zona sa prosledjenim id-jem ne postoji.");

            }
        }
        /// <summary>
        /// Kreira novu  zonu
        /// </summary>
        /// <param name="addZonaRequest">Model zone</param>
        /// <param name="brz">Mogući brojevi zona </param>
        /// <remarks>
        /// Primer zahteva za kreiranje nove zone \
        /// POST /api/Zona \
        /// {
        ///     "brojZona": "zona1"
        /// }
        /// </remarks>
        /// <returns>Potvrda o kreiranju zone</returns>
        /// <response code="201">Vraća kreiranu zonu</response>
        /// <response code="500">Desila se greška prilikom unosa nove zone</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<ZonaDto>> AddZona([FromBody] ZonaCreationDto addZonaRequest, brojevi_zona brz)
        {
            Zona zona = await zonaRepository.AddZona(mapper.Map<Zona>(addZonaRequest),brz);
            return Ok(mapper.Map<ZonaDto>(zona));
        }
        /// <summary>
        /// Izmena zone
        /// </summary>
        /// <param name="updateZona">Model zone</param>
        /// <param name="zonaID">ID zone</param>
        /// <param name="brz">Mogući brojevi zona </param>
        /// <returns>Potvrda o izmeni zone</returns>
        /// <response code="200">Izmenjena zona</response>
        /// <response code="404">Nije pronađena zona za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene zone</response>
        /// 
        [HttpPut]
        [Route("{zonaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<ZonaDto>> UpdateZona([FromBody] ZonaUpdateDto updateZona, [FromRoute] Guid zonaID, brojevi_zona brz)
        {
            var zona = await zonaRepository.UpdateZona(mapper.Map<Zona>(updateZona), zonaID, brz);

            if (zona != null)
            {
                return Ok(mapper.Map<ZonaDto>(zona));
            }
            else
            {
                return NotFound("Zona sa prosledjenim id-jem ne postoji.");

            }
        }
        /// <summary>
        /// Brisanje zone na osnovu ID-ja
        /// </summary>
        /// <param name="zonaID">ID zone</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Zona je uspešno obrisana</response>
        /// <response code="404">Nije pronađena zona za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja zone</response>
        /// 
        [HttpDelete]
        [Route("{zonaID:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<Zona>> DeleteZona([FromRoute] Guid zonaID)
        {
            var zona = await zonaRepository.DeleteZona(zonaID);
            if (zona != null)
            {
                return Ok(zona);
            }
            else
            {
                return NotFound("Zona sa prosledjenim id-jem ne postoji.");

            }

        }
    }
}
