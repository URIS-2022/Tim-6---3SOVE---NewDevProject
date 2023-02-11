using AutoMapper;
using nadmetanje_microserviceBLL.Common;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataOut;
using nadmetanje_microserviceBLL.Services.Interfaces;
using nadmetanje_microserviceDAL.Repositories.Interfaces;
using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.Services.Implementations
{
    public class EtapaService : IEtapaService
    {
        private readonly IEtapaRepository _etapaRepository;
        private readonly IMapper _mapper;

        public EtapaService(IEtapaRepository etapaRepository, IMapper mapper)
        {
            _etapaRepository = etapaRepository;
            _mapper = mapper;
        }

        public async Task<ResponsePackage<List<EtapaDataOut>>> GetAllAsync()
        {
            var etapeDb = await _etapaRepository.GetAllAsync();

            var dataOut = _mapper.Map<List<EtapaDataOut>>(etapeDb);

            return new ResponsePackage<List<EtapaDataOut>>(dataOut.ToList(),ResponseStatus.OK);
        }
        
        public async Task<ResponsePackage<List<EtapaDataOut>>> GetAllByLicitacijaIdAsync(Guid licitacijaId)
        {
            var etapeDb = await _etapaRepository.GetAllByLicitacijaIdAsync(licitacijaId);

            var dataOut = _mapper.Map<List<EtapaDataOut>>(etapeDb);

            return new ResponsePackage<List<EtapaDataOut>>(dataOut.ToList(),ResponseStatus.OK);
        }

        public async Task<ResponsePackage<EtapaDataOut>> GetByIdAsync(Guid id)
        {
            var etapaDb = await _etapaRepository.GetByIdAsync(id);
            if (etapaDb == null)
                return new ResponsePackage<EtapaDataOut>(ResponseStatus.NotFound, "Etapa nije pronadjena.");
            var dataOut = _mapper.Map<EtapaDataOut>(etapaDb);

            return new ResponsePackage<EtapaDataOut>(dataOut, ResponseStatus.OK);
        }

        public async Task<ResponsePackageNoData> Remove(Guid id)
        {
            var etapaDb = await _etapaRepository.GetByIdAsync(id);
            if(etapaDb == null)
                return new ResponsePackageNoData(ResponseStatus.NotFound,"Etapa nije pronadjena.");
            _etapaRepository.Remove(etapaDb);
            await _etapaRepository.CompleteAsync();

            return new ResponsePackageNoData(ResponseStatus.OK, "Etapa uspesno izbrisana.");
        }

        public async Task<ResponsePackageNoData> Save(EtapaSaveDataIn dataIn)
        {
            var etapa = _mapper.Map<Etapa>(dataIn);
            //create
            if(dataIn.Id == null)
            {
                var newId = Guid.NewGuid();
                while(await _etapaRepository.GetByIdAsync(newId) != null)
                {
                    newId = Guid.NewGuid();
                }
                etapa.Id = newId;
                await _etapaRepository.AddAsync(etapa);
                await _etapaRepository.CompleteAsync();
                return new ResponsePackageNoData(ResponseStatus.OK, "Etapa uspesno kreirana.");
            }
            //update
            var etapaDb = await _etapaRepository.GetByIdAsync(etapa.Id);
            if (etapaDb == null)
                return new ResponsePackageNoData(ResponseStatus.NotFound, "Etapa nije pronadjena u bazi.");
            if (etapaDb.LicitacijaId != etapa.LicitacijaId)
                etapaDb.LicitacijaId = etapa.LicitacijaId;
            if (etapaDb.Datum != etapa.Datum)
                etapaDb.Datum = etapa.Datum;
            if (etapaDb.VremePocetka != etapa.VremePocetka)
                etapaDb.VremePocetka = etapa.VremePocetka;
            if (etapaDb.VremeZavrsetka != etapa.VremeZavrsetka)
                etapaDb.VremeZavrsetka = etapa.VremeZavrsetka;

            await _etapaRepository.CompleteAsync();
            return new ResponsePackageNoData(ResponseStatus.OK, "Etapa uspesno izmenjena.");

        }

        public async Task<ResponsePackage<Guid>> CreateEtapaForConnectionToNadmetanje(EtapaSaveDataIn dataIn)
        {
            var etapa = _mapper.Map<Etapa>(dataIn);
            //create
            var newId = Guid.NewGuid();
            while (await _etapaRepository.GetByIdAsync(newId) != null)
            {
                newId = Guid.NewGuid();
            }
            etapa.Id = newId;
            await _etapaRepository.AddAsync(etapa);
            await _etapaRepository.CompleteAsync();
            return new ResponsePackage<Guid>(etapa.Id,ResponseStatus.OK, "Etapa uspesno kreirana.");
        }
    }
}

