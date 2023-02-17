using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Entities;
using System;

namespace DokumentMicroservice.Data.Interfaces
{
    public interface IResenjeStrucnaKomisijaRepository
    {
        Task<List<ResenjeStrucnaKomisija>> GetAllResenjeStrucnaKomisija();

        Task<ResenjeStrucnaKomisija> GetResenjeStrucnaKomisijaById(Guid ResenjeId);

        Task<ResenjeStrucnaKomisijaConfirmation> CreateResenjeStrucnaKomisija(ResenjeStrucnaKomisija resenjeStrucnaKomisija);

        Task UpdateResenjeStrucnaKomisija(ResenjeStrucnaKomisija resenjeStrucnaKomisija);

        Task DeleteResenjeStrucnaKomisija(Guid ResenjeId);

        Task SaveChangesAsync();
    }
}
