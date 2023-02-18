using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using nadmetanje_microserviceBLL.Common;
using nadmetanje_microserviceBLL.DTOs;
using nadmetanje_microserviceBLL.DTOs.Etapa;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataOut;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataOut;
using nadmetanje_microserviceBLL.Services.Interfaces;
using nadmetanje_microserviceDAL.Repositories.Implementations;
using nadmetanje_microserviceDAL.Repositories.Interfaces;
using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.Services.Implementations
{
    public class NadmetanjeService : INadmetanjeService
    {
        private readonly INadmetanjeRepository _nadmetanjeRepository;
        private readonly IEtapaService _etapaService;
        private readonly IMapper _mapper;
        private readonly IHttpService<double> _httpService;
        private readonly IConfiguration _configuration;

        public NadmetanjeService(IConfiguration configuration,INadmetanjeRepository nadmetanjeRepository, IMapper mapper, IEtapaService etapaService, IHttpService<double> httpService)
        {
            _nadmetanjeRepository = nadmetanjeRepository;
            _etapaService = etapaService;
            _mapper = mapper;
            _httpService = httpService;
            _configuration = configuration;
        }
        public async Task<ResponsePackage<List<NadmetanjeDataOut>>> GetAllAsync()
        {
            var nadmetanjaDb = await _nadmetanjeRepository.GetAllAsync();

            var dataOut = _mapper.Map<List<NadmetanjeDataOut>>(nadmetanjaDb);

            return new ResponsePackage<List<NadmetanjeDataOut>>(dataOut.ToList(), ResponseStatus.OK);
        }

        public async Task<ResponsePackage<List<NadmetanjeDataOut>>> GetAllByEtapaIdAsync(Guid etapaId)
        {
            var nadmetanjaDb = await _nadmetanjeRepository.GetAllByEtapaIdAsync(etapaId);

            var dataOut = _mapper.Map<List<NadmetanjeDataOut>>(nadmetanjaDb);

            return new ResponsePackage<List<NadmetanjeDataOut>>(dataOut.ToList(), ResponseStatus.OK);
        }

        public async Task<ResponsePackage<NadmetanjeDataOut>> GetByIdAsync(Guid id)
        {
            var nadmetanjeDb = await _nadmetanjeRepository.GetByIdAsync(id);
            if (nadmetanjeDb == null)
                return new ResponsePackage<NadmetanjeDataOut>(ResponseStatus.NotFound, "Nadmetanje nije pronadjeno.");
            var dataOut = _mapper.Map<NadmetanjeDataOut>(nadmetanjeDb);

            return new ResponsePackage<NadmetanjeDataOut>(dataOut, ResponseStatus.OK);
        }

        public async Task<ResponsePackageNoData> Remove(Guid id)
        {
            var nadmetanjeDb = await _nadmetanjeRepository.GetByIdAsync(id);
            if (nadmetanjeDb == null)
                return new ResponsePackageNoData(ResponseStatus.NotFound, "Nadmetanje nije pronadjeno.");
            _nadmetanjeRepository.Remove(nadmetanjeDb);
            await _nadmetanjeRepository.CompleteAsync();

            return new ResponsePackageNoData(ResponseStatus.OK, "Nadmetanje uspesno izbrisano.");
        }

        private string getRedniBroj(Nadmetanje firstByRedniBroj)
        {
            int rB = 0;
            if (firstByRedniBroj == null)
                return "001";
            else
            {
                if (int.TryParse(firstByRedniBroj.RedniBroj, out rB))
                {
                    rB++;
                    return rB.ToString().PadLeft(3, '0');
                }
                else
                    return "001";
            }
        }


        public async Task<ResponsePackageNoData> Save(NadmetanjeDataIn dataIn)
        {
            var firstByRedniBroj = await _nadmetanjeRepository.GetFirstSortedByRedniBroj();
            string redniBroj = getRedniBroj(firstByRedniBroj);
            var nadmetanje = _mapper.Map<Nadmetanje>(dataIn);
            //create
            if (dataIn.Id == null)
            {
                var newId = Guid.NewGuid();
                while (await _nadmetanjeRepository.GetByIdAsync(newId) != null)
                {
                    newId = Guid.NewGuid();
                }
                nadmetanje.Id = newId;
                nadmetanje.RedniBroj = redniBroj;
                //Default vrijednosti top priority
                if (TipNadmetanjaTopPriority.TipNadmetanjaTop != null)
                    nadmetanje.Tip = TipNadmetanjaTopPriority.TipNadmetanjaTop.Value;
                else if (dataIn.Tip == null)
                    nadmetanje.Tip = TipNadmetanja.JavnoNadmetanje;
                if (TipNadmetanjaTopPriority.CenaPoHektaru != null)
                    nadmetanje.CenaPoHektaru = TipNadmetanjaTopPriority.CenaPoHektaru.Value;
                if (TipNadmetanjaTopPriority.DuzinaZakupa != null)
                    nadmetanje.DuzinaZakupa = TipNadmetanjaTopPriority.DuzinaZakupa.Value;

                await _nadmetanjeRepository.AddAsync(nadmetanje);
                await _nadmetanjeRepository.CompleteAsync();
                return new ResponsePackageNoData(ResponseStatus.OK, "Nadmetanje uspesno kreirano.");
            }
            //update
            var nadmetanjeDb = await _nadmetanjeRepository.GetByIdAsync(nadmetanje.Id);
            if (nadmetanjeDb == null)
                return new ResponsePackageNoData(ResponseStatus.NotFound, "Nadmetanje nije pronadjeno u bazi.");
            UpdateNadmetanje(nadmetanje,nadmetanjeDb);

            await _nadmetanjeRepository.CompleteAsync();
            return new ResponsePackageNoData(ResponseStatus.OK, "nadmetanje uspesno izmenjena.");
        }

        private void UpdateNadmetanje(Nadmetanje nadmetanje, Nadmetanje nadmetanjeDb)
        {
            if (nadmetanjeDb.EtapaId != nadmetanje.EtapaId)
                nadmetanjeDb.EtapaId = nadmetanje.EtapaId;
            if (nadmetanjeDb.Tip != nadmetanje.Tip)
                nadmetanjeDb.Tip = nadmetanje.Tip;
            if (nadmetanjeDb.Status != nadmetanje.Status)
                nadmetanjeDb.Status = nadmetanje.Status;
            if (nadmetanjeDb.CenaPoHektaru != nadmetanje.CenaPoHektaru)
                nadmetanjeDb.CenaPoHektaru = nadmetanje.CenaPoHektaru;
            if (nadmetanjeDb.DuzinaZakupa != nadmetanje.DuzinaZakupa)
                nadmetanjeDb.DuzinaZakupa = nadmetanje.DuzinaZakupa;
            //if (nadmetanjeDb.RedniBroj != nadmetanje.RedniBroj)
            //    nadmetanjeDb.RedniBroj = nadmetanje.RedniBroj;
            if (nadmetanjeDb.KrugNadmetanja != nadmetanje.KrugNadmetanja)
                nadmetanjeDb.KrugNadmetanja = nadmetanje.KrugNadmetanja;
            if (nadmetanjeDb.StatusDrugiKrug != nadmetanje.StatusDrugiKrug)
                nadmetanjeDb.StatusDrugiKrug = nadmetanje.StatusDrugiKrug;
        }

        public List<DictionaryItem<string>> GetTipoviForOptions()
        {
            string[] tipovi = Enum.GetNames(typeof(TipNadmetanja));
            return tipovi.Select((name) => new DictionaryItem<string> { Key = (int)Enum.Parse(typeof(TipNadmetanja), name), Value = name }).ToList();
        }

        public List<DictionaryItem<string>> GetStatusiForOptions()
        {
            string[] tipovi = Enum.GetNames(typeof(StatusNadmetanja));
            return tipovi.Select((name) => new DictionaryItem<string> { Key = (int)Enum.Parse(typeof(StatusNadmetanja), name), Value = name }).ToList();
        }

        public List<DictionaryItem<string>> GetKrugForOptions()
        {
            string[] tipovi = Enum.GetNames(typeof(KrugNadmetanja));
            return tipovi.Select((name) => new DictionaryItem<string> { Key = (int)Enum.Parse(typeof(KrugNadmetanja), name), Value = name }).ToList();
        }
        public List<DictionaryItem<string>> GetStatusiDrugiKrugForOptions()
        {
            string[] tipovi = Enum.GetNames(typeof(StatusDrugiKrug));
            return tipovi.Select((name) => new DictionaryItem<string> { Key = (int)Enum.Parse(typeof(StatusDrugiKrug), name), Value = name }).ToList();
        }

        public ResponsePackageNoData SetTipNadmetanjaDefault(TipNadmetanja? dataIn)
        {
            TipNadmetanjaTopPriority.TipNadmetanjaTop = dataIn;
            return new ResponsePackageNoData(ResponseStatus.OK, "Uspjesno postavljen defoltni tip nadmetanja.");
        }
        public ResponsePackageNoData SetCenaPoHektaruNadmetanjaDefault(double? dataIn)
        {
            TipNadmetanjaTopPriority.CenaPoHektaru = dataIn;
            return new ResponsePackageNoData(ResponseStatus.OK, "Uspjesno postavljen defoltni tip nadmetanja.");
        }
        public ResponsePackageNoData SetDuzinaZakupaNadmetanjaDefault(int? dataIn)
        {
            TipNadmetanjaTopPriority.DuzinaZakupa = dataIn;
            return new ResponsePackageNoData(ResponseStatus.OK, "Uspjesno postavljen defoltni tip nadmetanja.");
        }

        public async Task<ResponsePackage<double>> GetVrednostJavnogNadmetanja(Guid id)
        {
            // dobavljamo iz servisa za parcele
            var ukupnaPovrsina = 1;
            //
            var javnoNadmetanje = await _nadmetanjeRepository.GetByIdAsync(id);
            if(javnoNadmetanje == null)
                return new ResponsePackage<double>(ResponseStatus.NotFound, "Javno nadmetanje nije pronadjeno.");
            var vrednostJavnogNadmetanja = (ukupnaPovrsina * javnoNadmetanje.CenaPoHektaru) * javnoNadmetanje.DuzinaZakupa;
            return new ResponsePackage<double>(vrednostJavnogNadmetanja, ResponseStatus.OK);
        }

        //public async Task<ResponsePackageNoData> SetVrednostJavnogNadmetanja(VrednostJavnogNadmetanjaDataIn dataIn)
        //{
        //    var javnoNadmetanje = await _nadmetanjeRepository.GetByIdAsync(dataIn.JavnoNadmetanjeId);
        //    if (javnoNadmetanje == null)
        //        return new ResponsePackageNoData(ResponseStatus.NotFound, "Javno nadmetanje nije pronadjeno.");
        //    javnoNadmetanje.VrednostJavnogNadmetanja = dataIn.VrednostJavnogNadmetanja;
        //    await _nadmetanjeRepository.CompleteAsync();
        //    return new ResponsePackageNoData(ResponseStatus.OK, "Vrijednost javnog nadmetanja uspjesno setovana.");
        //}

        public async Task<ResponsePackageNoData> CreateEtapaAndConnectToNadmetanja(CreateEtapaAndConnectToNadmetanjaDataIn dataIn)
        {
            var etapaId = await _etapaService.CreateEtapaForConnectionToNadmetanje(dataIn.EtapaInfos);
            await _nadmetanjeRepository.SetEtapaIdToAllNadmetanjaByIds(dataIn.NadmetanjaIds, etapaId.Data);
            return new ResponsePackageNoData(ResponseStatus.OK, "Uspjesno kreirana etapa i povezana sa selektovanim nadmetanjima.");
        }

        public async Task<ResponsePackageNoData> SetStatusNadmetanja(SetTipNadmetanjaDataIn<StatusNadmetanja> dataIn)
        {
            var nadmetanje = await _nadmetanjeRepository.GetByIdAsync(dataIn.NadmetanjeId);
            if(nadmetanje == null)
                return new ResponsePackageNoData(ResponseStatus.NotFound, "Nadmetanje nije pronadjeno.");
            if (dataIn.Enumeracija == StatusNadmetanja.ZavrsenoUpsesno && dataIn.KupacId == null)
                return new ResponsePackageNoData(ResponseStatus.BadRequest, "Id kupca pobjednika je obavezan ukoliko je uspjesno zavrseno nadmetanje.");
            nadmetanje.Status = dataIn.Enumeracija;
            nadmetanje.KupacId = dataIn.KupacId;
            await _nadmetanjeRepository.CompleteAsync();
            return new ResponsePackageNoData(ResponseStatus.OK, "Uspjesno setovan novi status nadmetanju.");
        }

        public async Task<ResponsePackage<List<Nadmetanje>>> GetAllByStatusNadmetanja(StatusNadmetanja dataIn)
        {
            var nadmetanja = await _nadmetanjeRepository.GetAllByStatusAsync(dataIn);
            return new ResponsePackage<List<Nadmetanje>>(nadmetanja,ResponseStatus.OK);
        }

        public async Task<ResponsePackageNoData> SetStatusDrugiKrugNadmetanja(SetTipNadmetanjaDataIn<StatusDrugiKrug> dataIn)
        {
            var nadmetanje = await _nadmetanjeRepository.GetByIdAsync(dataIn.NadmetanjeId);
            if (nadmetanje == null)
                return new ResponsePackageNoData(ResponseStatus.NotFound, "Nadmetanje nije pronadjeno.");
            nadmetanje.StatusDrugiKrug = dataIn.Enumeracija;
            await _nadmetanjeRepository.CompleteAsync();
            return new ResponsePackageNoData(ResponseStatus.OK, "Uspjesno setovan novi status drugog kruga nadmetanju.");
        }

        public async Task<ResponsePackage<List<Nadmetanje>>> GetAllByStatusDrugiKrugAsync(StatusDrugiKrug dataIn)
        {
            var nadmetanja = await _nadmetanjeRepository.GetAllByStatusDrugiKrugAsync(dataIn);
            return new ResponsePackage<List<Nadmetanje>>(nadmetanja, ResponseStatus.OK);
        }

        public async Task<ResponsePackageNoData> SetKrugNadmetanja(SetTipNadmetanjaDataIn<KrugNadmetanja> dataIn)
        {
            var nadmetanje = await _nadmetanjeRepository.GetByIdAsync(dataIn.NadmetanjeId);
            if (nadmetanje == null)
                return new ResponsePackageNoData(ResponseStatus.NotFound, "Nadmetanje nije pronadjeno.");
            nadmetanje.KrugNadmetanja = dataIn.Enumeracija;
            await _nadmetanjeRepository.CompleteAsync();
            return new ResponsePackageNoData(ResponseStatus.OK, "Uspjesno setovan novi status drugog kruga nadmetanju.");
        }

        public async Task<ResponsePackage<List<Nadmetanje>>> GetAllByKrugNadmetanjaAsync(KrugNadmetanja dataIn)
        {
            var nadmetanja = await _nadmetanjeRepository.GetAllByKrugNadmetanjaAsync(dataIn);
            return new ResponsePackage<List<Nadmetanje>>(nadmetanja, ResponseStatus.OK);
        }

        public async Task<ResponsePackageNoData> PokretanjeDrugogKruga()
        {
            await _nadmetanjeRepository.PokretanjeDrugogKruga();
            return new ResponsePackageNoData(ResponseStatus.OK, "Drugi krug uspjesno pokrenut");
        }

        public async Task<ResponsePackage<double>> GetUkupnaZakupljenaPovrsinaByKupacId(Guid kupacId, string token)
        {
            var nadmetanjaIds = await _nadmetanjeRepository.GetAllNadmetanjeIdsByKupacId(kupacId);
            if (nadmetanjaIds.Count == 0 || nadmetanjaIds == null)
                return new ResponsePackage<double>(ResponseStatus.NotFound, "Nepostoji nijedno nadmetanje dobijeno od strane specificiranog kupca.");
            //Dobavljam iz servisa za parcele
            StringBuilder sb = new StringBuilder();
            nadmetanjaIds.ForEach(x => sb.Append(x.ToString()+','));
            double ukupnaPovrsinaKupca = await _httpService
                .SendGetRequestAsync(_configuration["GatewayUrl"] + "DeoParcele/nadmetanja/" + sb, token); ;
            //
            return new ResponsePackage<double>(ukupnaPovrsinaKupca, ResponseStatus.OK);
        }

        public async Task<ResponsePackage<double>> GetMaksimalnaPovrsina(Guid nadmetanjeId, string token)
        {
            var nadmetanje = await _nadmetanjeRepository.GetByIdAsync(nadmetanjeId, x => x.Etapa);
            if (nadmetanje == null)
                return new ResponsePackage<double>(ResponseStatus.NotFound, "Nije pronadjeno nijedno nadmetanje sa specificiranim idijem.");
            var licitacijaId = nadmetanje.Etapa.LicitacijaId;
            //Dobavljam iz servisa za licitaciju
            double maksimalnaPovrsina = await _httpService
                .SendGetRequestAsync(_configuration["GatewayUrl"] + "licitacija/maksimalnaPovrsina/" + licitacijaId, token);
            //
            return new ResponsePackage<double>(maksimalnaPovrsina, ResponseStatus.OK);
        }


    }
}
