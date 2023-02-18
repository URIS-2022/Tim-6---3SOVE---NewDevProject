using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using nadmetanje_microserviceBLL.Common;
using nadmetanje_microserviceBLL.DTOs.Etapa;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataOut;
using nadmetanje_microserviceBLL.Services.Interfaces;
using nadmetanje_microserviceDLL.Model;
using System.Data;

namespace nadmetanje_microserviceWebApp.Controllers
{
    public class NadmetanjeController : BaseController
    {
        private readonly INadmetanjeService _nadmetanjeService;
        public NadmetanjeController(INadmetanjeService nadmetanjeService)
        {
            _nadmetanjeService = nadmetanjeService;
        }

        /// <summary>
        /// Vraća sva nadmetanja.
        /// </summary>
        /// <returns>Lista nadmetanja</returns>
        /// <response code="200">Vraća listu nadmetanja</response>
        /// <response code="404">Nije pronađeno nijedno nadmetanje</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getAllNadmetanja")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> GetAllNadmetanjes()
        {
            return Ok(await _nadmetanjeService.GetAllAsync());
        }

        /// <summary>
        /// Vraća sva nadmetanja na osnovu id etape.
        /// </summary>
        /// <param name="etapaId">ID etape</param>
        /// <returns>Lista nadmetanja</returns>
        /// <response code="200">Vraća listu nadmetanja sadrzanih na odredjenoj etapi</response>
        /// <response code="404">Nije pronađeno nijedno nadmetanje na specificiranoj etapi</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getAllNadmetanjaByEtapaId/{etapaId}")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> GetAllNadmetanjaByEtapaId(Guid etapaId)
        {
            return Ok(await _nadmetanjeService.GetAllByEtapaIdAsync(etapaId));
        }

        /// <summary>
        /// Kreira novo nadmetanje ili izmjenjuje staro u zavisnosti da li je prosledjen id nadmetanja.
        /// </summary>
        /// <param name="dataIn">Ulazni model nadmetanja</param>
        /// <returns>Potvrdu o kreiranom ili izmjenjenom nadmetanju.</returns>
        /// <remarks>
        /// Primer zahtjeva za kreiranje novog nadmetanja \
        /// POST /api/nadmetanje/save \
        /// {     \
        ///"id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        ///"tip": 1,\
        ///"status": 1,\
        ///"cenaPoHektaru": 50,\
        ///"duzinaZakupa": 2,\
        ///"etapaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        ///"krugNadmetanja": 1,\
        ///"statusDrugiKrug": null\
        ///}
        /// </remarks>
        /// <response code="200">Vraća poruku o uspjesnom kreiranju/izmjeni nadmetanja.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("save")]
        public async Task<ActionResult<ResponsePackageNoData>> Save([FromBody] NadmetanjeDataIn dataIn)
        {
            return Ok(await _nadmetanjeService.Save(dataIn));
        }

        /// <summary>
        /// Vraća jedno nadmetanje na osnovu ID-ja nadmetanja.
        /// </summary>
        /// <param name="NadmetanjeId">ID nadmetanja</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženo nadmetanje</response>
        /// <response code="404">Nije pronađeno nijedno nadmetanje sa specificiranim IDijem</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getNadmetanjeById/{NadmetanjeId}")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> GetNadmetanjeById(Guid NadmetanjeId)
        {
            return Ok(await _nadmetanjeService.GetByIdAsync(NadmetanjeId));
        }

        /// <summary>
        /// Vrši brisanje jednog nadmetanja sistema na osnovu ID-ja nadmetanja.
        /// </summary>
        /// <param name="NadmetanjeId">ID etape</param>
        /// <returns>Poruka o uspjesnom brisanju nadmetanja ili o gresci.</returns>
        /// <response code="204">Nadmetanje uspješno obrisano</response>
        /// <response code="404">Nije pronađeno nadmetanje za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpDelete("remove/{NadmetanjeId}")]
        public async Task<ActionResult<ResponsePackageNoData>> Remove(Guid NadmetanjeId)
        {
            return Ok(await _nadmetanjeService.Remove(NadmetanjeId));
        }

        /// <summary>
        /// Vraća sve tipove nadmetanja (enumeracija).
        /// </summary>
        /// <returns>Lista (key, value) predstavljena enumeracija po vrijednosti i imenu</returns>
        /// <response code="200">Lista (key, value) za tipove nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getTipoviForOptions")]
        public ActionResult<List<DictionaryItem<string>>> GetTipoviForOptions()
        {
            return Ok(_nadmetanjeService.GetTipoviForOptions());
        }

        /// <summary>
        /// Vraća sve statuse nadmetanja (enumeracija).
        /// </summary>
        /// <returns>Lista (key, value) predstavljena enumeracija po vrijednosti i imenu</returns>
        /// <response code="200">Lista (key, value) za statuse nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getStatusiForOptions")]
        public ActionResult<List<DictionaryItem<string>>> GetStatusiForOptions()
        {
            return Ok(_nadmetanjeService.GetStatusiForOptions());
        }

        /// <summary>
        /// Vraća sve krugove nadmetanja (enumeracija).
        /// </summary>
        /// <returns>Lista (key, value) predstavljena enumeracija po vrijednosti i imenu</returns>
        /// <response code="200">Lista (key, value) za krugove nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getKrugForOptions")]
        public ActionResult<List<DictionaryItem<string>>> GetKrugForOptions()
        {
            return Ok(_nadmetanjeService.GetKrugForOptions());
        }

        /// <summary>
        /// Vraća sve statuse drugog kruga nadmetanja (enumeracija).
        /// </summary>
        /// <returns>Lista (key, value) predstavljena enumeracija po vrijednosti i imenu</returns>
        /// <response code="200">Lista (key, value) za statuse drugog kruga nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getStatusiDrugiKrugForOptions")]
        public ActionResult<List<DictionaryItem<string>>> GetStatusiDrugiKrugForOptions()
        {
            return Ok(_nadmetanjeService.GetStatusiDrugiKrugForOptions());
        }

        /// <summary>
        /// Vrši setovanje default-nog tipa nadmetanja.
        /// </summary>
        /// <param name="TipNadmetanja">Vrijednost default-nog tipa</param>
        /// <returns>Poruka o uspjesnom postavljanju default-nog tipa nadmetanja</returns>
        /// <response code="200">Default-ni tip uspjesno postavljen.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom postavljanja default tipa</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("setTipNadmetanjaDefault/{TipNadmetanja}")]
        public ActionResult<ResponsePackageNoData> SetTipNadmetanjaDefault(TipNadmetanja? TipNadmetanja)
        {
            return Ok(_nadmetanjeService.SetTipNadmetanjaDefault(TipNadmetanja));
        }

        /// <summary>
        /// Vrši setovanje default-ne cijene po hektaru nadmetanja.
        /// </summary>
        /// <param name="dataIn">Vrijednost default-ne cijene po hektaru</param>
        /// <returns>Poruka o uspjesnom postavljanju default-ne cijene po hektaru nadmetanja</returns>
        /// <response code="200">default-na cijena po hektaru uspjesno postavljen.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom postavljanja default-ne cijene po hektaru</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("setCenaPoHektaruNadmetanjaDefault/{dataIn}")]
        public ActionResult<ResponsePackageNoData> SetCenaPoHektaruNadmetanjaDefault(double? dataIn)
        {
            return Ok(_nadmetanjeService.SetCenaPoHektaruNadmetanjaDefault(dataIn));
        }

        /// <summary>
        /// Vrši setovanje default-ne duzine zakupa nadmetanja.
        /// </summary>
        /// <param name="dataIn">Vrijednost default-ne duzine zakupa</param>
        /// <returns>Poruka o uspjesnom postavljanju default-ne duzine zakupa nadmetanja</returns>
        /// <response code="200">default-na duzina zakupa uspjesno postavljen.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom postavljanja default-ne duzine zakupa</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("setDuzinaZakupaNadmetanjaDefault/{dataIn}")]
        public ActionResult<ResponsePackageNoData> SetDuzinaZakupaNadmetanjaDefault(int? dataIn)
        {
            return Ok(_nadmetanjeService.SetDuzinaZakupaNadmetanjaDefault(dataIn));
        }

        //[HttpGet("setVrednostJavnogNadmetanja")]
        //public async Task<ActionResult<ResponsePackageNoData>> SetVrednostJavnogNadmetanja()
        //{
        //    return Ok(await _nadmetanjeService.SetVrednostJavnogNadmetanja());
        //}

        /// <summary>
        /// Vraca ukupnu vrijednost javnog nadmetanja.
        /// </summary>
        /// <param name="id">ID javnog nadmetanja</param>
        /// <response code="200">Ukupna vrijednost javnog nadmetanja (double)</response>
        /// <response code="404">Nije pronađeno javno nadmetanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom dobavljanja ukupne vrijednosti javnog nadmetanja.</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getVrednostJavnogNadmetanja/{id}")]
        public async Task<ActionResult<ResponsePackage<double>>> GetVrednostJavnogNadmetanja(Guid id)
        {
            return Ok(await _nadmetanjeService.GetVrednostJavnogNadmetanja(id));
        }

        /// <summary>
        /// Kreira novu etapu i povezuje selektovana nadmetanja sa njom
        /// </summary>
        /// <param name="dataIn">Ulazni model sastavljen iz dva dijela : EtapaSaveDataIn kao i lista idijeva selektovanih nadmetanja.</param>
        /// <returns>Potvrdu o kreiranoj etapi i povezivanju sa selektovanim nadmetanjima.</returns>
        /// <remarks>
        /// Primer zahtjeva za kreiranje etape i povezivanje sa selektovanim nadmetanjima \
        /// POST /api/nadmetanje/createEtapaAndConnectToNadmetanja \
        ///{\\
        ///  "etapaInfos": {\
        ///    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        ///    "licitacijaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        ///    "datum": "2023-02-17T23:30:35.541Z",\
        ///    "vremePocetka": "00:00:00",\
        ///    "vremeZavrsetka": "00:00:00"\
        ///  },\
        ///  "nadmetanjaIds": [\
        ///    "3fa85f64-5717-4562-b3fc-2c963f66afa6"\
        ///  ]\
        /// }
        /// </remarks>
        /// <response code="200">Vraća poruku o uspjesnom kreiranju etape i povezivanju sa selektovanim nadmetanjima.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranju etape i povezivanju sa selektovanim nadmetanjima</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("createEtapaAndConnectToNadmetanja")]
        public async Task<ActionResult<List<NadmetanjeDataOut>>> CreateEtapaAndConnectToNadmetanja([FromBody] CreateEtapaAndConnectToNadmetanjaDataIn dataIn)
        {
            return Ok(await _nadmetanjeService.CreateEtapaAndConnectToNadmetanja(dataIn));
        }

        /// <summary>
        /// Setovanje statusa selektovanog nadmetanja
        /// </summary>
        /// <param name="dataIn">Ulazni model SetTipNadmetanjaDataIn<T>.</param>
        /// <returns>Potvrdu o Setovanju statusa selektovanog nadmetanja </returns>
        /// <remarks>
        /// Primer zahtjeva za Setovanje statusa selektovanog nadmetanja \
        /// POST /api/nadmetanje/setStatusNadmetanja \
        ///{\
        /// "enumeracija": 1,\
        /// "nadmetanjeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        /// "kupacId": null\
        /// }\
        /// </remarks>
        /// <response code="200">Vraća poruku o uspjesnom Setovanju statusa selektovanog nadmetanja.</response>
        /// <response code="500">Došlo je do greške na serveru setovanja statusa setovanog nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("setStatusNadmetanja")]
        public async Task<ActionResult<ResponsePackageNoData>> SetStatusNadmetanja([FromBody] SetTipNadmetanjaDataIn<StatusNadmetanja> dataIn)
        {
            return Ok(await _nadmetanjeService.SetStatusNadmetanja(dataIn));
        }

        /// <summary>
        /// Setovanje statusa drugog kruga selektovanog nadmetanja
        /// </summary>
        /// <param name="dataIn">Ulazni model SetTipNadmetanjaDataIn<T>.</param>
        /// <returns>Potvrdu o Setovanju statusa drugog kruga selektovanog nadmetanja </returns>
        /// <remarks>
        /// Primer zahtjeva za Setovanje statusa drugog kruga selektovanog nadmetanja \
        /// POST /api/nadmetanje/setStatusDrugiKrugNadmetanja \
        ///{\
        /// "enumeracija": 1,\
        /// "nadmetanjeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        /// "kupacId": null\
        /// }\
        /// </remarks>
        /// <response code="200">Vraća poruku o uspjesnom Setovanju statusa drugog kruga selektovanog nadmetanja.</response>
        /// <response code="500">Došlo je do greške na serveru setovanja statusa drugog kruga setovanog nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("setStatusDrugiKrugNadmetanja")]
        public async Task<ActionResult<ResponsePackageNoData>> SetStatusDrugiKrugNadmetanja([FromBody] SetTipNadmetanjaDataIn<StatusDrugiKrug> dataIn)
        {
            return Ok(await _nadmetanjeService.SetStatusDrugiKrugNadmetanja(dataIn));
        }

        /// <summary>
        /// Setovanje kruga selektovanog nadmetanja
        /// </summary>
        /// <param name="dataIn">Ulazni model SetTipNadmetanjaDataIn<T>.</param>
        /// <returns>Potvrdu o Setovanju kruga selektovanog nadmetanja </returns>
        /// <remarks>
        /// Primer zahtjeva za Setovanje kruga selektovanog nadmetanja \
        /// POST /api/nadmetanje/setKrugNadmetanja \
        ///{\
        /// "enumeracija": 1,\
        /// "nadmetanjeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
        /// "kupacId": null\
        /// }\
        /// </remarks>
        /// <response code="200">Vraća poruku o uspjesnom Setovanju kruga selektovanog nadmetanja.</response>
        /// <response code="500">Došlo je do greške na serveru setovanja kruga setovanog nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje")]
        [HttpPost("setKrugNadmetanja")]
        public async Task<ActionResult<ResponsePackageNoData>> SetKrugNadmetanja([FromBody] SetTipNadmetanjaDataIn<KrugNadmetanja> dataIn)
        {
            return Ok(await _nadmetanjeService.SetKrugNadmetanja(dataIn));
        }

        /// <summary>
        /// Vraća sva nadmetanja na osnovu prosledjene vrijednosti enumeracije.
        /// </summary>
        /// <param name="dataIn">vrijednost enumeracije status nadmetanja</param>
        /// <returns>Lista nadmetanja</returns>
        /// <response code="200">Vraća listu nadmetanja koja su odredjenog statusa</response>
        /// <response code="404">Nije pronađeno nijedno nadmetanje sa specificiranim statusom</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getAllByStatusNadmetanja/{dataIn}")]
        public async Task<ActionResult<ResponsePackage<List<Nadmetanje>>>> GetAllByStatusNadmetanja(StatusNadmetanja dataIn)
        {
            return Ok(await _nadmetanjeService.GetAllByStatusNadmetanja(dataIn));
        }

        /// <summary>
        /// Vraća sva nadmetanja na osnovu prosledjene vrijednosti enumeracije.
        /// </summary>
        /// <param name="dataIn">vrijednost enumeracije statusa drugog kruga nadmetanja</param>
        /// <returns>Lista nadmetanja</returns>
        /// <response code="200">Vraća listu nadmetanja koja su odredjenog statusa drugog kruga</response>
        /// <response code="404">Nije pronađeno nijedno nadmetanje sa specificiranim statusom drugog kruga</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getAllByStatusDrugiKrugAsync/{dataIn}")]
        public async Task<ActionResult<ResponsePackage<List<Nadmetanje>>>> GetAllByStatusDrugiKrugAsync(StatusDrugiKrug dataIn)
        {
            return Ok(await _nadmetanjeService.GetAllByStatusDrugiKrugAsync(dataIn));
        }

        /// <summary>
        /// Vraća sva nadmetanja na osnovu prosledjene vrijednosti enumeracije.
        /// </summary>
        /// <param name="dataIn">vrijednost enumeracije krug nadmetanja</param>
        /// <returns>Lista nadmetanja</returns>
        /// <response code="200">Vraća listu nadmetanja koja su odredjenog kruga</response>
        /// <response code="404">Nije pronađeno nijedno nadmetanje sa specificiranim krugom</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getAllByKrugNadmetanjaAsync/{dataIn}")]
        public async Task<ActionResult<ResponsePackage<List<Nadmetanje>>>> GetAllByStatusDrugiKrugAsync(KrugNadmetanja dataIn)
        {
            return Ok(await _nadmetanjeService.GetAllByKrugNadmetanjaAsync(dataIn));
        }

        /// <summary>
        /// Inicira se proces pokretanja drugog kruga.
        /// </summary>
        /// <returns>Poruka o uspjehu</returns>
        /// <response code="200">Drugi krug uspjesno pokrenut.</response>
        /// <response code="500">Drugi krug nije pokrenut radi greske servera</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("pokretanjeDrugogKruga")]
        public async Task<ActionResult<ResponsePackageNoData>> PokretanjeDrugogKruga()
        {
            return Ok(await _nadmetanjeService.PokretanjeDrugogKruga());
        }

        /// <summary>
        /// Vraća ukupnu zakupljenu povrsinu za odredjenog kupca.
        /// </summary>
        /// <param name="kupacId">ID kupca</param>
        /// <returns>(double) ukupnu povrsinu koju kupac ima zakupljenu na prethodnim nadmetanjima.</returns>
        /// <response code="200">(double) Ukupnu povrsinu koju kupac ima zakupljenu na prethodnim nadmetanjima.</response>
        /// <response code="404">Nije pronađeno nijedno nadmetanje sa specificiranim IDijem kupca</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getUkupnaZakupljenaPovrsinaByKupacId/{kupacId}")]
        public async Task<ActionResult<ResponsePackage<double>>> GetUkupnaZakupljenaPovrsinaByKupacId(Guid kupacId)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            return Ok(await _nadmetanjeService.GetUkupnaZakupljenaPovrsinaByKupacId(kupacId, token));
        }

        /// <summary>
        /// Vraća maksimalnu povrsinu predvidjenu programom za dato nadmetanje.
        /// </summary>
        /// <param name="nadmetanjeId">ID nadmetanja</param>
        /// <returns>(double) maksimalnu povrsinu predvidjenu programom za dato nadmetanje.</returns>
        /// <response code="200">(double) maksimalnu povrsinu predvidjenu programom za dato nadmetanje.</response>
        /// <response code="404">Nije pronađeno nijedno nadmetanje sa specificiranim IDijem nadmetanja</response>
        [Authorize(Roles = "Administrator, Superuser, OperaterNadmetanje, Menadzer")]
        [HttpGet("getMaksimalnaPovrsina/{nadmetanjeId}")]
        public async Task<ActionResult<ResponsePackage<double>>> GetMaksimalnaPovrsina(Guid nadmetanjeId)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            return Ok(await _nadmetanjeService.GetMaksimalnaPovrsina(nadmetanjeId, token));
        }
    }
}
