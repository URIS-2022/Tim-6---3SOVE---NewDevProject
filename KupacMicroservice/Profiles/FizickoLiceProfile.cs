using AutoMapper;
using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.model.FizickoLice;
using System;

namespace KupacMicroservice.Profiles
{
    public class FizickoLiceProfile : Profile
    {

        public FizickoLiceProfile() 
        {

        CreateMap<FizickoLice, CreateFizickoLiceDto>().ReverseMap();
        CreateMap<UpdateFizickoLiceDto, FizickoLice>().ReverseMap();
        CreateMap<FizickoLice, FizickoLice>().ReverseMap();
        CreateMap<FizickoLice, FizickoLiceDto>().ReverseMap();
        CreateMap<FizickoLiceConfirmation, ConfirmationFizickoLiceDto>().ReverseMap();
        CreateMap<FizickoLice, FizickoLiceConfirmation>().ReverseMap();

        }
    }
}
