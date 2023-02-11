using AutoMapper;
using nadmetanje_microserviceBLL.Common;
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
        private readonly IMapper _mapper;

        public NadmetanjeService(INadmetanjeRepository nadmetanjeRepository, IMapper mapper)
        {
            _nadmetanjeRepository = nadmetanjeRepository;
            _mapper = mapper;
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

        public async Task<ResponsePackageNoData> Save(NadmetanjeDataIn dataIn)
        {
            string redniBroj = "";
            int rB = 0;
            var firstByRedniBroj = await _nadmetanjeRepository.GetFirstSortedByRedniBroj();
            if (firstByRedniBroj == null)
                redniBroj = "001";
            else
            {
                if (int.TryParse(firstByRedniBroj.RedniBroj, out rB))
                {
                    rB++;
                    redniBroj = rB.ToString().PadLeft(3, '0');
                }
            }
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
                await _nadmetanjeRepository.AddAsync(nadmetanje);
                await _nadmetanjeRepository.CompleteAsync();
                return new ResponsePackageNoData(ResponseStatus.OK, "Nadmetanje uspesno kreirano.");
            }
            //update
            var nadmetanjeDb = await _nadmetanjeRepository.GetByIdAsync(nadmetanje.Id);
            if (nadmetanjeDb == null)
                return new ResponsePackageNoData(ResponseStatus.NotFound, "Nadmetanje nije pronadjeno u bazi.");
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
            if (nadmetanjeDb.RedniBroj != nadmetanje.RedniBroj)
                nadmetanjeDb.RedniBroj = nadmetanje.RedniBroj;
            if (nadmetanjeDb.KrugNadmetanja != nadmetanje.KrugNadmetanja)
                nadmetanjeDb.KrugNadmetanja = nadmetanje.KrugNadmetanja;
            if (nadmetanjeDb.StatusDrugiKrug != nadmetanje.StatusDrugiKrug)
                nadmetanjeDb.StatusDrugiKrug = nadmetanje.StatusDrugiKrug;

            await _nadmetanjeRepository.CompleteAsync();
            return new ResponsePackageNoData(ResponseStatus.OK, "nadmetanje uspesno izmenjena.");
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
    }
}
