using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms_parcela.Data;
using ms_parcela.Entities;
using ms_parcela.Models.KatastarskaOpstinaModel;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ms_parcela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class KatastarskaOpstinaController : ControllerBase
    {
        private readonly IKatastarskaOpstinaRepository katastarskaOpstinaRepository;
        private readonly IMapper mapper;
        public KatastarskaOpstinaController(IKatastarskaOpstinaRepository katastarskaOpstinaRepository, IMapper mapper)
        {
            this.katastarskaOpstinaRepository= katastarskaOpstinaRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Vraća sve katastarske opštine
        /// </summary>
        /// <returns>Lista katastarskih opština</returns>
        /// <response code="200">Vraća listu katastarskih opština</response>
        /// <response code="404">Nije pronađena ni jedna katastarska opština</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,OperaterNadmetanja")]
        public async Task<ActionResult<List<KatastarskaOpstinaDto>>> GetAllKatastarskaOpstina()
        {
            var ko= await katastarskaOpstinaRepository.GetAllKatastarskaOpstina();
            return Ok(mapper.Map <List<KatastarskaOpstinaDto>>(ko));
        }
        /// <summary>
        /// Vraća jednu katastarsku opštinu na osnovu broja katastarske opštine
        /// </summary>
        /// <param name="brojKO">Broj katastarske opštine</param>
        /// <returns>Katastarska opština</returns>
        /// <response code="200">Vraća traženu katastarsku opštinu</response>
        /// <response code="404">Nije pronađena katastarska opština za uneti broj</response>
        [HttpGet]
        [Route("{brojKO}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,OperaterNadmetanja")]
        public async Task<ActionResult<KatastarskaOpstinaDto>> GetKatastarskaOpstinaByBroj(Guid brojKO)
        {
            var opstina =await katastarskaOpstinaRepository.GetKatastarskaOpstinaByBroj(brojKO);
            if (opstina != null)
            {
                return Ok(mapper.Map<KatastarskaOpstinaDto>(opstina));
            }
            else
            {
                return NotFound("Katastarska opstina sa prosledjenim brojem ne postoji.");

            }
        }
        /// <summary>
        /// Kreira novu katastarsku opštinu
        /// </summary>
        /// <param name="addKoRequest">Model katastarske opštine</param>
        /// <param name="o">Naziv katastarske opštine</param>
        /// <remarks>
        /// Primer zahteva za kreiranje nove katastarske opštine \
        /// POST /api/KatastarskaOpstina \
        /// {
        ///       "nazivKatastarskeOpstine": "Naziv katastarske opštine"
        /// }
        /// </remarks>
        /// <returns>Potvrda o kreiranju katastarske opštine</returns>
        /// <response code="201">Vraća kreiranu katastarsku opštinu</response>
        /// <responese code="500">Desila se greška prilikom unosa nove katastarske opštine</responese>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<KatastarskaOpstinaDto>> AddKatastarskaOpstina([FromBody]KatastarskaOpstinaCreationDto addKoRequest,opstine o)
        {
            KatastarskaOpstina opstina = await katastarskaOpstinaRepository.AddKatastarskaOpstina(mapper.Map<KatastarskaOpstina>(addKoRequest),o);
            return Ok(mapper.Map<KatastarskaOpstinaDto>(opstina));

        }
        /// <summary>
        /// Izmena katastarske oštine
        /// </summary>
        /// <param name="updateKoRequest">Model katastarske opštine</param>
        ///<param name="brojKO">Broj katastarske opštine koja se menja</param>
        ///<param name="o">Naziv katastarske opštine</param>
        /// <returns>Potvrda o izmeni katastarske opštine</returns>
        /// <response code="200">Izmena katastarske opštine</response>
        /// <response code="404">Nije pronađena katastarska opština za uneti broj</response>
        /// <response code="500">Serverska greška tokom izmene katastarske opštine</response>
        [HttpPut]
        [Route("{brojKO}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<KatastarskaOpstinaDto>> UpdateKatastarskaOpstina([FromBody] KatastarskaOpstinaUpdateDto updateKoRequest, [FromRoute] Guid brojKO,opstine o)
        {
            var opstina =await katastarskaOpstinaRepository.UpdateKatastarskaOpstina(mapper.Map<KatastarskaOpstina>(updateKoRequest),brojKO,o);

            if (opstina != null)
            {
                return Ok(mapper.Map<KatastarskaOpstinaDto>(opstina));
            }
            else
            {
                return NotFound("Katastarska opstina sa prosledjenim brojem ne postoji.");

            }
        }
        /// <summary>
        /// Brisanje katastarske opštine
        /// </summary>
        /// <param name="brojKO"></param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Katastarska opština je uspešno obrisana</response>
        /// <response code="404">Nije pronađena katastarska opština za uneti broj</response>
        /// <response code="500">Serverska greška tokom brisanja katastarske opštine</response>
        [HttpDelete]
        [Route("{brojKO:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<KatastarskaOpstina>> DeleteKatastarskaOpstina([FromRoute] Guid brojKO)
        {
            var opstina = await katastarskaOpstinaRepository.DeleteKatastarskaOpstina(brojKO);
            if (opstina != null)
            {
                return Ok(opstina);
            }
            else
            {
                return NotFound("Katastarska opstina sa prosledjenim brojem ne postoji.");

            }
         
        }
    }
}
