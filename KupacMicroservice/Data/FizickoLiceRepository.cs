using AutoMapper;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.DataContext;
using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using Microsoft.EntityFrameworkCore;
using System;

namespace KupacMicroservice.Data
{
    public class FizickoLiceRepository : IFizickoLiceRepository
    {


        private readonly KupacDbContext _context;
        private readonly IMapper _mapper;


        public FizickoLiceRepository(KupacDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }


        public async Task<FizickoLiceConfirmation> CreateFizickoLice(FizickoLice fizickoLice)
        {

            var novofizickoLice = await _context.FizickaLica.AddAsync(fizickoLice);

            await _context.SaveChangesAsync();

            return _mapper.Map<FizickoLiceConfirmation>(novofizickoLice.Entity);

        }

        public async Task<FizickoLice> GetFizickoLiceById(Guid fizickoliceId)
        {

            return await _context.FizickaLica.FirstOrDefaultAsync(e => e.FizickoliceId == fizickoliceId);


        }

        public async Task DeleteFizickoLice(Guid fizickoliceId)
        {

            var fizickolice = await GetFizickoLiceById(fizickoliceId);

            _context.FizickaLica.Remove(fizickolice);

            await _context.SaveChangesAsync();

        }

        public async Task<List<FizickoLice>> GetAllFizickoLice()
        {

            return await _context.FizickaLica.ToListAsync();
        }

        
        public async Task SaveChangesAsync()
        {

            await _context.SaveChangesAsync();
        }

        public async Task UpdateFizickoLice(FizickoLice fizickoLice)
        {

            await _context.SaveChangesAsync();
        }
    }
}
