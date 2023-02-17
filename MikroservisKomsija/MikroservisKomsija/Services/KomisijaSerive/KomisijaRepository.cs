using MikroservisKomsija.Data;
using MikroservisKomsija.Models;
namespace MikroservisKomsija.Services.KomisijaSerive
{
    public class KomisijaRepository : IKomisija
    {

        private readonly DataContext _context;
        public KomisijaRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<Komisija>> AddKom(Komisija komisija)
        {
            _context.Koms.Add(komisija);
            await _context.SaveChangesAsync();
            return await _context.Koms.ToListAsync();
        }
        public async Task<List<Komisija>?> DeleteKom(Guid id)
        {
            var komisija = await _context.Koms.FindAsync(id);
            if (komisija is null)
                return null;

            _context.Koms.Remove(komisija);
            await _context.SaveChangesAsync();
            return await _context.Koms.ToListAsync();
        }
        public async Task<List<Komisija>> GetAllKoms()
        {
            var koms = await _context.Koms.ToListAsync();
            return koms;
        }
        public async Task<Komisija?> GetSingleKom(Guid id)
        {
            var komisija = await _context.Koms.FindAsync(id);
            if (komisija is null)
                return null;
            return komisija;
        }
        public async Task<List<Komisija>?> UpdateKom(Guid id, Komisija request)
        {
            var komisija = await _context.Koms.FindAsync(id);
            if (komisija is null)
                return null;


            komisija.ImeKomisije = request.ImeKomisije;
            komisija.Ovlascenje = request.Ovlascenje;
            komisija.OznakaKomisije = request.OznakaKomisije;

            await _context.SaveChangesAsync();
            return await _context.Koms.ToListAsync();
        }
    }
}
