using Microsoft.AspNetCore.Mvc;
using ms_parcela.Entities;

namespace ms_parcela.Data
{
    public interface IZonaRepository
    {
        Task<List<Zona>> GetAllZona();
        Task<Zona?> GetZonaById(Guid zonaID);
        Task<Zona> AddZona([FromBody] Zona addZonaRequest, brojevi_zona brz);
        Task<Zona?> UpdateZona([FromBody] Zona updateZonaRequest, Guid zonaID, brojevi_zona brz);
        Task<Zona?> DeleteZona(Guid zonaID);
    }
}
