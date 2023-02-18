using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ms_zalba.Database;
using ms_zalba.Entities.ZalbaE;
using ms_zalba.Models.TipZalbeModel;
using ms_zalba.Models.ZalbaModel;

namespace ms_zalba.Data
{
    public class ZalbaRepository : IZalbaRepository
    {
        private static List<Zalba> zalbe = new List<Zalba> {
                new Zalba
                {
                zalbaID=Guid.NewGuid(),
                tipZalbe=Guid.NewGuid(),
                datumPodnosenjaZalbe=new DateTime(2022, 02, 08),
                razlogZalbe="Parcela nije dodeljena u potpunosti",
                obrazlozenje="Nepotpuna dodela",
                datumResenja=new DateTime(2022, 02, 10),
                brojResenja="123",
                statusZalbe=StatusZalbe.odbijena,
                brojNadmetanja=1,
                radnjaNaOsnovuZalbe=RadnjaNaOsnovuZalbe.JN_ide_u_drugi_krug_sa_starim_uslovima,
                podnosilacZalbe=Guid.NewGuid()
                }
              
        };
        private readonly AuctionLandAPIContext dbContext;
        private readonly IMapper mapper;

        public ZalbaRepository(AuctionLandAPIContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ZalbaConfirmation> AddZalba([FromBody] Zalba addZalbaRequest, StatusZalbe sz, RadnjaNaOsnovuZalbe radnjaNaOsnovuZalbe)
        {
            var zalba = new Zalba()
            {
                zalbaID = Guid.NewGuid(),
                tipZalbe = addZalbaRequest.tipZalbe,
                datumPodnosenjaZalbe = addZalbaRequest.datumPodnosenjaZalbe,
                razlogZalbe = addZalbaRequest.razlogZalbe,
                obrazlozenje = addZalbaRequest.obrazlozenje,
                datumResenja = addZalbaRequest.datumResenja,
                brojResenja =addZalbaRequest.brojResenja,
                statusZalbe = sz,
                brojNadmetanja = addZalbaRequest.brojNadmetanja,
                radnjaNaOsnovuZalbe = radnjaNaOsnovuZalbe,
                podnosilacZalbe =addZalbaRequest.podnosilacZalbe

            };
            dbContext.Zalbas.Add(zalba);
            await dbContext.SaveChangesAsync();
            //var zelbe = await dbContext.Zalbas.ToListAsync();
            return mapper.Map<ZalbaConfirmation>(zalba);
        }

        public async Task<Zalba?> DeleteZalba(Guid zalbaID)
        {
            var zalba = await dbContext.Zalbas.FindAsync(zalbaID);
            if (zalba != null)
            {
                dbContext.Zalbas.Remove(zalba);
                await dbContext.SaveChangesAsync();
                return zalba;
            }
            else
            {
                return null;

            }
        }

        public async Task<List<Zalba>> GetAllZalba()
        {
            var zalbe = await dbContext.Zalbas.ToListAsync();
            return zalbe;
        }

        public async Task<Zalba?> GetZalbaById(Guid zalbaID)
        {
            var zalba = await dbContext.Zalbas.FindAsync(zalbaID);
            if (zalba is null)
                return null;
            return zalba;
        }

        public async Task<ZalbaConfirmation?> UpdateZalba([FromBody] Zalba updateZalbaRequest, Guid zalbaID, StatusZalbe sz, RadnjaNaOsnovuZalbe radnjaNaOsnovuZalbe)
        {
            var zalba = await dbContext.Zalbas.FindAsync(zalbaID);
            if (zalba != null)
            {
                zalba.tipZalbe = updateZalbaRequest.tipZalbe;
                zalba.datumPodnosenjaZalbe = updateZalbaRequest.datumPodnosenjaZalbe;
                zalba.razlogZalbe = updateZalbaRequest.razlogZalbe;
                zalba.obrazlozenje = updateZalbaRequest.obrazlozenje;
                zalba.datumResenja = updateZalbaRequest.datumResenja;
                zalba.brojResenja = updateZalbaRequest.brojResenja;
                zalba.statusZalbe = sz;
                zalba.brojNadmetanja = updateZalbaRequest.brojNadmetanja;
                zalba.radnjaNaOsnovuZalbe = radnjaNaOsnovuZalbe;
                zalba.podnosilacZalbe = updateZalbaRequest.podnosilacZalbe;

                await dbContext.SaveChangesAsync();
                //var zalbe = await dbContext.Zalbas.ToListAsync();
                return mapper.Map<ZalbaConfirmation>(zalba);
            }
            else
            {
                return null;

            }
        }
    }
}
