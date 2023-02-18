using Microsoft.AspNetCore.Mvc;
using ms_zalba.Entities.ZalbaE;


namespace ms_zalba.Data
{
    public interface IZalbaRepository
    {
        Task<List<Zalba>> GetAllZalba();
        Task<Zalba?> GetZalbaById(Guid zalbaID);
        Task<ZalbaConfirmation> AddZalba([FromBody] Zalba addZalbaRequest,StatusZalbe sz, RadnjaNaOsnovuZalbe radnjaNaOsnovuZalbe);
        Task<ZalbaConfirmation?> UpdateZalba([FromBody] Zalba updateZalbaRequest, Guid zalbaID, StatusZalbe sz, RadnjaNaOsnovuZalbe radnjaNaOsnovuZalbe);
        Task<Zalba?> DeleteZalba(Guid zalbaID);
    }
}
