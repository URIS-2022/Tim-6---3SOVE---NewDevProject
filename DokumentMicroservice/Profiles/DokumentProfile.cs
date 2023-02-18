using AutoMapper;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Models;
using DokumentMicroservice.Models.Oglas;
using DokumentMicroservice.Models.PredlogPlanaProjekta;
using DokumentMicroservice.Models.ResenjeStrucnaKomisija;
using System;


namespace DokumentMicroservice.Profiles
{
    public class DokumentProfile : Profile
    {

        public DokumentProfile()
        {

            CreateMap<Dokument, CreateDokumentDto>().ReverseMap();
            CreateMap<UpdateDokumentDto, Dokument>().ReverseMap();
            CreateMap<Dokument, Dokument>().ReverseMap();
            CreateMap<Dokument, DokumentDto>().ReverseMap();
            CreateMap<DokumentConfirmation, ConfirmationDokumentDto>().ReverseMap();
            CreateMap<Dokument, DokumentConfirmation>().ReverseMap();




        }



    }
}
