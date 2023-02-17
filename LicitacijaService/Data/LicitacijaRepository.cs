using AutoMapper;
using LicitacijaService.Data.Interfaces;
using LicitacijaService.Entities;
using LicitacijaService.Entities.Confirmations;
using LicitacijaService.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LicitacijaService.Data
{
    public class LicitacijaRepository : ILicitacijaRepository
    {
        private readonly LicitacijaContext _context;
        private readonly IMapper _mapper;

        public LicitacijaRepository(LicitacijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Licitacija>> GetAllLicitacija()
        {
            return await _context.Licitacija
                .Include(pr => pr.ProgramEntitet)
                .ToListAsync();
        }

        public async Task<Licitacija> GetLicitacijaById(Guid licitacijaId)
        {
            return await _context.Licitacija
                .Include(pr => pr.ProgramEntitet)
                .FirstOrDefaultAsync(l => l.LicitacijaId == licitacijaId);
        }

        public async Task<LicitacijaConfirmation> CreateLicitacija(Licitacija licitacija)
        {
            var kreiranaLicitacija = await _context.Licitacija.AddAsync(licitacija);

            await _context.SaveChangesAsync();

            return _mapper.Map<LicitacijaConfirmation>(kreiranaLicitacija.Entity);
        }

        public async Task DeleteLicitacija(Guid licitacijaId)
        {
            var licitacija = await GetLicitacijaById(licitacijaId);

            _context.Licitacija.Remove(licitacija);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLicitacija(Licitacija licitacija)
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
