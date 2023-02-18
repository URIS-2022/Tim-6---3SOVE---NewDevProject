using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ms_zalba.Data;
using ms_zalba.Entities.ZalbaE;
using ms_zalba.Models.TipZalbeModel;
using ms_zalba.Models.ZalbaModel;
using Microsoft.AspNetCore.Authorization;

namespace ms_zalba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class ZalbaController : ControllerBase
    {
        private readonly IZalbaRepository zalbaRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public ZalbaController(IZalbaRepository zalbaRepository,IMapper mapper,IConfiguration configuration)
        {
            this.zalbaRepository = zalbaRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        /// <summary>
        /// Vraća sve žalbe
        /// </summary>
        /// <returns>Lista žalbi</returns>
        /// <response code="200">Vraća listu žalbi</response>
        /// <response code="404">Nije pronađena ni jedna žalba</response>
        /// 
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer")]
        public async Task<ActionResult<List<ZalbaDto>>> GetAllZalba()
        {
            return Ok(mapper.Map<List<ZalbaDto>>(await zalbaRepository.GetAllZalba()));
        }
        /// <summary>
        /// Vraća jednu žalbu na osnovu ID-ja
        /// </summary>
        /// <param name="zalbaID">ID žalbe</param>
        /// <returns>Žalba</returns>
        /// <response code="200">Vraća traženu žalbu</response>
        /// <response code="404">Nije pronađena žalba za uneti ID</response>
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{zalbaID}")]
        [Authorize(Roles = "Administrator,Superuser,Menadzer")]
        public async Task<ActionResult<TipZalbeDto>> GetTipZalbeByID(Guid zalbaID)
        {
            var zalba = await zalbaRepository.GetZalbaById(zalbaID);
            if (zalba != null)
            {
                return Ok(mapper.Map<ZalbaDto>(zalba));
            }
            else
            {
                return NotFound("Zalba sa prosledjenim id-jem ne postoji.");
            }
        }
        /// <summary>
        /// Kreira novu žalbu
        /// </summary>
        /// <param name="addZalbaRequest">Model žalba</param>
        /// <param name="sz">Status žalbe</param>
        /// <param name="radnjaNaOsnovuZalbe">Radnja na osnovu žalbe</param>
        /// <remarks>
        /// Primer zahteva za kreiranje nove žalbe \
        /// POST /api/Zalba \
        /// {   
        ///     "tipZalbe": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     "datumPodnosenjaZalbe": "2023-02-17",
        ///     "razlogZalbe": "string",
        ///     "obrazlozenje": "string",
        ///     "datumResenja": "2023-02-17",
        ///     "brojResenja": "string",
        ///     "statusZalbe": "usvojena",
        ///     "brojNadmetanja": 0,
        ///     "radnjaNaOsnovuZalbe": "JN_ide_u_drugi_krug_sa_novim_uslovima",
        ///     "podnosilacZalbe": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///}
        /// </remarks>
        /// <returns>Potvrda o kreiranju žalbe</returns>
        /// <response code="201">Vraća kreiranu žalbu</response>
        /// <response code="500">Desila se greška prilikom unosa nove žalbe</response>
        /// 
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser")]
        public async Task<ActionResult<ZalbaConfirmationDto>> AddZalba([FromBody] ZalbaCreationDto addZalbaRequest, StatusZalbe sz, RadnjaNaOsnovuZalbe radnjaNaOsnovuZalbe)
        {
            Zalba zalbaNova = mapper.Map<Zalba>(addZalbaRequest);
            ZalbaConfirmation zalbaConfirm = await zalbaRepository.AddZalba(zalbaNova,sz,radnjaNaOsnovuZalbe);
            return Ok(mapper.Map<ZalbaConfirmationDto>(zalbaConfirm));

        }
        /// <summary>
        /// Izmena žalbe
        /// </summary>
        /// <param name="zalbaID">ID žalbe</param>
        /// <param name="updateZalbaRequest">Model žalbe</param>
        /// <param name="sz">Status žalbe</param>
        /// <param name="radnjaNaOsnovuZalbe">Radnja na osnovu žalbe</param>
        /// <returns>Potvrda o izmeni žalbe</returns>
        /// <response code="200">Izmenjena žalba</response>
        /// <response code="400">Desila se greška prilikom unosa istih podataka za žalbu</response>
        /// <response code="404">Nije pronađena žalba za uneti ID</response>
        /// <response code="500">Serverska greška tokom modifikacije žalbe</response>
        /// 
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{zalbaID}")]
        [Authorize(Roles = "Administrator,Superuser")]
        public async Task<ActionResult<ZalbaConfirmationDto>> UpdateZalba([FromBody] ZalbaUpdateDto updateZalbaRequest, [FromRoute] Guid zalbaID, StatusZalbe sz, RadnjaNaOsnovuZalbe radnjaNaOsnovuZalbe)
        {
            Zalba zalbaUpdate = mapper.Map<Zalba>(updateZalbaRequest);
            ZalbaConfirmation zalbaConfirm = await zalbaRepository.UpdateZalba(zalbaUpdate, zalbaID, sz,radnjaNaOsnovuZalbe);

            if (zalbaRepository.GetZalbaById(updateZalbaRequest.zalbaID) != null)
            {
                return Ok(mapper.Map<ZalbaConfirmationDto>(zalbaConfirm));
            }
            else
            {
                return NotFound("Žalba sa prosledjenim id-jem ne postoji.");

            }
        }
        /// <summary>
        /// Brisanje žalbe na osnovu ID-ja
        /// </summary>
        /// <param name="zalbaID">ID žalbe</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Žalba je uspešno obrisana</response>
        /// <response code="404">Nije pronađena žalba za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja žalbe</response>
        /// 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{zalbaID}")]
        [Authorize(Roles = "Administrator,Superuser")]
        public async Task<ActionResult<Zalba>> DeleteZalba([FromRoute] Guid zalbaID)
        {
            var zalba = await zalbaRepository.DeleteZalba(zalbaID);
            if (zalba != null)
            {
                return Ok(zalba);
            }
            else
            {
                return NotFound("Zalba sa prosledjenim id-jem ne postoji.");

            }

        }
    }
}
