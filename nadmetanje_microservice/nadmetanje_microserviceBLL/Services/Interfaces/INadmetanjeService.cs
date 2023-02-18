using Microsoft.AspNetCore.Mvc;
using nadmetanje_microserviceBLL.Common;
using nadmetanje_microserviceBLL.DTOs.Etapa;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataOut;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataOut;
using nadmetanje_microserviceDLL.Model;
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
        ResponsePackageNoData SetTipNadmetanjaDefault(TipNadmetanja? dataIn);
        ResponsePackageNoData SetCenaPoHektaruNadmetanjaDefault(double? dataIn);
        ResponsePackageNoData SetDuzinaZakupaNadmetanjaDefault(int? dataIn);
        //Task<ResponsePackageNoData> SetVrednostJavnogNadmetanja(VrednostJavnogNadmetanjaDataIn dataIn);
        Task<ResponsePackage<double>> GetVrednostJavnogNadmetanja(Guid id);
        Task<ResponsePackageNoData> CreateEtapaAndConnectToNadmetanja(CreateEtapaAndConnectToNadmetanjaDataIn dataIn);

        //Enumeracije
        Task<ResponsePackageNoData> SetStatusNadmetanja(SetTipNadmetanjaDataIn<StatusNadmetanja> dataIn);
        Task<ResponsePackage<List<Nadmetanje>>> GetAllByStatusNadmetanja(StatusNadmetanja dataIn);
        Task<ResponsePackageNoData> SetStatusDrugiKrugNadmetanja(SetTipNadmetanjaDataIn<StatusDrugiKrug> dataIn);
        Task<ResponsePackage<List<Nadmetanje>>> GetAllByStatusDrugiKrugAsync(StatusDrugiKrug dataIn);
        Task<ResponsePackageNoData> SetKrugNadmetanja(SetTipNadmetanjaDataIn<KrugNadmetanja> dataIn);
        Task<ResponsePackage<List<Nadmetanje>>> GetAllByKrugNadmetanjaAsync(KrugNadmetanja dataIn);
        //

        Task<ResponsePackageNoData> PokretanjeDrugogKruga();
        //komunikacija sa drugim servisima
        Task<ResponsePackage<double>> GetUkupnaZakupljenaPovrsinaByKupacId(Guid kupacId, string token);
        Task<ResponsePackage<double>> GetMaksimalnaPovrsina(Guid nadmetanjeId, string token);

    }
}
