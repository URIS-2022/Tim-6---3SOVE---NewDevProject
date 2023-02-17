using MikroservisKomsija.Services;
using MikroservisKomsija.Models;
namespace MikroservisKomsija.Services.KomisijaSerive
{
    public interface IKomisija
    {
        Task<List<Komisija>> GetAllKoms();
        Task<Komisija?> GetSingleKom(Guid id);
        Task<List<Komisija>> AddKom(Komisija komisija);

        Task<List<Komisija>?> UpdateKom(Guid id, Komisija request);
        Task<List<Komisija>?> DeleteKom(Guid id);
    }
}
