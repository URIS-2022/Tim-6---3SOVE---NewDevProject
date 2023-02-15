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
        
        public async Task<List<Nadmetanje>> GetAllByStatusAsync(StatusNadmetanja status)
        {
            return await _dbContext.Set<Nadmetanje>().Where(x => x.Status == status && x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Nadmetanje>> GetAllByStatusDrugiKrugAsync(StatusDrugiKrug status)
        {
            return await _dbContext.Set<Nadmetanje>().Where(x => x.StatusDrugiKrug == status && x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Nadmetanje>> GetAllByKrugNadmetanjaAsync(KrugNadmetanja krug)
        {
            return await _dbContext.Set<Nadmetanje>().Where(x => x.KrugNadmetanja == krug && x.IsDeleted == false).ToListAsync();
        }

        public async Task<Nadmetanje> GetFirstSortedByRedniBroj()
        {
            return await _dbContext.Set<Nadmetanje>()
                .OrderByDescending(x => x.RedniBroj)
                .FirstOrDefaultAsync(x => x.IsDeleted == false);
        }

        public async Task SetEtapaIdToAllNadmetanjaByIds(List<Guid> nadmetanjaIds, Guid etapaId)
        {
            var nadmetanja = _dbContext.Set<Nadmetanje>()
                                .Where(x => x.IsDeleted == false && nadmetanjaIds.Contains(x.Id));
            foreach(var nadmetanje in nadmetanja)
            {
                nadmetanje.EtapaId = etapaId;
            }
            await _dbContext.SaveChangesAsync();                    
                
        }

        public async Task PokretanjeDrugogKruga()
        {
            var nadmetanjaKojaSeIzbacuju = _dbContext.Set<Nadmetanje>()
                .Where(x => x.IsDeleted == false && x.Status != StatusNadmetanja.ZavrsenoNeuspesno && x.Status != StatusNadmetanja.ZavrsenoUpsesno);
            foreach(var nadmetanje in nadmetanjaKojaSeIzbacuju)
            {
                nadmetanje.IsDeleted = true;
            }
            var nadmetanjaDrugiKrug = _dbContext.Set<Nadmetanje>()
                .Where(x => x.IsDeleted == false && x.Status == StatusNadmetanja.ZavrsenoNeuspesno);
            foreach (var nadmetanje in nadmetanjaDrugiKrug)
            {
                nadmetanje.KrugNadmetanja = KrugNadmetanja.Drugi;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Guid>> GetAllNadmetanjeIdsByKupacId(Guid kupacId)
        {
            var ids = await _dbContext.Set<Nadmetanje>()
                .Where(x => x.KupacId == kupacId && x.IsDeleted == false && x.Status == StatusNadmetanja.ZavrsenoUpsesno).Select(x => x.Id)
                .ToListAsync();
            return ids;
        }

    }
}
