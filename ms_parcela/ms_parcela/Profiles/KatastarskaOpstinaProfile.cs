using AutoMapper;
using ms_parcela.Entities;
using ms_parcela.Models.KatastarskaOpstinaModel;

namespace ms_parcela.Profiles
{
    public class KatastarskaOpstinaProfile : Profile
    {
        public KatastarskaOpstinaProfile()
        {
            CreateMap<KatastarskaOpstina, KatastarskaOpstinaDto>();
            CreateMap<KatastarskaOpstinaCreationDto, KatastarskaOpstina>();
            CreateMap<KatastarskaOpstinaUpdateDto, KatastarskaOpstina>();
            //CreateMap<List<KatastarskaOpstinaUpdateDto>, KatastarskaOpstinaDto>();
            //CreateMap<List<KatastarskaOpstina>, KatastarskaOpstinaDto>();

        }
    }
}
