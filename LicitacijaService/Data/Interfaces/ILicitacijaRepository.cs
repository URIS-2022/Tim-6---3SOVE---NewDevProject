using LicitacijaService.Entities;
using LicitacijaService.Entities.Confirmations;

namespace LicitacijaService.Data.Interfaces
{
    public interface ILicitacijaRepository
    {
        Task<List<Licitacija>> GetAllLicitacija();
        Task<Licitacija> GetLicitacijaById(Guid licitacijaId);
        Task<LicitacijaConfirmation> CreateLicitacija(Licitacija licitacija);
        Task UpdateLicitacija(Licitacija licitacija);
        Task DeleteLicitacija(Guid licitacijaId);
        Task SaveChangesAsync();
    }
}
