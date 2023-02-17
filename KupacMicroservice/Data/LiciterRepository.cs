using AutoMapper;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.DataContext;
using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using Microsoft.EntityFrameworkCore;
using System;

namespace KupacMicroservice.Data
{
    public class LiciterRepository : ILiciterRepository
    {

        private readonly KupacDbContext _context;
        private readonly IMapper _mapper;


        public LiciterRepository(KupacDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }


        public async Task<LiciterConfirmation> CreateLiciter(Liciter liciter)
        {


            liciter.LiciterId = Guid.NewGuid();
            var kreiranliciter = await _context.Liciteri.AddAsync(liciter);


            if (liciter.Kupci != null)
            {

                foreach (var kupacId in liciter.Kupci)
                {

                    OvlascenoLice kupacliciter = new OvlascenoLice
                    {

                        LiciterId = liciter.LiciterId,
                        KupacId = kupacId


                    };
                    _context.OvlascenaLica.Add(kupacliciter);


                }

            }

            await _context.SaveChangesAsync();

            return _mapper.Map<LiciterConfirmation>(kreiranliciter.Entity);



        }

        public async Task DeleteLiciter(Guid liciterId)
        {

            var liciter = await GetLiciterById(liciterId);

            _context.Liciteri.Remove(liciter);

            await _context.SaveChangesAsync();

        }

        public async Task<List<Liciter>> GetAllLiciter()
        {

            var liciteri = await _context.Liciteri.ToListAsync();

            foreach (var l in liciteri)
            {

                
                l.Kupci = await _context.OvlascenaLica.Where(ku => ku.LiciterId == l.LiciterId).Select(pk => pk.KupacId).ToListAsync();

            }

            return liciteri;



        }

        public async Task<Liciter> GetLiciterById(Guid liciterId)
        {

            var liciter = await _context.Liciteri.Where(p => p.LiciterId == liciterId).FirstOrDefaultAsync();
            liciter.Kupci = await _context.OvlascenaLica.Where(ku => ku.LiciterId == liciterId).Select(pk => pk.KupacId).ToListAsync();

            return liciter;
            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLiciter(Liciter stariliciter, Liciter noviliciter)
        {

            _context.OvlascenaLica.Where(j => j.LiciterId == stariliciter.LiciterId).ExecuteDelete();

            _context.Remove(stariliciter);
            _context.SaveChanges();
            _context.Add(noviliciter);

            if(noviliciter.Kupci != null)
            {

                foreach(var KupacId  in noviliciter.Kupci)
                {
                    OvlascenoLice kupacOvl = new OvlascenoLice
                    {
                        LiciterId = noviliciter.LiciterId,
                        KupacId = KupacId


                    };
                    _context.OvlascenaLica.Add(kupacOvl);


                }



            }
            _context.SaveChanges();








        }
    }
}
