using Microsoft.AspNetCore.Mvc;
using ms_zalba.Entities.UgovorOZakupuE;


namespace ms_zalba.Data
{
    public interface IUgovorOZakupuRepository
    {
        Task<List<UgovorOZakupu>> GetAllUgovorOZakupu();
        Task<UgovorOZakupu?> GetUgovorOZakupuById(Guid idUgovora);
        Task<UgovorOZakupuConfirmation> AddUgovorOZakupu([FromBody] UgovorOZakupu addUgovorOZakupuRequest);
        Task<UgovorOZakupuConfirmation?> UpdateUgovorOZakupu([FromBody] UgovorOZakupu updateUgovorOZakupuRequest, Guid idUgovora);
        Task<UgovorOZakupu?> DeleteUgovorOZakupu(Guid idUgovora);
    }
}
