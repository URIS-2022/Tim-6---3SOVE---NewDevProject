using AutoMapper;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataOut;
using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.Mappings
{
    public class EtapaProfile : Profile
    {
        public EtapaProfile() 
        {
            CreateMap<Etapa, EtapaDataOut>()
                .ForMember(dest => dest.LicitacijaId, opt => opt.MapFrom(src => src.LicitacijaId))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src => src.Datum))
                .ForMember(dest => dest.VremePocetka, opt => opt.MapFrom(src => src.VremePocetka))
                .ForMember(dest => dest.VremeZavrsetka, opt => opt.MapFrom(src => src.VremeZavrsetka))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<EtapaSaveDataIn, Etapa>()
                .ForMember(dest => dest.LicitacijaId, opt => opt.MapFrom(src => src.LicitacijaId))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src => src.Datum))
                .ForMember(dest => dest.VremePocetka, opt => opt.MapFrom(src => src.VremePocetka))
                .ForMember(dest => dest.VremeZavrsetka, opt => opt.MapFrom(src => src.VremeZavrsetka));
        }
    }
}
