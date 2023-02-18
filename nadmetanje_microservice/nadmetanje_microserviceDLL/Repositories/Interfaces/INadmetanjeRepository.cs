using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceDAL.Repositories.Interfaces
{
    public interface INadmetanjeRepository : IRepository<Nadmetanje>
    {
        Task<List<Nadmetanje>> GetAllByEtapaIdAsync(Guid etapaId);
        Task<List<Nadmetanje>> GetAllByStatusAsync(StatusNadmetanja status);
        Task<List<Nadmetanje>> GetAllByStatusDrugiKrugAsync(StatusDrugiKrug status);
        Task<List<Nadmetanje>> GetAllByKrugNadmetanjaAsync(KrugNadmetanja krug);
        Task PokretanjeDrugogKruga();
        Task<Nadmetanje> GetFirstSortedByRedniBroj();
        Task SetEtapaIdToAllNadmetanjaByIds(List<Guid> nadmetanjaIds, Guid etapaId);
        Task<List<Guid>> GetAllNadmetanjeIdsByKupacId(Guid kupacId);
    } 
}
