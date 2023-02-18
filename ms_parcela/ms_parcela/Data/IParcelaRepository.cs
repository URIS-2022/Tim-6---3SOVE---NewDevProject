using Microsoft.AspNetCore.Mvc;
using ms_parcela.Entities.ParcelaE;


namespace ms_parcela.Data
{
    public interface IParcelaRepository
    {
        Task<List<Parcela>> GetAllParcela();
        Task<Parcela?> GetParcelaByID(Guid brojKo, int brojParcele);
        Task<ParcelaConfirmation> AddParcela([FromBody] Parcela addParcelaRequest, Kultura k, Obradivost o, OblikSvojine os, Odvodnjavanje odv, Klasa kl);
        Task<ParcelaConfirmation?> UpdateParcela([FromBody] Parcela updateParcelaRequest, Guid brojKo, int brojParcele, Kultura k, Obradivost o, OblikSvojine os, Odvodnjavanje odv, Klasa kl);
        Task<Parcela?> DeleteParcela(Guid brojKo, int brojParcele);
    }
}
