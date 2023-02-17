using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using System;


namespace KupacMicroservice.Data.Interfaces
{
    public interface IFizickoLiceRepository
    {

        Task<List<FizickoLice>> GetAllFizickoLice();

        Task<FizickoLice> GetFizickoLiceById(Guid FizickoliceId);

        Task<FizickoLiceConfirmation> CreateFizickoLice(FizickoLice fizickoLice);

        Task UpdateFizickoLice(FizickoLice fizickoLice);

        Task DeleteFizickoLice(Guid FizickoliceId);

        Task SaveChangesAsync();

    }
}
