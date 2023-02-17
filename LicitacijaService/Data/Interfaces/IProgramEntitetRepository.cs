using LicitacijaService.Entities;

namespace LicitacijaService.Data.Interfaces
{
    public interface IProgramEntitetRepository
    {
        Task<List<ProgramEntitet>> GetAllProgramEntitet();
        Task<ProgramEntitet> GetProgramEntitetById(Guid programId);
        Task<ProgramEntitet> CreateProgramEntitet(ProgramEntitet programEntitet);
        Task UpdateProgramEntitet(ProgramEntitet programEntitet);
        Task DeleteProgramEntitet(Guid programId);
        Task SaveChangesAsync();
    }
}
