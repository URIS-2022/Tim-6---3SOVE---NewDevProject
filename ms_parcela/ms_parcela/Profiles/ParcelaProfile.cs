using AutoMapper;
using ms_parcela.Entities.ParcelaE;
using ms_parcela.Models.ParcelaModel;

namespace ms_parcela.Profiles
{
    public class ParcelaProfile : Profile
    {
        public ParcelaProfile()
        {
            CreateMap<Parcela, ParcelaDto>();
            CreateMap<ParcelaCreationDto, Parcela>();
            CreateMap<ParcelaUpdateDto, Parcela>();
            CreateMap<ParcelaConfirmation, ParcelaConfirmationDto>();
            CreateMap<Parcela, ParcelaConfirmation>();
            CreateMap<List<Parcela>, ParcelaConfirmation>();
        }
    }
}
