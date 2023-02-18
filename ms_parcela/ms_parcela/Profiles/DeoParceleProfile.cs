using AutoMapper;
using ms_parcela.Entities.DeoParceleE;
using ms_parcela.Models.DeoParceleModel;

namespace ms_parcela.Profiles
{
    public class DeoParceleProfile :Profile
    {
        public DeoParceleProfile()
        {
            CreateMap<DeoParcele, DeoParceleDto>();
            CreateMap<DeoParceleCreationDto, DeoParcele>();
            CreateMap<DeoParceleUpdateDto, DeoParcele>();
            CreateMap<DeoParceleConfirmation, DeoParceleConfirmationDto>();
            CreateMap<DeoParcele, DeoParceleConfirmation>();




        }
    }
}
