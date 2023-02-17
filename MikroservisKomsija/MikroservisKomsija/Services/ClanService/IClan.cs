using MikroservisKomsija.Services;
using MikroservisKomsija.Models;
namespace MikroservisKomsija.Services.ClanService
{
    public interface IClan
    {
        Task<List<Clan>> GetAllClans();
        Task<Clan?> GetSingleClan(int id);
        Task<List<Clan>> AddClan(Clan clan);

        Task<List<Clan>?> UpdateClan(int id, Clan request);
        Task<List<Clan>?> DeleteClan(int id);
    }
}
