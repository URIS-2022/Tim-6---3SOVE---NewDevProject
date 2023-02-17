using AutoMapper;
using PrijavaJnService.Entities;
using PrijavaJnService.Entities.Confirmations;
using PrijavaJnService.Models.PrijavaJn;

namespace PrijavaJnService.Profiles
{
    public class PrijavaJnProfile : Profile
    {
        public PrijavaJnProfile()
        {
            CreateMap<PrijavaJn, PrijavaJnCreationDto>().ReverseMap();
            CreateMap<PrijavaJnUpdateDto, PrijavaJn>().ReverseMap();
            CreateMap<PrijavaJn, PrijavaJn>();
            CreateMap<PrijavaJn, PrijavaJnDto>();
            CreateMap<PrijavaJnConfirmation, PrijavaJnConfirmationDto>();
            CreateMap<PrijavaJn, PrijavaJnConfirmation>();
        }
    }
}
