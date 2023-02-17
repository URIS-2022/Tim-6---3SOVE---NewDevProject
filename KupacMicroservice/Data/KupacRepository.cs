using AutoMapper;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.DataContext;
using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using Microsoft.EntityFrameworkCore;
using System;

namespace KupacMicroservice.Data
{
    public class KupacRepository : IKupacRepository
    {

        private readonly KupacDbContext _context;
        private readonly IMapper _mapper;


        public KupacRepository(KupacDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }


        public async Task<KupacConfirmation> CreateKupac(Kupac kupac)
        {

            kupac.KupacId = Guid.NewGuid();
            var kreirankupac = await _context.Kupci.AddAsync(kupac);


            if(kupac.Liciteri != null)
            {

                foreach(var liciterId in kupac.Liciteri)
                {

                    OvlascenoLice kupacliciter = new OvlascenoLice
                    {

                        KupacId = kupac.KupacId,
                        LiciterId = liciterId


                    };
                    _context.OvlascenaLica.Add(kupacliciter);


                }

            }

            await _context.SaveChangesAsync();

            return _mapper.Map<KupacConfirmation>(kreirankupac.Entity);


        }


        public async Task DeleteKupac(Guid kupacId)
        {

            var kupac = await GetKupacById(kupacId);

            _context.Kupci.Remove(kupac);

            await _context.SaveChangesAsync();

        }


        public async Task<List<Kupac>> GetAllKupac()
        {
            
            var kupci = await _context.Kupci.Include(i => i.FizickoLice).Include(i => i.PravnoLice).ToListAsync();

            foreach(var k in kupci)
            {

                k.Liciteri = await _context.OvlascenaLica.Where(ku => ku.KupacId == k.KupacId).Select(pk => pk.LiciterId).ToListAsync();

            }

            return kupci;


        }


        public async Task<Kupac> GetKupacById(Guid kupacId)
        {
            
            var kupac = await _context.Kupci.Include(i => i.PravnoLice).Include(i => i.FizickoLice).Where(i => i.KupacId == kupacId).FirstOrDefaultAsync();

            kupac.Liciteri = await _context.OvlascenaLica.Where(ku => ku.KupacId == kupacId).Select(pk => pk.LiciterId).ToListAsync();

            return kupac;

        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task UpdateKupac(Kupac stariKupac, Kupac noviKupac)
        {


            _context.OvlascenaLica.Where(j => j.KupacId == stariKupac.KupacId).ExecuteDelete();
            _context.Remove(stariKupac);

            _context.SaveChanges();
            _context.Add(noviKupac);


            if(noviKupac.Liciteri != null)
            {
                foreach(var liciterId in noviKupac.Liciteri)
                {

                    OvlascenoLice ovlascenaLica = new OvlascenoLice
                    {

                        KupacId = noviKupac.KupacId,
                        LiciterId = liciterId

                    };
                    _context.OvlascenaLica.Add(ovlascenaLica);

                }


            }

            await _context.SaveChangesAsync();

        }
    }
}
