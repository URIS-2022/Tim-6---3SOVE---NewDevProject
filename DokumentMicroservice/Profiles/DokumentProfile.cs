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

            /*CreateMap<Dokument, DokumentDto>()
             .ForMember(dto => dto.Oglas, opt => opt.MapFrom(d => d.Oglas.OglasId))
             .ForMember(dto => dto.PredlogPlanaProjekta, opt => opt.MapFrom(d => d.PredlogPlanaProjekta.PredlogId))
             .ForMember(dto => dto.ResenjeStrucneKomisije, opt => opt.MapFrom(d => d.ResenjeStrucnaKomisija.ResenjeId))
             .ReverseMap();

            CreateMap<Oglas, OglasDto>().ReverseMap();
            CreateMap<PredlogPlanaProjekta, PredlogPlanaProjektaDto>().ReverseMap();
            CreateMap<ResenjeStrucnaKomisija, ResenjeStrucnaKomisijaDto>().ReverseMap();*/



        }



    }
}
