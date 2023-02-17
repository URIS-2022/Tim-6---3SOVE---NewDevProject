using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.Entities;
using System;



namespace KupacMicroservice.Data.Interfaces
{
    public interface IPravnoLiceRepository
    {

        Task<List<PravnoLice>> GetAllPravnoLice();

        Task<PravnoLice> GetPravnoLiceById(Guid PravnoliceId);

        Task<PravnoLiceConfirmation> CreatePravnoLice(PravnoLice pravnoLice);

        Task UpdatePravnoLice(PravnoLice pravnoLice);

        Task DeletePravnoLice(Guid PravnoliceId);

        Task SaveChangesAsync();


    }
}
