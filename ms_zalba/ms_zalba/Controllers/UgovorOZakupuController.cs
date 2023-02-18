using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using ms_zalba.Data;
using ms_zalba.Entities.UgovorOZakupuE;
using ms_zalba.Models.UgovorOZakupuModel;
using System.Collections.Generic;

namespace ms_zalba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class UgovorOZakupuController : ControllerBase
    {
        private readonly IUgovorOZakupuRepository ugovorOZakupuRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UgovorOZakupuController(IUgovorOZakupuRepository ugovorOZakupuRepository,IMapper mapper )
        {
            this.ugovorOZakupuRepository = ugovorOZakupuRepository;
            this.mapper = mapper;
        }
        /// <summary>
        ///  Vraća sve ugovore o zakupu
        /// </summary>
        /// <returns>Lista ugovora o zakupu</returns>
        /// <response code="200">Vraća listu ugovora o zakupu</response>
        /// <response code="204">Nije pronadjen nijedan ugovor o zakupu</response>
        /// <response code="500">Greška prilikom vraćanja liste ugovora o zakupu</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,TehnickiSekretar")]
        public async Task<ActionResult<List<UgovorOZakupuDto>>> GetAllUgovorOZakupu()
        {
            return Ok(mapper.Map<List<UgovorOZakupuDto>>(await ugovorOZakupuRepository.GetAllUgovorOZakupu()));
        }
        /// <summary>
        /// Vraća jedan ugovor o zakupu na osnovu ID-ja
        /// </summary>
        /// <param name="idUgovor">ID ugovora o zakupu</param>
        /// <returns>Ugovor o zakupu</returns>
        /// <response code="200">Vraća traženi ugovor o zakupu</response>
        /// <response code="404">Nije pronadjen ugovor o zakupu za uneti ID</response>
        /// <response code="500">Greška prilikom vraćanja ugovora o zakupu</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [HttpGet]
        [Route("{idUgovor}")]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,TehnickiSekretar")]
        public async Task<ActionResult<UgovorOZakupuDto>> GetUgovorOZakupueByID(Guid idUgovor)
        {
            var ugovor = await ugovorOZakupuRepository.GetUgovorOZakupuById(idUgovor);
            if (ugovor != null)
            {
                return Ok(mapper.Map<UgovorOZakupuDto>(ugovor));
            }
            else
            {
                return NotFound("Ugovor o zakupu sa prosledjenim id-jem ne postoji.");
            }
        }
        /// <summary>
        /// Kreira novi ugovor o zakupu
        /// </summary>
        /// <param name="addUgovorOZakupuRequest">Model ugovora o zakupu za kreiranje</param>
        /// <returns>Ugovor o zakupu</returns>
        /// <response code="201">Vraća kreirani ugovor o zakupu</response>
        /// <response code="500">Greška prilikom kreiranja ugovora o zakupu</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Administrator,Superuser,TehnickiSekretar")]
        public async Task<ActionResult<UgovorOZakupuConfirmationDto>> AddUgovorOZakupu([FromBody] UgovorOZakupuCreationDto addUgovorOZakupuRequest)
        {
            UgovorOZakupu ugovorNovi = mapper.Map<UgovorOZakupu>(addUgovorOZakupuRequest);
            UgovorOZakupuConfirmation ugovorConfirm = await ugovorOZakupuRepository.AddUgovorOZakupu(ugovorNovi);
            return Ok(mapper.Map<UgovorOZakupuConfirmationDto>(ugovorConfirm));

        }
        /// <summary>
        /// Izmena ugovor o zakupu
        /// </summary>
        /// <param name="idUgovor">ID ugovora o zakupu</param>
        /// <param name="updateUgovorOZakupuRequest">Model ugovora o zakupu za izmenu</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Potvrda o izmeni ugovora o zakupu</response>
        /// <response code="404">Nije pronadjen ugovor o zakupu za uneti ID</response>
        /// <response code="400">ID nije isti kao onaj proledjen u modelu ugovora o zakupu</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        /// <response code="500">Greška prilikom izmene ugovora o zakupu</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("{idUgovor}")]
        [Authorize(Roles = "Administrator,Superuser,TehnickiSekretar")]
        public async Task<ActionResult<UgovorOZakupuConfirmationDto>> UpdateUgovorOZakupu([FromBody] UgovorOZakupuUpdateDto updateUgovorOZakupuRequest, [FromRoute] Guid idUgovor)
        {
            UgovorOZakupu ugovorUpdate = mapper.Map<UgovorOZakupu>(updateUgovorOZakupuRequest);
            UgovorOZakupuConfirmation ugovorConfirm = await ugovorOZakupuRepository.UpdateUgovorOZakupu(ugovorUpdate, idUgovor);

            if (ugovorOZakupuRepository.GetUgovorOZakupuById(updateUgovorOZakupuRequest.idUgovor) != null)
            {
                return Ok(mapper.Map<UgovorOZakupuConfirmationDto>(ugovorConfirm));
            }
            else
            {
                return NotFound("Ugovor o zakupu sa prosledjenim id-jem ne postoji.");

            }
        }

        /// <summary>
        /// Brisanje ugovora o zakupu na osnovu ID-ja
        /// </summary>
        /// <param name="idUgovor">ID ugovora o zakupu</param>
        /// <response code="204">Ugovor o zakupu je uspešno obrisan</response>
        /// <response code="404">Nije pronadjen ugovor o zakupu za uneti ID</response>
        /// <response code="401">Greška prilikom autentifikacije</response>
        /// <response code="500">Greška prilikom brisanja ugovora o zakupu</response>
        [HttpDelete]
        [Route("{idUgovor}")]
        [Authorize(Roles = "Administrator,Superuser,TehnickiSekretar")]
        public async Task<ActionResult<UgovorOZakupu>> DeleteUgovorOZakupu([FromRoute] Guid idUgovor)
        {
            var ugovor = await ugovorOZakupuRepository.DeleteUgovorOZakupu(idUgovor);
            if (ugovor != null)
            {
                return Ok(ugovor);
            }
            else
            {
                return NotFound("Ugovor o zakupu sa prosledjenim id-jem ne postoji.");

            }

        }
    }
}
