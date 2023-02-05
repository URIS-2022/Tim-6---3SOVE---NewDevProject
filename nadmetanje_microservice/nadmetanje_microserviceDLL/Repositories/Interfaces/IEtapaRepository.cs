using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceDAL.Repositories.Interfaces
{
    public interface IEtapaRepository : IRepository<Etapa>
    {
        Task<List<Etapa>> GetAllByLicitacijaIdAsync(Guid licitacijaId);
    }
}
