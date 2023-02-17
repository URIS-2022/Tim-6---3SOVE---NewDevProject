using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using System;


namespace KupacMicroservice.Data.Interfaces
{
    public interface IKupacRepository
    {

        
        Task<List<Kupac>> GetAllKupac();

        Task<Kupac> GetKupacById(Guid KupacId);

        Task<KupacConfirmation> CreateKupac(Kupac kupac);

        Task UpdateKupac(Kupac starikupac, Kupac novikupac);

        Task DeleteKupac(Guid KupacId);

        Task SaveChangesAsync();

    }
}
