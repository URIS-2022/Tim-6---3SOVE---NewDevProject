using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ms_parcela.Database;
using ms_parcela.Entities.ParcelaE;


namespace ms_parcela.Data
{
    public class ParcelaRepository : IParcelaRepository
    {
        private static List<Parcela> parcele = new List<Parcela> {
                new Parcela
                {
                brojParcele = 3,
                brojKatastraskaOpstina = Guid.NewGuid(),
                povrsina=3000,
                kultura=Kultura.sume,
                klasa=Klasa.VIII,
                obradivost=Obradivost.ostalo,
                oblikSvojine=OblikSvojine.drzavna_svojina,
                odvodnjavanje=Odvodnjavanje.cevni_sistem
                },
                new Parcela
                {
                brojParcele = 4,
                brojKatastraskaOpstina = Guid.NewGuid(),
                povrsina=4000,
                kultura=Kultura.pasnjaci,
                klasa=Klasa.VIII,
                obradivost=Obradivost.ostalo,
                oblikSvojine=OblikSvojine.drzavna_svojina,
                odvodnjavanje=Odvodnjavanje.cevni_sistem
                },
        };
        private readonly AuctionLandAPIContext dbContext;
        private readonly IMapper mapper;

        public ParcelaRepository(AuctionLandAPIContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ParcelaConfirmation> AddParcela([FromBody] Parcela addParcelaRequest, Kultura k, Obradivost o, OblikSvojine os, Odvodnjavanje odv, Klasa kl)
        {
            var parcela = new Parcela()
            {
                brojParcele= addParcelaRequest.brojParcele,
                brojKatastraskaOpstina = addParcelaRequest.brojKatastraskaOpstina,
                povrsina = addParcelaRequest.povrsina,  //addKoRequest.nazivKatastarskeOpstine
                kultura=k,
                obradivost=o,
                oblikSvojine=os,
                odvodnjavanje=odv,
                klasa=kl
            };
            dbContext.Parcelas.Add(parcela);
            await dbContext.SaveChangesAsync();
            //var parcele = await dbContext.Parcelas.ToListAsync();
            return mapper.Map<ParcelaConfirmation>(parcela);
            //return parcela;
        }

        public async Task<Parcela?> DeleteParcela(Guid brojKo, int brojParcele)
        {
            var parcela = await dbContext.Parcelas.FindAsync(brojKo,brojParcele);
            if (parcela != null)
            {
                dbContext.Parcelas.Remove(parcela);
                await dbContext.SaveChangesAsync();
                return parcela;
            }
            else
            {
                return null;

            }
        }

        public async Task<List<Parcela>> GetAllParcela()
        {
            var parcele = await dbContext.Parcelas.ToListAsync();
            return parcele;
        }

        public async Task<Parcela?> GetParcelaByID(Guid brojKo, int brojParcele)
        {
            var parcela = await dbContext.Parcelas.FindAsync(brojKo, brojParcele);
            if (parcela is null)
                return null;
            return parcela;
        }

        public async Task<ParcelaConfirmation?> UpdateParcela([FromBody] Parcela updateParcelaRequest, Guid brojKo, int brojParcele, Kultura k, Obradivost o, OblikSvojine os, Odvodnjavanje odv, Klasa kl)
        {
            var parcela = await dbContext.Parcelas.FindAsync(brojKo,brojParcele);
            if (parcela != null)
            {
               
                parcela.povrsina = updateParcelaRequest.povrsina;
                parcela.kultura = k;
                parcela.obradivost = o;
                parcela.oblikSvojine=os;
                parcela.odvodnjavanje = odv;
                parcela.klasa = kl;
                await dbContext.SaveChangesAsync();
                //var parcele = await dbContext.Parcelas.ToListAsync();
                return mapper.Map<ParcelaConfirmation>(parcela);
            }
            else
            {
                return null;

            }
        }
    }
}
