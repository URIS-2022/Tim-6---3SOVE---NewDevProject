using MikroservisPrijavaNaLicitaciju.Services;
namespace MikroservisPrijavaNaLicitaciju.Services.PrijavaNaLicitacijuService
{
    public class PrijavaNaLicitacijuRepository : IPrijavaNaLicitaciju
    {

        private readonly DataContext _context;
        public PrijavaNaLicitacijuRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<PrijavaNaLicitaciju>> AddPLic(PrijavaNaLicitaciju prijavanalicitaciju)
        {
            _context.PLics.Add(prijavanalicitaciju);
            await _context.SaveChangesAsync();
            return await _context.PLics.ToListAsync();
        }
        public async Task<List<PrijavaNaLicitaciju>?> DeletePLic(Guid id)
        {
            var prijavanalicitaciju = await _context.PLics.FindAsync(id);
            if (prijavanalicitaciju is null)
                return null;

            _context.PLics.Remove(prijavanalicitaciju);
            await _context.SaveChangesAsync();
            return await _context.PLics.ToListAsync();
        }
        public async Task<List<PrijavaNaLicitaciju>> GetAllPLics()
        {
            var prijavanalicitaciju = await _context.PLics.ToListAsync();
            return prijavanalicitaciju;
        }
        public async Task<PrijavaNaLicitaciju?> GetSinglePLic(Guid id)
        {
            var prijavanalicitaciju = await _context.PLics.FindAsync(id);
            if (prijavanalicitaciju is null)
                return null;
            return prijavanalicitaciju;
        }
        public async Task<List<PrijavaNaLicitaciju>?> UpdatePLic(Guid id, PrijavaNaLicitaciju request)
        {
            var prijavanalicitaciju = await _context.PLics.FindAsync(id);
            if (prijavanalicitaciju is null)
                return null;


            prijavanalicitaciju.DatumPrijave = request.DatumPrijave;
            prijavanalicitaciju.TipPrijave = request.TipPrijave;
            prijavanalicitaciju.IznosDepozita = request.IznosDepozita;


            await _context.SaveChangesAsync();
            return await _context.PLics.ToListAsync();
        }
    }
}
