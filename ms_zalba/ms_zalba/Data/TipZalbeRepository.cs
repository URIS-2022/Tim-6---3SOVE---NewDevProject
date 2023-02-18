using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ms_zalba.Database;
using ms_zalba.Entities;

namespace ms_zalba.Data
{
    public class TipZalbeRepository : ITipZalbeRepository
    {
        private static List<TipZalbe> tipoviZalbi = new List<TipZalbe> {
                new TipZalbe
                {
                idTipZalbe=Guid.NewGuid(),
                nazivTipaZalbe="Zalba na tok javnog nadmetanja"
                },
                new TipZalbe
                {
                idTipZalbe=Guid.NewGuid(),
                nazivTipaZalbe="Zalba na Odluku o davanju u zakup"
                }
        };
        private readonly AuctionLandAPIContext dbContext;
        public TipZalbeRepository(AuctionLandAPIContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<TipZalbe> AddTipZalbe([FromBody] TipZalbe addTipZalbeRequest)
        {
            var tipZalbe = new TipZalbe()
            {
                idTipZalbe = Guid.NewGuid(),
                nazivTipaZalbe = addTipZalbeRequest.nazivTipaZalbe
                
            };
            dbContext.TipZalbes.Add(tipZalbe);
            await dbContext.SaveChangesAsync();
            //var tipoviZalbi = await dbContext.TipZalbes.ToListAsync();
            return tipZalbe;
        }

        public async Task<TipZalbe?> DeleteTipZalbe(Guid tipZalbeID)
        {
            var tipZalbe = await dbContext.TipZalbes.FindAsync(tipZalbeID);
            if (tipZalbe != null)
            {
                dbContext.TipZalbes.Remove(tipZalbe);
                await dbContext.SaveChangesAsync();
                return tipZalbe;
            }
            else
            {
                return null;

            }
        }

        public async Task<List<TipZalbe>> GetAllTipZalbe()
        {
            var tipoviZalbi = await dbContext.TipZalbes.ToListAsync();
            return tipoviZalbi;
        }

        public async Task<TipZalbe?> GetTipZalbeById(Guid tipZalbeID)
        {
            var tipZalbe = await dbContext.TipZalbes.FindAsync(tipZalbeID);
            if (tipZalbe is null)
                return null;
            return tipZalbe;
        }

        public async Task<TipZalbe?> UpdateTipZalbe([FromBody] TipZalbe updateTipZalbeRequest, Guid tipZalbeID)
        {
            var tipZalbe = await dbContext.TipZalbes.FindAsync(tipZalbeID);
            if (tipZalbe != null)
            {
                tipZalbe.nazivTipaZalbe = updateTipZalbeRequest.nazivTipaZalbe;
                
                await dbContext.SaveChangesAsync();
                //var tipoviZalbi= await dbContext.TipZalbes.ToListAsync();
                return tipZalbe;
            }
            else
            {
                return null;

            }
        }
    }
}
