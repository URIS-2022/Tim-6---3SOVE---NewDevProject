using AutoMapper;
using DokumentMicroservice.Data.Interfaces;
using DokumentMicroservice.DataContext;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Entities.DataConfirmations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DokumentMicroservice.Data
{
    public class DokumentRepository : IDokumentRepository
    {

        private readonly DokumentDbContext _context;
        private readonly IMapper _mapper;


        public DokumentRepository(DokumentDbContext context, IMapper mapper)
        {
               this._context = context;
               this._mapper = mapper;

        }

        /*public DokumentRepository(DokumentDbContext context)
        {

        } */

        public async Task<DokumentConfirmation> CreateDokument(Dokument dokument)
        {

            var kreirandokument = await _context.Dokumenti.AddAsync(dokument);

            await _context.SaveChangesAsync();

            return _mapper.Map<DokumentConfirmation>(kreirandokument.Entity);

        }


        public async Task<Dokument> GetDokumentById(Guid dokumentId)
        {

            var dokument = await _context.Dokumenti.Include(i => i.Oglas).Include(i => i.PredlogPlanaProjekta).Include(i => i.ResenjeStrucnaKomisija).Where(i => i.DokumentId == dokumentId).FirstOrDefaultAsync();

            return dokument;
        }


        public async Task DeleteDokument(Guid dokumentId)
        {

            var dokument = await GetDokumentById(dokumentId);

            _context.Dokumenti.Remove(dokument);

            await _context.SaveChangesAsync();

        }

        public async Task<List<Dokument>> GetAllDokument()
        {

            var dokumenti = await _context.Dokumenti.Include(i => i.Oglas).Include( i => i.PredlogPlanaProjekta).Include(i => i.ResenjeStrucnaKomisija).ToListAsync();

            return dokumenti;

        }

        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDokument(Dokument dokument)
        {

            await _context.SaveChangesAsync();

        }

        
    }
}
