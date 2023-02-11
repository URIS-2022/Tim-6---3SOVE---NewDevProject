using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public class NadmetanjeRepository : Repository<Nadmetanje>, INadmetanjeRepository
    {
        public NadmetanjeContext NadmetanjeContext
        {
            get { return _dbContext as NadmetanjeContext; }
        }
        public NadmetanjeRepository(NadmetanjeContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Nadmetanje>> GetAllByEtapaIdAsync(Guid etapaId)
        {
            return await _dbContext.Set<Nadmetanje>().Where(x => x.EtapaId == etapaId && x.IsDeleted == false).ToListAsync();
        }

        public async Task<Nadmetanje> GetFirstSortedByRedniBroj()
        {
            return await _dbContext.Set<Nadmetanje>()
                .OrderByDescending(x => x.RedniBroj)
                .FirstOrDefaultAsync(x => x.IsDeleted == false);
        }

    }
}
