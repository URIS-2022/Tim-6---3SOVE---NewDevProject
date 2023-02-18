using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ms_parcela.Data;
using ms_parcela.Entities.DeoParceleE;
using ms_parcela.Models.DeoParceleModel;
using ms_parcela.Models.ExternalServices;
using ms_parcela.ServiceCalls;
using System.Runtime.Intrinsics.Arm;
using Microsoft.Net.Http.Headers;

namespace ms_parcela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json","application/xml")]
    public class DeoParceleController : ControllerBase
    {
        private readonly IDeoParceleRepository deoParceleRepository;
        private readonly IMapper mapper;
        private readonly IServiceCall<NadmetanjeDto> nadmetanjeService;
        private readonly IConfiguration configuration;

        public DeoParceleController(IDeoParceleRepository deoParceleRepository,IMapper mapper,IServiceCall<NadmetanjeDto> nadmetanjeService,IConfiguration configuration)
        {
            this.deoParceleRepository = deoParceleRepository;
            this.mapper = mapper;
            this.nadmetanjeService=nadmetanjeService;
            this.configuration=configuration;
        }
        /// <summary>
        /// Vraća sve delove parcela
        /// </summary>
        /// <returns>Lista delova parcela</returns>
        /// <response code="200">Vraća listu delova parcela</response>
        /// <response code="404">Nije pronadjen ni jedan deo parcele</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,OperaterNadmetanja")]
        public async Task<ActionResult<List<DeoParceleDto>>> GetAllDeoParcele()
        {
            //var deoParcele = mapper.Map<List<DeoParceleDto>>(await deoParceleRepository.GetAllDeoParcele());
           
            var deloviParcele = await deoParceleRepository.GetAllDeoParcele();
            
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var deloviParceleDto=new List<DeoParceleDto>();
            string url = configuration["Services:NadmetanjeService"];
            foreach(var deoParcele in deloviParcele)
            {
                var deoParceleDto=mapper.Map<DeoParceleDto>(deoParcele);
                if(deoParcele.NadmetanjeId is not null)
                {
                    var nadmetanjeDto = await nadmetanjeService.SendGetRequestAsync(url + deoParcele.NadmetanjeId, token);
                    if (nadmetanjeDto is not null)
                        deoParceleDto.Nadmetanje = nadmetanjeDto;
                }
                deloviParceleDto.Add(deoParceleDto);
            }
            return Ok(deloviParceleDto);
            //return Ok(deoParcele);

            
        }
        /// <summary>
        /// Vraća jedan deo parcele na osnovu IDija dela
        /// </summary>
        /// <param name="deoParceleID">ID dela parcele</param>
        /// <returns>Deo parcele</returns>
        /// <response code="200">Vraća traženi deo parcele</response>
        /// <response code="404">Nije pronadjen deo parcele za uneti ID</response>
        [HttpGet]
        [Route("{deoParceleID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Superuser,Menadzer,OperaterNadmetanja")]
        public async Task<ActionResult<DeoParceleDto>> GetDeoParceleById(Guid deoParceleID)
        {
            var deoParcele = await deoParceleRepository.GetDeoParceleById(deoParceleID);
            if (deoParcele != null)
            {

                /*var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                string url = configuration["Services:NadmetanjeService"];
                var deoParceleDto = mapper.Map<DeoParceleDto>(deoParcele);
                if (deoParcele.NadmetanjeId is not null)
                {
                    var nadmetanjeDto = await nadmetanjeService.SendGetRequestAsync(url + deoParcele.NadmetanjeId, token);
                    if (nadmetanjeDto is not null)
                        deoParceleDto.Nadmetanje = nadmetanjeDto;
                }*/
                return Ok(deoParcele);
            }
            else
            {
                return NotFound("Deo parcele sa prosledjenim id-jem ne postoji.");
            }
        }
        /// <summary>
        /// Kreira novi deo parcele
        /// </summary>
        /// <param name="addDeoPRequest">Model deo parcele</param>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dela parcele \
        /// POST /api/addDeoPRequest \
        /// {   "redniBrojDelaParcele": 0,
        ///     "povrsinaDelaParcele": 0,
        ///     "brojKatastraskaOpstina": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     "brojParcele": 0,
        ///     "nadmetanjeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///}
        /// </remarks>
        /// <returns>Potvrda o kreiranju dela parcele</returns>
        /// <response code="201">Vraća kreirani deo parcele</response>
        /// <response code="500">Desila se greška prilikom unosa novog dela parcele</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<DeoParceleConfirmationDto>> AddDeoParcele([FromBody] DeoParceleCreationDto addDeoPRequest)
        {
            DeoParcele deoParcele = mapper.Map<DeoParcele>(addDeoPRequest);
            DeoParceleConfirmation deoParceleConfirm = await deoParceleRepository.AddDeoParcele(deoParcele);
            return Ok(mapper.Map<DeoParceleConfirmationDto>(deoParceleConfirm));

        }
        /// <summary>
        /// Izmena dela parcele
        /// </summary>
        /// <param name="deoParceleID">ID dela parcele</param>
        /// <param name="updateDeoPRequest">Model deo parcele</param>
        /// <returns>Potvrda o izmeni dela parcele</returns>
        /// <response code="200">Izmenjeni deo parcele</response>
        /// <response code="404">Nije pronadjen deo parcele za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene dela parcele</response>
        [HttpPut]
        [Route("{deoParceleID}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<DeoParceleConfirmationDto>> UpdateDeoParcele([FromBody] DeoParceleUpdateDto updateDeoPRequest, [FromRoute] Guid deoParceleID)
        {
            DeoParcele deoParcele = mapper.Map<DeoParcele>(updateDeoPRequest);
            DeoParceleConfirmation deoParceleConfirm = await deoParceleRepository.UpdateDeoParcele(deoParcele, deoParceleID);

            if (deoParceleRepository.GetDeoParceleById(updateDeoPRequest.deoParceleID) != null)
            {
                return Ok(mapper.Map<DeoParceleConfirmationDto>(deoParceleConfirm));
            }
            else
            {
                return NotFound("Deo parcele sa prosledjenim id-jem ne postoji.");

            }
        }
        /// <summary>
        /// Izmena dela parcele
        /// </summary>
        /// <param name="deoParceleID">ID dela parcele</param>
        /// <returns>Potvrda o izmeni dela parcele</returns>
        /// <response code="200">Izmenjeni deo parcele</response>
        /// <response code="404">Nije pronađen deo parcele za uneti ID</response>
        /// <response code="500">Serverska greška tokom izmene dela parcele</response>
        [HttpDelete]
        [Route("{deoParceleID:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<DeoParcele>> DeleteDeoParcele([FromRoute] Guid deoParceleID)
        {
            var deoParcele = await deoParceleRepository.DeleteDeoParcele(deoParceleID);
            if (deoParcele != null)
            {
                return Ok(deoParcele);
            }
            else
            {
                return NotFound("Deo parcele sa prosledjenim id-jem ne postoji.");

            }

        }
        /// <summary>
        /// Računanje ukupne povšine delova za prosledjenu listu nadmetanja
        /// </summary>
        /// <param name="nadmetanjaIds">Lista id-jeva nadmetanja</param>
        /// <returns>Potvrda o sabranoj površini</returns>
        /// <response code="200">Sabrane površine delova</response>
        /// <response code="404">Nije pronadjena lista nadmetanja</response>
        /// <response code="500">Serverska greška tokom računanja površine za nadmetanja</response>
        [HttpGet("nadmetanja")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator,Superuser,OperaterNadmetanja")]
        public async Task<ActionResult<int>> GetUkupnaPovrsinaDelovaByNadmetanjeId([FromQuery]List<Guid?> nadmetanjaIds)
        {
            return Ok(await deoParceleRepository.GetUkupnaPovrsinaDelovaByNadmetanjeId(nadmetanjaIds));
        }
    }
}
