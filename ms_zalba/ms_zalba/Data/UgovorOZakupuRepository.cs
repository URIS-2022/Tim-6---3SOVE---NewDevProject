using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ms_zalba.Database;
using ms_zalba.Entities.UgovorOZakupuE;
using ms_zalba.Models.TipZalbeModel;
using ms_zalba.Models.UgovorOZakupuModel;
using ms_zalba.Models.ZalbaModel;

namespace ms_zalba.Data
{
    public class UgovorOZakupuRepository : IUgovorOZakupuRepository
    {
        private readonly AuctionLandAPIContext dbContext;
        private readonly IMapper mapper;

        public UgovorOZakupuRepository(AuctionLandAPIContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<UgovorOZakupuConfirmation> AddUgovorOZakupu([FromBody] UgovorOZakupu addUgovorOZakupuRequest)
        {
            var ugovorOZ = new UgovorOZakupu()
            {
                
                idUgovor=addUgovorOZakupuRequest.idUgovor,
                tipGarancije=addUgovorOZakupuRequest.tipGarancije, 
                rokDospeca=addUgovorOZakupuRequest.rokDospeca,
                zavodniBroj=addUgovorOZakupuRequest.zavodniBroj,
                datumZavodjanja=addUgovorOZakupuRequest.datumZavodjanja,
                rokZaVracanjeZemljista=addUgovorOZakupuRequest.rokZaVracanjeZemljista,
                mestoPotpisivanja=addUgovorOZakupuRequest.mestoPotpisivanja,
                datumPotpisa=addUgovorOZakupuRequest.datumPotpisa

            };
            dbContext.UgovorOZakupus.Add(ugovorOZ);
            await dbContext.SaveChangesAsync();
            //var ugovori = await dbContext.UgovorOZakupus.ToListAsync();
            return mapper.Map<UgovorOZakupuConfirmation>(ugovorOZ);
        }

        public async Task<UgovorOZakupu?> DeleteUgovorOZakupu(Guid idUgovora)
        {
            var ugovor = await dbContext.UgovorOZakupus.FindAsync(idUgovora);
            if (ugovor != null)
            {
                dbContext.UgovorOZakupus.Remove(ugovor);
                await dbContext.SaveChangesAsync();
                return ugovor;
            }
            else
            {
                return null;

            }
        }

        public async Task<List<UgovorOZakupu>> GetAllUgovorOZakupu()
        {
            var ugovori = await dbContext.UgovorOZakupus.ToListAsync();
            return ugovori;
        }

        public async Task<UgovorOZakupu?> GetUgovorOZakupuById(Guid idUgovora)
        {
            var ugovor = await dbContext.UgovorOZakupus.FindAsync(idUgovora);
            if (ugovor is null)
                return null;
            return ugovor;
        }

        public async Task<UgovorOZakupuConfirmation?> UpdateUgovorOZakupu([FromBody] UgovorOZakupu updateUgovorOZakupuRequest, Guid idUgovora)
        {
            var ugovor = await dbContext.UgovorOZakupus.FindAsync(idUgovora);
            if (ugovor != null)
            {
                
                ugovor.idUgovor = updateUgovorOZakupuRequest.idUgovor;
                ugovor.tipGarancije = updateUgovorOZakupuRequest.tipGarancije;
                ugovor.rokDospeca = updateUgovorOZakupuRequest.rokDospeca;
                ugovor.zavodniBroj = updateUgovorOZakupuRequest.zavodniBroj;
                ugovor.datumZavodjanja = updateUgovorOZakupuRequest.datumZavodjanja;
                ugovor.rokZaVracanjeZemljista = updateUgovorOZakupuRequest.rokZaVracanjeZemljista;
                ugovor.mestoPotpisivanja = updateUgovorOZakupuRequest.mestoPotpisivanja;
                ugovor.datumPotpisa = updateUgovorOZakupuRequest.datumPotpisa;
                await dbContext.SaveChangesAsync();
                //var ugovorUpd = await dbContext.UgovorOZakupus.ToListAsync();
                return mapper.Map<UgovorOZakupuConfirmation>(ugovor);
            }
            else
            {
                return null;

            }
        }

        
    }
}
