using AutoMapper;
using ms_zalba.Entities.UgovorOZakupuE;
using ms_zalba.Models.UgovorOZakupuModel;
using ms_zalba.Models.ZalbaModel;

namespace ms_zalba.Profiles
{
    public class UgovorOZakupuProfile : Profile
    {
        public UgovorOZakupuProfile()
        {
            CreateMap<UgovorOZakupu, UgovorOZakupuDto>();
            CreateMap<UgovorOZakupuCreationDto, UgovorOZakupu>();
            CreateMap<UgovorOZakupuUpdateDto, UgovorOZakupu>();
            CreateMap<UgovorOZakupuConfirmation, UgovorOZakupuConfirmationDto>();
            CreateMap<UgovorOZakupu, UgovorOZakupuConfirmation>();
            CreateMap<List<UgovorOZakupu>, UgovorOZakupuConfirmation>();

        }
    }
}
