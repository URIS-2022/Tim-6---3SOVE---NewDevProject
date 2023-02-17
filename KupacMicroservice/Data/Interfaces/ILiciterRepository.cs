using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.Entities;
using System;

namespace KupacMicroservice.Data.Interfaces
{
    public interface ILiciterRepository
    {

        Task<List<Liciter>> GetAllLiciter();

        Task<Liciter> GetLiciterById(Guid LiciterId);

        Task<LiciterConfirmation> CreateLiciter(Liciter liciter);

        Task UpdateLiciter(Liciter stariliciter, Liciter noviliciter);

        Task DeleteLiciter(Guid LiciterId);

        Task SaveChangesAsync();
    }
}
