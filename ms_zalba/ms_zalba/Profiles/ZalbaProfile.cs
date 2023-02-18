using AutoMapper;
using ms_zalba.Entities.ZalbaE;
using ms_zalba.Models.ZalbaModel;

namespace ms_zalba.Profiles
{
    public class ZalbaProfile : Profile
    {
        public ZalbaProfile()
        {
            CreateMap<Zalba, ZalbaDto>();
            CreateMap<ZalbaCreationDto, Zalba>();
            CreateMap<ZalbaUpdateDto, Zalba>();
            CreateMap<ZalbaConfirmation, ZalbaConfirmationDto>();
            CreateMap<Zalba, ZalbaConfirmation>();
            CreateMap<List<Zalba>, ZalbaConfirmation>();

        }
    }
}
