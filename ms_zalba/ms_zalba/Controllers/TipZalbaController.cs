using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms_zalba.Data;
using ms_zalba.Entities;
using ms_zalba.Models.TipZalbeModel;
using System.Collections.Generic;

namespace ms_zalba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class TipZalbaController : ControllerBase
    {
        private readonly ITipZalbeRepository tipZalbeRepository;
        private readonly IMapper mapper;

        public TipZalbaController(ITipZalbeRepository tipZalbeRepository,IMapper mapper)
        {
            this.tipZalbeRepository = tipZalbeRepository;
            this.mapper = mapper;
           
        }
        /// <summary>
        /// Vraća sve tipove žalbi
        /// </summary>
        /// <returns>Lista tipova žalbi</returns>
        /// <response code="200">Vraća listu tipova žalbi</response>
        /// <response code="204">Nije pronađen ni jedan tip žalbe</response>
        ///
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer")]
        public async Task<ActionResult<List<TipZalbeDto>>> GetAllTipZalbe()
        {
            var tz= await tipZalbeRepository.GetAllTipZalbe();
            return Ok(mapper.Map<List<TipZalbeDto>>(tz));

        }

        /// <summary>
        /// Vraća jedan tip žalbe na osnovu ID-ja
        /// </summary>
        /// <param name="tipZalbeID">ID tipa žalbe</param>
        /// <returns>Tip žalbe</returns>
        /// <response code="200">Vraća traženi tip žalbe</response>
        /// <response code="404">Nije pronađen tip žalbe za uneti ID</response>
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{tipZalbeID}")]
        [Authorize(Roles = "Administrator,Superuser,Menadzer")]
        public async Task<ActionResult<TipZalbeDto>> GetTipZalbeByID(Guid tipZalbeID)
        {
            var tipZalbe = await tipZalbeRepository.GetTipZalbeById(tipZalbeID);
            if (tipZalbe != null)
            {
                return Ok(mapper.Map<TipZalbeDto>(tipZalbe));
            }
            else
            {
                return NotFound("Tip zalbe sa prosledjenim id-jem ne postoji.");
            }
        }
        /// <summary>
        /// Kreira novi tip žalbe
        /// </summary>
        /// <param name="addTipZalbeRequest">Model tip žalbe</param>
        /// <remarks>
        /// Primer zahteva za kreiranje novog tipa žalbe \
        /// POST /api/TipZalbe \
        /// {   
        ///     "NazivTipaZalbe": "Zalba na Odluku o davanju u zakup"
        ///}
        /// </remarks>
        /// <returns>Potvrda o kreiranju tipa zalbe</returns>
        /// <response code="201">Vraća kreiran tip zalbe</response>
        /// <response code="400">Desila se greška prilikom unosa istih podataka za tip žalbe</response>
        /// <response code="500">Desila se greška prilikom unosa novog tipa žalbe</response>
        /// 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser")]
        public async Task<ActionResult<TipZalbeDto>> AddTipZalbe([FromBody] TipZalbeCreationDto addTipZalbeRequest)
        {
            TipZalbe tipZalbe = await tipZalbeRepository.AddTipZalbe(mapper.Map<TipZalbe>(addTipZalbeRequest));
            return Ok(mapper.Map<TipZalbeDto>(tipZalbe));

        }
        /// <summary>
        /// Izmena tipa žalbe
        /// </summary>
        /// <param name="tipZalbeID">ID tipa žalbe</param>
        /// <param name="updateTipZalbe">Model tip žalbe</param>
        /// <returns>Potvrda o izmeni tipa žalbe</returns>
        /// <response code="200">Izmenjen tip žalbe</response>
        /// <response code="400">Desila se greška prilikom unosa istih podataka za tip žalbe</response>
        /// <response code="404">Nije pronađen tip žalbe za uneti ID</response>
        /// <response code="500">Serverska greška tokom modifikacije tipa žalbe</response>
        /// 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{tipZalbeID}")]
        [Authorize(Roles = "Administrator,Superuser")]
        public async Task<ActionResult<TipZalbe>> UpdateTipZalbe([FromBody] TipZalbeUpdateDto updateTipZalbe, [FromRoute] Guid tipZalbeID)
        {
            var tipZalbe = await tipZalbeRepository.UpdateTipZalbe(mapper.Map<TipZalbe>(updateTipZalbe), tipZalbeID);

            if (tipZalbe != null)
            {
                return Ok(mapper.Map<TipZalbeDto>(tipZalbe));

            }
            else
            {
                return NotFound("Tip zalbe sa prosledjenim id-jem ne postoji.");

            }
        }
        /// <summary>
        /// Brisanje tipa žalbe na osnovu ID-ja
        /// </summary>
        /// <param name="tipZalbeID">ID tipa žalbe</param>
        /// <returns>Status 204 (No Content)</returns>
        /// <response code="204">Tip žalbe je uspešno obrisan</response>
        /// <response code="404">Nije pronađen tip žalbe za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja tipa žalbe</response>
        ///
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{tipZalbeID}")]
        [Authorize(Roles = "Administrator,Superuser ")]
        public async Task<ActionResult<TipZalbe>> DeleteTipZalbe([FromRoute] Guid tipZalbeID)
        {
            var tipZalbe = await tipZalbeRepository.DeleteTipZalbe(tipZalbeID);
            if (tipZalbe != null)
            {
                return Ok(tipZalbe);
            }
            else
            {
                return NotFound("Tip zalbe sa prosledjenim id-jem ne postoji.");

            }

        }
    }
}
