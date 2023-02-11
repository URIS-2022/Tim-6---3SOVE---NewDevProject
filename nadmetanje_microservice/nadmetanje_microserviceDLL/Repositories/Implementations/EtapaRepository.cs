using Microsoft.EntityFrameworkCore;
using nadmetanje_microserviceDAL.Repositories.Interfaces;
using nadmetanje_microserviceDLL.Context;
using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceDAL.Repositories.Implementations
{
    public class EtapaRepository : Repository<Etapa>, IEtapaRepository
    {
        public NadmetanjeContext NadmetanjeContext
        {
            get { return _dbContext as NadmetanjeContext; }
        }
        public EtapaRepository(NadmetanjeContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Etapa>> GetAllByLicitacijaIdAsync(Guid licitacijaId)
        {
            return await _dbContext.Set<Etapa>().Where(x => x.LicitacijaId == licitacijaId && x.IsDeleted == false).ToListAsync();
        }
    }
}
