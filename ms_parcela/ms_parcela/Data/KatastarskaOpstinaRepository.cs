using Microsoft.AspNetCore.Mvc;
using ms_parcela.Database;
using ms_parcela.Entities;

namespace ms_parcela.Data
{
    public class KatastarskaOpstinaRepository : IKatastarskaOpstinaRepository
    {
        private static List<KatastarskaOpstina> katastarskeOpstine = new List<KatastarskaOpstina> {
                new KatastarskaOpstina
                {
                brojKatastarskeOpstine=Guid.NewGuid(),
                nazivKatastarskeOpstine=opstine.Cantavir
                },
                new KatastarskaOpstina
                {
                brojKatastarskeOpstine=Guid.NewGuid(),
                nazivKatastarskeOpstine=opstine.Bajmok
                },
        };
        private readonly AuctionLandAPIContext dbContext;
        public KatastarskaOpstinaRepository(AuctionLandAPIContext dbContext)
        {
            this.dbContext=dbContext;
        }
        public async Task<KatastarskaOpstina> AddKatastarskaOpstina([FromBody] KatastarskaOpstina addKoRequest, opstine o)
        {
            var ko = new KatastarskaOpstina()
            {
                brojKatastarskeOpstine = Guid.NewGuid(),
                nazivKatastarskeOpstine = o,   //addKoRequest.nazivKatastarskeOpstine
            };
            dbContext.KatastarskaOpstinas.Add(ko);
            await dbContext.SaveChangesAsync();
            //var katastarskeOpstine = await dbContext.KatastarskaOpstinas.ToListAsync();
            return ko;
        }
 
        public async Task<KatastarskaOpstina?> DeleteKatastarskaOpstina(Guid brojKO)
        {
            var opstina = await dbContext.KatastarskaOpstinas.FindAsync(brojKO);
            if (opstina != null)
            {
                dbContext.KatastarskaOpstinas.Remove(opstina);
                await dbContext.SaveChangesAsync();
                return opstina;
            }
            else
            {
                return null;

            }
        }

        public async Task<List<KatastarskaOpstina>> GetAllKatastarskaOpstina()
        {
            var katastarskeOpstine = await dbContext.KatastarskaOpstinas.ToListAsync();
            return katastarskeOpstine;
        }

        public async Task<KatastarskaOpstina?> GetKatastarskaOpstinaByBroj(Guid brojKO)
        {
            var katastarskaOpstina = await dbContext.KatastarskaOpstinas.FindAsync(brojKO);
            if (katastarskaOpstina is null)
                return null;
            return katastarskaOpstina;
        }

        public async Task<KatastarskaOpstina?> UpdateKatastarskaOpstina([FromBody] KatastarskaOpstina updateKoRequest, Guid brojKO, opstine o)
        {

            var opstina = await dbContext.KatastarskaOpstinas.FindAsync(brojKO);
            if (opstina != null)
            {
                opstina.nazivKatastarskeOpstine = o;   //updateKoRequest.nazivKatastarskeOpstine
                await dbContext.SaveChangesAsync();
                //var katastarskeOpstine = await dbContext.KatastarskaOpstinas.ToListAsync();
                return opstina;
            }
            else
            {
                return null;

            }
        }
    }
}
