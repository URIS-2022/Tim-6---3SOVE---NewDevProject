using PrijavaJnService.Entities;
using PrijavaJnService.Entities.Confirmations;
using PrijavaJnService.Models.PrijavaJn;

namespace PrijavaJnService.Data.Interfaces
{
    public interface IPrijavaJnRepository
    {
        Task<List<PrijavaJn>> GetAllPrijavaJn();
        Task<PrijavaJn> GetPrijavaJnById(Guid PrijavaId);
        Task<PrijavaJnConfirmation> CreatePrijavaJn(PrijavaJn prijavaJn);
        Task UpdatePrijavaJn(PrijavaJn prijavaJn);
        Task DeletePrijavaJn(Guid PrijavaId);
        Task SaveChangesAsync();
        Task<bool> IsZatvorenaPrijavaJn(PrijavaJn prijavaJn);
    }
}
