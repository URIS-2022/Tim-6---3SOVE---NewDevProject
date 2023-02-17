using AutoMapper;
using DokumentMicroservice.Entities;
using DokumentMicroservice.Entities.DataConfirmations;
using DokumentMicroservice.Models;
using DokumentMicroservice.Models.PredlogPlanaProjekta;
using System;


namespace DokumentMicroservice.Profiles
{
    public class PredlogPlanaProjektaProfile : Profile
    {
        public PredlogPlanaProjektaProfile()
        {

            CreateMap<PredlogPlanaProjekta, CreatePredlogPlanaProjektaDto>().ReverseMap();
            CreateMap<UpdatePredlogPlanaProjektaDto, PredlogPlanaProjekta>().ReverseMap();
            CreateMap<PredlogPlanaProjekta, PredlogPlanaProjekta>().ReverseMap();
            CreateMap<PredlogPlanaProjekta,PredlogPlanaProjektaDto>().ReverseMap();
            CreateMap<PredlogPlanaProjektaConfirmation, ConfirmationPredlogPlanaProjektaDto>().ReverseMap();
            CreateMap<PredlogPlanaProjekta, PredlogPlanaProjektaConfirmation>().ReverseMap();
        }

    }
}
