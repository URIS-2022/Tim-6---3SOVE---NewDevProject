using AutoMapper;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using nadmetanje_microserviceBLL.DTOs.Etapa.DataOut;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn;
using nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataOut;
using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.Mappings
{
    public class NadmetanjeProfile : Profile
    {
        public NadmetanjeProfile()
        {
            CreateMap<Nadmetanje, NadmetanjeDataOut>()
                .ForMember(dest => dest.Tip, opt => opt.MapFrom(src => src.Tip))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CenaPoHektaru, opt => opt.MapFrom(src => src.CenaPoHektaru))
                .ForMember(dest => dest.DuzinaZakupa, opt => opt.MapFrom(src => src.DuzinaZakupa))
                .ForMember(dest => dest.RedniBroj, opt => opt.MapFrom(src => src.RedniBroj))
                .ForMember(dest => dest.EtapaId, opt => opt.MapFrom(src => src.EtapaId))
                .ForMember(dest => dest.KrugNadmetanja, opt => opt.MapFrom(src => src.KrugNadmetanja))
                .ForMember(dest => dest.StatusDrugiKrug, opt => opt.MapFrom(src => src.StatusDrugiKrug))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<NadmetanjeDataIn, Nadmetanje>()
                .ForMember(dest => dest.Tip, opt => opt.MapFrom(src => src.Tip))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CenaPoHektaru, opt => opt.MapFrom(src => src.CenaPoHektaru))
                .ForMember(dest => dest.DuzinaZakupa, opt => opt.MapFrom(src => src.DuzinaZakupa))
                .ForMember(dest => dest.EtapaId, opt => opt.MapFrom(src => src.EtapaId))
                .ForMember(dest => dest.KrugNadmetanja, opt => opt.MapFrom(src => src.KrugNadmetanja))
                .ForMember(dest => dest.StatusDrugiKrug, opt => opt.MapFrom(src => src.StatusDrugiKrug));
        }
    }
}
