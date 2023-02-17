using AutoMapper;
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Models;
using System;
using DokumentMicroservice.Models.ResenjeStrucnaKomisija;

namespace DokumentMicroservice.Profiles
{

    public class ResenjeStrucnaKomisijaProfile : Profile
    {
        public ResenjeStrucnaKomisijaProfile() 
        {

            CreateMap<ResenjeStrucnaKomisija,CreateResenjeStrucnaKomisijaDto>().ReverseMap();
            CreateMap<UpdateResenjeStrucnaKomisijaDto, ResenjeStrucnaKomisija>().ReverseMap();
            CreateMap<ResenjeStrucnaKomisija, ResenjeStrucnaKomisija>().ReverseMap();
            CreateMap<ResenjeStrucnaKomisija,ResenjeStrucnaKomisijaDto>().ReverseMap();
            CreateMap<ResenjeStrucnaKomisijaConfirmation, ConfirmationResenjeStrucnaKomisijaDto>().ReverseMap();
            CreateMap<ResenjeStrucnaKomisija, ResenjeStrucnaKomisijaConfirmation>().ReverseMap();

        }

    }
}
