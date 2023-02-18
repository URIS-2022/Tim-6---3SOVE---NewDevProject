using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms_parcela.Data;
using ms_parcela.Entities.ParcelaE;
using ms_parcela.Models.DeoParceleModel;
using ms_parcela.Models.ParcelaModel;

namespace ms_parcela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository parceleRepository;
        private readonly IMapper mapper;

        public ParcelaController(IParcelaRepository parcelaRepository,IMapper mapper)
        {
            this.parceleRepository = parcelaRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Vraća sve parcele
        /// </summary>
        /// <returns>Lista parcela</returns>
        /// <response code="200">Vraća listu parcela</response>
        /// <response code="404">Nije pronađena ni jedna parcela </response>
        ///
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,OperaterNadmetanja")]
        public async Task<ActionResult<List<ParcelaDto>>> GetAllParcela()
        {
            return Ok(mapper.Map<List<ParcelaDto>>(await parceleRepository.GetAllParcela()));
        }
        /// <summary>
        /// Vraća jednu parcelu na osnovu broja opštine u kojoj se parcela nalazi i njenog broja u njoj čija kombinacija predstavlja jedinstveni identifikator svake parcele
        /// </summary>
        /// <param name="brojKo">Broj katastarske opštine</param>
        /// <param name="brojParcele">Broj parcele</param>
        /// <returns>Parcela</returns>
        /// <response code="200">Vraća traženu parcelu</response>
        /// <response code="404">Nije pronađena parcela za uneti broj opštine i broj parcele</response>
        /// 
        [HttpGet]  
        [Route("{brojKo:guid}/{brojParcele}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,OperaterNadmetanja")]
        public async Task<ActionResult<ParcelaDto>> GetParcelaByID(Guid brojKo,int brojParcele)
        {
            var parcela = await parceleRepository.GetParcelaByID(brojKo,brojParcele);
            if (parcela != null)
            {
                return Ok(mapper.Map<ParcelaDto>(parcela));
            }
            else
            {
                return NotFound("Parcela sa prosledjenim broj-jem ne postoji u okviru opstine.");
            }
        }
        /// <summary>
        /// Kreira novu parcelu
        /// </summary>
        /// <param name="addParceleRequest">Model parcele</param>
        /// <param name="k">Kultura parcele</param>
        /// <param name="o">Obradivost parcele</param>
        /// <param name="os">Oblik svojine parcele</param>
        /// <param name="odv">Odvodnjavanje parcele</param>
        /// <param name="kl">Klasa parcele</param>
        /// <remarks>
        /// Primer zahteva za kreiranje nove parcele \
        /// POST /api/Parcela \
        /// {
        ///   "brojParcele": 0,
        ///   "brojKatastraskaOpstina": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///   "povrsina": 0,
        ///    "kultura": "njive",
        ///    "obradivost": "obradivo",
        ///    "klasa": "I",
        ///   "oblikSvojine": "privatna_svojina",
        ///    "odvodnjavanje": "linijsko_odvodnjavanje"
        /// }
        /// </remarks>
        /// <returns>Potvrda o kreiranju parcele</returns>
        /// <response code="201">Vraća kreiranu parcelu </response>
        /// <response code="500">Desila se greška prilikom unosa nove parcele</response>
        /// 
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<ParcelaConfirmationDto>> AddParcela([FromBody] Parcela addParceleRequest, Kultura k, Obradivost o, OblikSvojine os, Odvodnjavanje odv, Klasa kl)
        {
            Parcela novaParcela = mapper.Map<Parcela>(addParceleRequest);
            ParcelaConfirmation parcelaConfirm = await parceleRepository.AddParcela(novaParcela, k,o,os,odv,kl);
            return Ok(mapper.Map<ParcelaConfirmationDto>(parcelaConfirm));

        }
        /// <summary>
        /// Izmena parcele
        /// </summary>
        /// <param name="brojKo">Broj katastarske opštine</param>
        /// <param name="brojParcele">Broj parcele</param>
        /// <param name="updateParcelaRequest">Model parcele</param>
        /// <param name="k">Kultura parcele</param>
        /// <param name="o">Obradivost parcele</param>
        /// <param name="os">Oblik svojine parcele</param>
        /// <param name="odv">Odvodnjavanje parcele</param>
        /// <param name="kl">Klasa parcele</param>
        /// <returns>Potvrda o izmeni parcele</returns>
        /// <response code="200">Izmenjena parcela</response>
        /// <response code="404">Nije pronađena parcela za uneti broj opštine i broj parcele</response>
        /// <response code="500">Serverska greška tokom izmene parcele</response>
        /// 
        [HttpPut]
        [Route("{brojKo:guid}/{brojParcele}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<ParcelaConfirmationDto>> UpdateParcela([FromBody] ParcelaUpdateDto updateParcelaRequest, [FromRoute] Guid brojKo,int brojParcele, Kultura k, Obradivost o, OblikSvojine os, Odvodnjavanje odv, Klasa kl)
        {
            Parcela parcelaUpdate = mapper.Map<Parcela>(updateParcelaRequest);
            ParcelaConfirmation? parcelaConfirm = await parceleRepository.UpdateParcela(parcelaUpdate, brojKo,brojParcele,k,o,os,odv,kl);

            if (parceleRepository.GetParcelaByID(updateParcelaRequest.brojKatastraskaOpstina, updateParcelaRequest.brojParcele) != null)
            {
                return Ok(mapper.Map<ParcelaConfirmationDto>(parcelaConfirm));
            }
            else
            {
                return NotFound("Parcela sa prosledjenim broj-jem ne postoji u okviru opstine.");

            }
        }
        /// <summary>
        /// Brisanje parcele na osnovu broja katastarske opštine i broja parcele
        /// </summary>
        /// <param name="brojKo">Broj katastarske opštine</param>
        /// <param name="brojParcele">Broj parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Parcela je uspešno obrisan</response>
        /// <response code="404">Nije pronađena parcela za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja parcele</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{brojKo:guid}/{brojParcele}")]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<Parcela>> DeleteParcela([FromRoute] Guid brojKo,int brojParcele)
        {
            var parcela = await parceleRepository.DeleteParcela(brojKo,brojParcele);
            if (parcela != null)
            {
                return Ok(parcela);
            }
            else
            {
                return NotFound("Deo parcele sa prosledjenim id-jem ne postoji.");

            }

        }
    }
}
