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
        Task<List<Nadmetanje>> GetAllByEtapaIdAsync(Guid licitacijaId);
        Task<Nadmetanje> GetFirstSortedByRedniBroj();
    } 
}
