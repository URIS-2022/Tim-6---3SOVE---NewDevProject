using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Entities;
using System;

namespace DokumentMicroservice.Data.Interfaces
{
    public interface IOglasRepository
    {
        Task<List<Oglas>> GetAllOglas();

        Task<Oglas> GetOglasById(Guid OglasId);

        Task<OglasConfirmation> CreateOglas(Oglas oglas);

        Task UpdateOglas(Oglas oglas);

        Task DeleteOglas(Guid OglasId);

        Task SaveChangesAsync();
    }
}
