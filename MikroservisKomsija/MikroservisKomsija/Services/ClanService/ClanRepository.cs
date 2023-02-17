 using MikroservisKomsija.Services;
using MikroservisKomsija.Data;
using MikroservisKomsija.Models;
using MikroservisKomsija.Services.ClanService;

namespace MikroservisKomsija.Services.ClanService
{
    public class ClanRepository : IClan
    {
       
        private readonly DataContext _context;
        public ClanRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<Clan>> AddClan(Clan clan)
        {
            _context.Clans.Add(clan);
            await _context.SaveChangesAsync();
            return await _context.Clans.ToListAsync();
        }
        public async Task<List<Clan>?> DeleteClan(int id)
        {
            var clan = await _context.Clans.FindAsync(id);
            if (clan is null)
                return null;

            _context.Clans.Remove(clan);
            await _context.SaveChangesAsync();
            return await _context.Clans.ToListAsync();
        }
        public async Task<List<Clan>> GetAllClans()
        {
            var clans = await _context.Clans.ToListAsync();
            return clans;
        }
        public async Task<Clan?> GetSingleClan (int id)
        {
            var clan = await _context.Clans.FindAsync(id);
            if (clan is null)
                return null;
            return clan;
        }
        public async Task<List<Clan>?> UpdateClan(int id, Clan request)
        {
            var clan = await _context.Clans.FindAsync(id);
            if (clan is null)
                return null;


            clan.ImeClana = request.ImeClana;
            clan.PrezimeClana = request.PrezimeClana;
            clan.Mjesto = request.Mjesto;
            clan.DatumRodjenja = request.DatumRodjenja;

            await _context.SaveChangesAsync();
            return await _context.Clans.ToListAsync();
        }
    }
}
