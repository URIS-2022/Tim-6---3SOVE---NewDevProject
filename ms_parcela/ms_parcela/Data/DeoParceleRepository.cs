using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ms_parcela.Database;
using ms_parcela.Entities.DeoParceleE;
using ms_parcela.Entities.ParcelaE;
using ms_parcela.Models.ExternalServices;
using System.Linq;

namespace ms_parcela.Data
{
    public class DeoParceleRepository : IDeoParceleRepository
    {
        private static List<DeoParcele> deloviParcele = new List<DeoParcele> {
                new DeoParcele
                {
                deoParceleID=Guid.NewGuid(),
                redniBrojDelaParcele=1,
                povrsinaDelaParcele=250,
                brojParcele=1,
                brojKatastraskaOpstina=Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                NadmetanjeId=Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950s")
                },
                new DeoParcele
                {
                deoParceleID=Guid.NewGuid(),
                redniBrojDelaParcele=2,
                povrsinaDelaParcele=350,
                brojParcele=1,
                brojKatastraskaOpstina=Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                NadmetanjeId=Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950p")

                },
        };
        private readonly AuctionLandAPIContext dbContext;
        private readonly IMapper mapper;

        public DeoParceleRepository(AuctionLandAPIContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<DeoParceleConfirmation> AddDeoParcele([FromBody] DeoParcele addDeoPRequest)
        {
            var deoParcele = new DeoParcele()
            {
                deoParceleID = Guid.NewGuid(),
                redniBrojDelaParcele = addDeoPRequest.redniBrojDelaParcele,  //addKoRequest.nazivKatastarskeOpstine
                povrsinaDelaParcele=addDeoPRequest.povrsinaDelaParcele,
                brojParcele=addDeoPRequest.brojParcele,
                brojKatastraskaOpstina=addDeoPRequest.brojKatastraskaOpstina,
                NadmetanjeId=addDeoPRequest.NadmetanjeId

            };
            dbContext.DeoParceles.Add(deoParcele);
            await dbContext.SaveChangesAsync();
            //var deloviParcele = await dbContext.DeoParceles.ToListAsync();
            return mapper.Map<DeoParceleConfirmation>(deoParcele);
        }

        public async Task<DeoParcele?> DeleteDeoParcele(Guid deoParceleId)
        {
            var deoParcele = await dbContext.DeoParceles.FindAsync(deoParceleId);
            if (deoParcele != null)
            {
                dbContext.DeoParceles.Remove(deoParcele);
                await dbContext.SaveChangesAsync();
                return deoParcele;
            }
            else
            {
                return null;

            }
        }

        public async Task<List<DeoParcele>> GetAllDeoParcele()
        {
            var deloviParcele = await dbContext.DeoParceles.ToListAsync();
            return deloviParcele;
        }

        public async Task<DeoParcele?>GetDeoParceleById(Guid deoParceleId)
        {
            var deoParcele = await dbContext.DeoParceles.FindAsync(deoParceleId);
            if (deoParcele is null)
                return null;
            return deoParcele;
        }

        public async Task<DeoParceleConfirmation?> UpdateDeoParcele([FromBody] DeoParcele updateDeoPRequest, Guid deoParceleId)
        {
            var deoParcele = await dbContext.DeoParceles.FindAsync(deoParceleId);
            if (deoParcele != null)
            {
                deoParcele.redniBrojDelaParcele = updateDeoPRequest.redniBrojDelaParcele;   //updateKoRequest.nazivKatastarskeOpstine
                deoParcele.povrsinaDelaParcele = updateDeoPRequest.povrsinaDelaParcele;
                deoParcele.brojParcele = updateDeoPRequest.brojParcele;
                deoParcele.brojKatastraskaOpstina = updateDeoPRequest.brojKatastraskaOpstina;
                await dbContext.SaveChangesAsync();
                //var deloviParcele = await dbContext.DeoParceles.ToListAsync();
                return mapper.Map<DeoParceleConfirmation>(deoParcele);
                
            }
            else
            {
                return null;

            }
        }
        public async Task<int> GetUkupnaPovrsinaDelovaByNadmetanjeId(List<Guid?> nadmetanja)
        {
            //var relevantniDelovi= await dbContext.Set<DeoParcele>()
                //.Where(dp => dp.NadmetanjeId == nadmetanjeID).Select(dp=>dp.NadmetanjeId).ToListAsync(); //nademtanja

            int ukupnaPovrsina=0;
            var relevantniDeloviParcele = await dbContext.DeoParceles.Where(dp => nadmetanja.Contains(dp.NadmetanjeId)).ToListAsync();
            foreach (var deo in relevantniDeloviParcele)
            {
                ukupnaPovrsina += deo.povrsinaDelaParcele;

            }
            return ukupnaPovrsina;

        }

        
    }
}
