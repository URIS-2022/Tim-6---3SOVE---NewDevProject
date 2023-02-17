using MikroservisKomsija.Services;
using MikroservisKomsija.Data;
using MikroservisKomsija.Models;
namespace MikroservisKomsija.Services.KomisijaClanService
{
    public class KomisijaClanRepository : IKomisijaClan
    {

        private readonly DataContext _context;
        public KomisijaClanRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<KomisijaClan>> AddKC(KomisijaClan komisijaclan)
        {
            _context.KCs.Add(komisijaclan);
            await _context.SaveChangesAsync();
            return await _context.KCs.ToListAsync();
        }
        public async Task<List<KomisijaClan>?> DeleteKC(Guid IDKomsije, int IDClan)
        {
            var komisijaclan = await _context.KCs.FindAsync(IDKomsije, IDClan);
            if (komisijaclan is null)
                return null;

            _context.KCs.Remove(komisijaclan);
            await _context.SaveChangesAsync();
            return await _context.KCs.ToListAsync();
        }
        public async Task<List<KomisijaClan>> GetAllKCs()
        {
            var kcs = await _context.KCs.ToListAsync();
            return kcs;
        }
        public async Task<KomisijaClan?> GetSingleKC(Guid IDKomsije, int IDClan)
        {
            var komisijaclan = await _context.KCs.FindAsync(IDKomsije, IDClan);
            if (komisijaclan is null)
                return null;
            return komisijaclan;
        }
        public async Task<List<KomisijaClan>?> UpdateKC(Guid IDKomsije, int IDClan, KomisijaClan request)
        {
            var komisijaclan = await _context.KCs.FindAsync(IDKomsije, IDClan);
            if (komisijaclan is null)
                return null;


            komisijaclan.IsPredsjednik = request.IsPredsjednik;
            

            await _context.SaveChangesAsync();
            return await _context.KCs.ToListAsync();
        }
    }
}
