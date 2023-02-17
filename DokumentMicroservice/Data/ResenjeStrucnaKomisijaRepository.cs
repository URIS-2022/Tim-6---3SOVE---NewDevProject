using AutoMapper;
using DokumentMicroservice.Data.Interfaces;
using DokumentMicroservice.DataContext;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Entities.DataConfirmations;
using Microsoft.EntityFrameworkCore;
using System;

namespace DokumentMicroservice.Data
{
    public class ResenjeStrucnaKomisijaRepository : IResenjeStrucnaKomisijaRepository
    {

        private readonly DokumentDbContext _context;
        private readonly IMapper _mapper;


        public ResenjeStrucnaKomisijaRepository(DokumentDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }


        public async Task<ResenjeStrucnaKomisijaConfirmation> CreateResenjeStrucnaKomisija(ResenjeStrucnaKomisija resenjeStrucnaKomisija)
        {

            var kreiranoresenje = await _context.Resenjastrucnakomisija.AddAsync(resenjeStrucnaKomisija);

            await _context.SaveChangesAsync();

            return _mapper.Map<ResenjeStrucnaKomisijaConfirmation>(kreiranoresenje.Entity);

        }

        public async Task DeleteResenjeStrucnaKomisija(Guid resenjeId)
        {

            var resenje = await GetResenjeStrucnaKomisijaById(resenjeId);

            _context.Resenjastrucnakomisija.Remove(resenje);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResenjeStrucnaKomisija>> GetAllResenjeStrucnaKomisija()
        {

            return await _context.Resenjastrucnakomisija.ToListAsync();
        }

        public async Task<ResenjeStrucnaKomisija> GetResenjeStrucnaKomisijaById(Guid resenjeId)
        {
            return await _context.Resenjastrucnakomisija.FirstOrDefaultAsync(e => e.ResenjeId == resenjeId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResenjeStrucnaKomisija(ResenjeStrucnaKomisija resenjeStrucnaKomisija)
        {
            await _context.SaveChangesAsync();
        }
    }
}
