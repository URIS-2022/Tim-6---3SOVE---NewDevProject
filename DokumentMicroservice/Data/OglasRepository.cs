using DokumentMicroservice.Data.Interfaces;
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Entities;
using System;
using DokumentMicroservice.DataContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DokumentMicroservice.Data
{
    public class OglasRepository : IOglasRepository
    {

        private readonly DokumentDbContext _context;
        private readonly IMapper _mapper;

        public OglasRepository(DokumentDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<OglasConfirmation> CreateOglas(Oglas oglas)
        {
            var kreiranoglas = await _context.Oglasi.AddAsync(oglas);

            await _context.SaveChangesAsync();

            return _mapper.Map<OglasConfirmation>(kreiranoglas.Entity);
        }


        public async Task DeleteOglas(Guid oglasId)
        {

            var oglas = await GetOglasById(oglasId);

            _context.Oglasi.Remove(oglas);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Oglas>> GetAllOglas()
        {

            return await _context.Oglasi.ToListAsync();
        }

        public async Task<Oglas> GetOglasById(Guid oglasId)
        {

          return await _context.Oglasi.FirstOrDefaultAsync(e => e.OglasId == oglasId);

            
        }

        public async Task SaveChangesAsync()
        {

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOglas(Oglas oglas)
        {

            await _context.SaveChangesAsync();

        }
    }
}
