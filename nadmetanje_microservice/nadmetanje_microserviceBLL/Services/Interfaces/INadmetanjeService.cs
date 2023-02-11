using nadmetanje_microserviceBLL.Common;
using nadmetanje_microserviceBLL.DTOs.Etapa;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataOut;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.Services.Interfaces
{
    public interface INadmetanjeService
    {
        Task<ResponsePackage<NadmetanjeDataOut>> GetByIdAsync(Guid id);
        Task<ResponsePackageNoData> Remove(Guid id);
        Task<ResponsePackage<List<NadmetanjeDataOut>>> GetAllAsync();
        Task<ResponsePackage<List<NadmetanjeDataOut>>> GetAllByEtapaIdAsync(Guid etapaId);
        Task<ResponsePackageNoData> Save(NadmetanjeDataIn dataIn);
        List<DictionaryItem<string>> GetTipoviForOptions();
        List<DictionaryItem<string>> GetStatusiForOptions();
        List<DictionaryItem<string>> GetKrugForOptions();
        List<DictionaryItem<string>> GetStatusiDrugiKrugForOptions();
    }
}
