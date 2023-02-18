using Microsoft.AspNetCore.Mvc;
using ms_parcela.Entities;


namespace ms_parcela.Data
{
    public interface IKatastarskaOpstinaRepository
    {
        Task<List<KatastarskaOpstina>> GetAllKatastarskaOpstina();
        Task<KatastarskaOpstina?> GetKatastarskaOpstinaByBroj(Guid brojKO);
        Task<KatastarskaOpstina> AddKatastarskaOpstina([FromBody] KatastarskaOpstina addKoRequest, opstine o=0);
        Task<KatastarskaOpstina?> UpdateKatastarskaOpstina([FromBody] KatastarskaOpstina updateKoRequest, Guid brojKO, opstine o);
        Task<KatastarskaOpstina?> DeleteKatastarskaOpstina(Guid brojKO);
    }
}
