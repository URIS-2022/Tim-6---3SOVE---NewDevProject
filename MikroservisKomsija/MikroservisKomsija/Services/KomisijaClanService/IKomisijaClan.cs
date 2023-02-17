using MikroservisKomsija.Models;
namespace MikroservisKomsija.Services.KomisijaClanService
{
    public interface IKomisijaClan
    {
        Task<List<KomisijaClan>> GetAllKCs();
        Task<KomisijaClan?> GetSingleKC(Guid IDKomsije, int IDClan);
        Task<List<KomisijaClan>> AddKC(KomisijaClan komisijaclan);

        Task<List<KomisijaClan>?> UpdateKC(Guid IDKomsije, int IDClan, KomisijaClan request);
        Task<List<KomisijaClan>?> DeleteKC(Guid IDKomsije, int IDClan);
    }
}
