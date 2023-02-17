using AutoMapper;
using KupacMicroservice.Entities;
using KupacMicroservice.Entities.DataConfirmations;
using KupacMicroservice.model.Kupac;
using System;


namespace KupacMicroservice.Profiles
{
    public class KupacProfile : Profile
    {
        public KupacProfile()
        {

            CreateMap<Kupac, CreateKupacDto>().ReverseMap();
            CreateMap<UpdateKupacDto, Kupac>().ReverseMap();
            CreateMap<Kupac, Kupac>().ReverseMap();
            CreateMap<Kupac, KupacDto>().ReverseMap();
            CreateMap<KupacConfirmation, ConfirmationKupacDto>().ReverseMap();
            CreateMap<Kupac, KupacConfirmation>().ReverseMap();

        }
    }
}
