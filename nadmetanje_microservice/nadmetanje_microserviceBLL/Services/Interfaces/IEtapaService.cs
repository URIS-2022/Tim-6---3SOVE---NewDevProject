using nadmetanje_microserviceBLL.Common;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataOut;
using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.Services.Interfaces
{
    public interface IEtapaService
    {
        Task<ResponsePackage<EtapaDataOut>> GetByIdAsync(Guid id);
        Task<ResponsePackageNoData> Remove(Guid id);
        Task<ResponsePackage<List<EtapaDataOut>>> GetAllAsync();
        Task<ResponsePackage<List<EtapaDataOut>>> GetAllByLicitacijaIdAsync(Guid licitacijaId);
        Task<ResponsePackageNoData> Save(EtapaSaveDataIn dataIn);
        Task<ResponsePackage<Guid>> CreateEtapaForConnectionToNadmetanje(EtapaSaveDataIn dataIn);
    }
}
