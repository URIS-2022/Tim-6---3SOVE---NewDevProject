using AutoMapper;
using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.Entities;
using KupacMicroservice.model.FizickoLice;
using System;
using KupacMicroservice.model.PravnoLice;

namespace KupacMicroservice.Profiles
{
    public class PravnoLiceProfile : Profile
    {
        public PravnoLiceProfile()
        {

            CreateMap<PravnoLice, CreatePravnoLiceDto>().ReverseMap();
            CreateMap<UpdatePravnoLiceDto, PravnoLice>().ReverseMap();
            CreateMap<PravnoLice, PravnoLice>().ReverseMap();
            CreateMap<PravnoLice, PravnoLiceDto>().ReverseMap();
            CreateMap<PravnoLiceConfirmation, ConfirmationPravnoLiceDto>().ReverseMap();
            CreateMap<PravnoLice, PravnoLiceConfirmation>().ReverseMap();

        }

    }
}
