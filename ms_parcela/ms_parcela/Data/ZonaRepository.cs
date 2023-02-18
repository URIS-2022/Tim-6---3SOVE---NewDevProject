using Microsoft.AspNetCore.Mvc;
using ms_parcela.Database;
using ms_parcela.Entities;
using ms_parcela.Models;

namespace ms_parcela.Data
{
    public class ZonaRepository : IZonaRepository
    {
        private static List<Zona> zone = new List<Zona> {
                new Zona
                {
                zonaID=Guid.NewGuid(),
                brojZona=brojevi_zona.zona1
                },
                new Zona
                {
                zonaID=Guid.NewGuid(),
                brojZona=brojevi_zona.zona2
                }
        };
        private readonly AuctionLandAPIContext dbContext;
        public ZonaRepository(AuctionLandAPIContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Zona> AddZona([FromBody] Zona addZonaRequest, brojevi_zona brz)
        {
            var zona = new Zona()
            {
                zonaID = Guid.NewGuid(),
                brojZona = brz
            };

            dbContext.Zonas.Add(zona);
            await dbContext.SaveChangesAsync();
            //var zone = await dbContext.Zonas.ToListAsync();
            return zona;
            
        }

        public async Task<Zona?> DeleteZona(Guid zonaID)
        {
            var zona = await dbContext.Zonas.FindAsync(zonaID);
            if (zona != null) 
            {
                dbContext.Zonas.Remove(zona);
                await dbContext.SaveChangesAsync();
                return zona;
            } else {
                return null;
            }
        }
        public async Task<List<Zona>> GetAllZona()
        {
            var zone = await dbContext.Zonas.ToListAsync();
            return zone;
        }

        public async Task<Zona?> GetZonaById(Guid zonaID)
        {
            var zona = await dbContext.Zonas.FindAsync(zonaID);
            if (zona is null)
                return null;
            return zona;
        }

        public async Task<Zona?> UpdateZona([FromBody] Zona updateZonaRequest, Guid zonaID, brojevi_zona brz)
        {
            var zona = await dbContext.Zonas.FindAsync(zonaID);

            if (zona != null)
            {
                zona.brojZona = brz;   //updateKoRequest.nazivKatastarskeOpstine
                await dbContext.SaveChangesAsync();
                //var zone = await dbContext.Zonas.ToListAsync();
                return zona;
            }else{
                return null;
            }
        }
    }
}
