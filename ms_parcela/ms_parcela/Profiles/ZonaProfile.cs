using AutoMapper;
using ms_parcela.Entities;
using ms_parcela.Models.ZonaModel;

namespace ms_parcela.Profiles
{
    public class ZonaProfile : Profile
    {
        public ZonaProfile()
        {
            CreateMap<Zona, ZonaDto>();
            CreateMap<ZonaCreationDto, Zona>();
            CreateMap<ZonaUpdateDto, Zona>();
        }
    }
}
