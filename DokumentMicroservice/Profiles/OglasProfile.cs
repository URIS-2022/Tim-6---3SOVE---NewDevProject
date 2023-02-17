
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Models;
using System;
using AutoMapper;
using DokumentMicroservice.Models.Oglas;

namespace DokumentMicroservice.Profiles
{
    public class OglasProfile : Profile
    {

        public OglasProfile()
        { 

            CreateMap<Oglas, CreateOglasDto>().ReverseMap();
            CreateMap<UpdateOglasDto, Oglas>().ReverseMap();
            CreateMap<Oglas, Oglas>().ReverseMap();
            CreateMap<Oglas, OglasDto>().ReverseMap();
            CreateMap<OglasConfirmation, ConfirmationOglasDto>().ReverseMap();
            CreateMap<Oglas, OglasConfirmation>().ReverseMap();

        }
    }
}
