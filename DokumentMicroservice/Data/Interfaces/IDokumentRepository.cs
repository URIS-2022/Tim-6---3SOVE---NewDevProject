using DokumentMicroservice.Entities;
using DokumentMicroservice.Entities.DataConfirmations;
using System;


namespace DokumentMicroservice.Data.Interfaces
{
    public interface IDokumentRepository
    {

        Task<List<Dokument>> GetAllDokument();

        Task<Dokument> GetDokumentById(Guid DokumentId);

        Task<DokumentConfirmation> CreateDokument(Dokument dokument);

        Task UpdateDokument(Dokument dokument);

        Task DeleteDokument(Guid DokumentId);

        Task SaveChangesAsync();

    }
}
