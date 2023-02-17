using AutoMapper;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.DataContext;
using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using Microsoft.EntityFrameworkCore;
using System;

namespace KupacMicroservice.Data
{
    public class PravnoLiceRepository : IPravnoLiceRepository
    {

        private readonly KupacDbContext _context;
        private readonly IMapper _mapper;

        public PravnoLiceRepository(KupacDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }


        public async Task<PravnoLiceConfirmation> CreatePravnoLice(PravnoLice pravnoLice)
        {


            var kreiranopravnolice = await _context.PravnaLica.AddAsync(pravnoLice);

            await _context.SaveChangesAsync();

            return _mapper.Map<PravnoLiceConfirmation>(kreiranopravnolice.Entity);


        }

        public async Task DeletePravnoLice(Guid pravnoliceId)
        {

            var pravnolice = await GetPravnoLiceById(pravnoliceId);

            _context.PravnaLica.Remove(pravnolice);

            await _context.SaveChangesAsync();
        }


        public async Task<List<PravnoLice>> GetAllPravnoLice()
        {
            return await _context.PravnaLica.ToListAsync();

        }

        public async Task<PravnoLice> GetPravnoLiceById(Guid pravnoliceId)
        {

            return await _context.PravnaLica.FirstOrDefaultAsync(e => e.PravnoliceId == pravnoliceId);

        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePravnoLice(PravnoLice pravnoLice)
        {
            await _context.SaveChangesAsync();
        }
    }
}
