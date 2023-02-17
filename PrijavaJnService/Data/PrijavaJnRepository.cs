using PrijavaJnService.Entities.DataContext;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PrijavaJnService.Data.Interfaces;
using PrijavaJnService.Entities;
using PrijavaJnService.Entities.Confirmations;
using Microsoft.EntityFrameworkCore;

namespace PrijavaJnService.Data
{
    public class PrijavaJnRepository : IPrijavaJnRepository
    {
        private readonly PrijavaJnContext _context;
        private readonly IMapper _mapper;

        public PrijavaJnRepository(PrijavaJnContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PrijavaJnConfirmation> CreatePrijavaJn(PrijavaJn prijavaJn)
        {
            var kreiranaPrijavaJn = await _context.PrijavaJn.AddAsync(prijavaJn);

            await _context.SaveChangesAsync();

            return _mapper.Map<PrijavaJnConfirmation>(kreiranaPrijavaJn.Entity);
        }
        public async Task<List<PrijavaJn>> GetAllPrijavaJn()
        {
            return await _context.PrijavaJn.ToListAsync();
        }

        public async Task<PrijavaJn> GetPrijavaJnById(Guid prijavaId)
        {
            return await _context.PrijavaJn.FirstOrDefaultAsync(p => p.PrijavaId == prijavaId);
        }
        public async Task DeletePrijavaJn(Guid prijavaId)
        {
            var prijavaJn = await GetPrijavaJnById(prijavaId);

            _context.PrijavaJn.Remove(prijavaJn);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePrijavaJn(PrijavaJn prijavaJn)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsZatvorenaPrijavaJn(PrijavaJn prijavaJn)
        {
            var res = await GetPrijavaJnById(prijavaJn.PrijavaId);

            return res.ZatvorenaPonuda;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
