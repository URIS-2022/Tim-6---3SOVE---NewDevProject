using AutoMapper;
using LicitacijaService.Entities;
using LicitacijaService.Models.ProgramEntitet;

namespace LicitacijaService.Profiles
{
    public class ProgramEntitetProfile : Profile
    {
        public ProgramEntitetProfile() 
        {
            CreateMap<ProgramEntitet, ProgramEntitetCreationDto>().ReverseMap();
            CreateMap<ProgramEntitetUpdateDto, ProgramEntitet>();
            CreateMap<ProgramEntitet, ProgramEntitet>();
            CreateMap<ProgramEntitet, ProgramEntitetDto>();
        }
    }
}
