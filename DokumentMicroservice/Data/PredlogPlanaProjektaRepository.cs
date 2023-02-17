using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Data.Interfaces;
using AutoMapper;
using DokumentMicroservice.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DokumentMicroservice.Data
{
    public class PredlogPlanaProjektaRepository : IPredlogPlanaProjektaRepository
    {

        private readonly DokumentDbContext _context;
        private readonly IMapper _mapper;

        public PredlogPlanaProjektaRepository(DokumentDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<PredlogPlanaProjektaConfirmation> CreatePredlogPlanaProjekta(PredlogPlanaProjekta predlogPlanaProjekta)
        {
            var kreiranpredlog = await _context.PredloziPlanaProjekta.AddAsync(predlogPlanaProjekta);

            await _context.SaveChangesAsync();

            return _mapper.Map<PredlogPlanaProjektaConfirmation>(kreiranpredlog.Entity);

        }

        public async Task DeletePredlogPlanaProjekta(Guid predlogId)
        {

            var predlog = await GetPredlogPlanaProjektaById(predlogId);

            _context.PredloziPlanaProjekta.Remove(predlog);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PredlogPlanaProjekta>> GetAllPredlogPlanaProjekta()
        {

            return await _context.PredloziPlanaProjekta.ToListAsync();
        }

        public async Task<PredlogPlanaProjekta> GetPredlogPlanaProjektaById(Guid predlogId)
        {
            return await _context.PredloziPlanaProjekta.FirstOrDefaultAsync(e => e.PredlogId == predlogId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePredlogPlanaProjekta(PredlogPlanaProjekta predlogPlanaProjekta)
        {
            await _context.SaveChangesAsync();
        }
    }
}
