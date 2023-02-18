using LicitacijaService.Data.Interfaces;
using LicitacijaService.Entities;
using LicitacijaService.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicitacijaService.Data
{
    public class ProgramEntitetRepository : IProgramEntitetRepository
    {
        private readonly LicitacijaContext _context;

        public ProgramEntitetRepository(LicitacijaContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramEntitet>> GetAllProgramEntitet()
        {
            return await _context.ProgramEntitet
                .ToListAsync();
        }

        public async Task<ProgramEntitet> GetProgramEntitetById(Guid programId)
        {
            return await _context.ProgramEntitet.FirstOrDefaultAsync(pr => pr.ProgramId == programId);
        }

        public async Task<ProgramEntitet> CreateProgramEntitet(ProgramEntitet programEntitet)
        {
            _context.ProgramEntitet.Add(programEntitet);
            await _context.SaveChangesAsync();

            return programEntitet;
        }

        public async Task DeleteProgramEntitet(Guid programEntitetId)
        {
            var programEntitet = await GetProgramEntitetById(programEntitetId);

            _context.ProgramEntitet.Remove(programEntitet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProgramEntitet(ProgramEntitet programEntitet)
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
