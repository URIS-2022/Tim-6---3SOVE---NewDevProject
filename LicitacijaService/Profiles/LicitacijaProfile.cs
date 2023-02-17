using AutoMapper;
using LicitacijaService.Entities;
using LicitacijaService.Entities.Confirmations;
using LicitacijaService.Models.Licitacija;

namespace LicitacijaService.Profiles
{
    public class LicitacijaProfile : Profile
    {
        public LicitacijaProfile() 
        {
            CreateMap<Licitacija, LicitacijaDto>()
                .ForMember(
                    dest => dest.ProgramEntitetProgramId,
                    opt => opt.MapFrom(src => $"{src.ProgramEntitet.ProgramId}"));
            CreateMap<Licitacija, LicitacijaCreationDto>().ReverseMap();
            CreateMap<LicitacijaUpdateDto, Licitacija>().ReverseMap();
            CreateMap<Licitacija, Licitacija>();
            CreateMap<LicitacijaConfirmation, LicitacijaConfirmationDto>();
            CreateMap<Licitacija, LicitacijaConfirmation>();
        }
    }
}
