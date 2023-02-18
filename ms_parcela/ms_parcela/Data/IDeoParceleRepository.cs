using Microsoft.AspNetCore.Mvc;
using ms_parcela.Entities.DeoParceleE;

namespace ms_parcela.Data
{
    public interface IDeoParceleRepository
    {
        Task<List<DeoParcele>> GetAllDeoParcele();
        Task<DeoParcele?> GetDeoParceleById(Guid deoParceleId);
        Task<DeoParceleConfirmation> AddDeoParcele([FromBody] DeoParcele addDeoPRequest);
        Task<DeoParceleConfirmation?> UpdateDeoParcele([FromBody] DeoParcele updateDeoPRequest, Guid deoParceleId);
        Task<DeoParcele?> DeleteDeoParcele(Guid deoParceleId);
        Task<int> GetUkupnaPovrsinaDelovaByNadmetanjeId(List<Guid?> nadmetanja);

    }
}
