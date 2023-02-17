using AutoMapper;
using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.Entities;
using System;
using KupacMicroservice.Model.Liciter;

namespace KupacMicroservice.Profiles
{
    public class LiciterProfile : Profile
    {

        public LiciterProfile()
        {

            CreateMap<Liciter, CreateLiciterDto>().ReverseMap();
            CreateMap<UpdateLiciterDto, Liciter>().ReverseMap();
            CreateMap<Liciter, Liciter>().ReverseMap();
            CreateMap<Liciter, LiciterDto>().ReverseMap();
            CreateMap<LiciterConfirmation, ConfirmationLiciterDto>().ReverseMap();
            CreateMap<Liciter, LiciterConfirmation>().ReverseMap();



        }

    }
}
