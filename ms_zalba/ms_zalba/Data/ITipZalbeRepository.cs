using Microsoft.AspNetCore.Mvc;
using ms_zalba.Entities;

namespace ms_zalba.Data
{
    public interface ITipZalbeRepository
    {
        Task<List<TipZalbe>> GetAllTipZalbe();
        Task<TipZalbe?> GetTipZalbeById(Guid tipZalbeID);
        Task<TipZalbe> AddTipZalbe([FromBody] TipZalbe addTipZalbeRequest);
        Task<TipZalbe?> UpdateTipZalbe([FromBody] TipZalbe updateTipZalbeRequest, Guid tipZalbeID);
        Task<TipZalbe?> DeleteTipZalbe(Guid tipZalbeID);
    }
}
