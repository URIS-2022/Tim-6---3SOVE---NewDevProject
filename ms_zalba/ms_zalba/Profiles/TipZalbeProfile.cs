using AutoMapper;
using ms_zalba.Entities;
using ms_zalba.Models.TipZalbeModel;

namespace ms_zalba.Profiles
{
    public class TipZalbeProfile : Profile
    {
        public TipZalbeProfile()
        {
            CreateMap<TipZalbe, TipZalbeDto>();
            CreateMap<TipZalbeCreationDto, TipZalbe>();
            CreateMap<TipZalbeUpdateDto, TipZalbe>();
        }
    }
}
