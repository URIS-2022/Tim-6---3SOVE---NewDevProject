using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Entities;


namespace DokumentMicroservice.Data.Interfaces
{
    public interface IPredlogPlanaProjektaRepository
    {
        Task<List<PredlogPlanaProjekta>> GetAllPredlogPlanaProjekta();

        Task<PredlogPlanaProjekta> GetPredlogPlanaProjektaById(Guid PredlogId);

        Task<PredlogPlanaProjektaConfirmation> CreatePredlogPlanaProjekta(PredlogPlanaProjekta predlogPlanaProjekta);

        Task UpdatePredlogPlanaProjekta(PredlogPlanaProjekta predlogPlanaProjekta);

        Task DeletePredlogPlanaProjekta(Guid PredlogId);

        Task SaveChangesAsync();
    }
}
