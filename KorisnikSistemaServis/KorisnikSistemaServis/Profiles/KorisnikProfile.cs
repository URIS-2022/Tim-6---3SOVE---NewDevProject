using AutoMapper;
using KorisnikSistemaServis.Entities;
using KorisnikSistemaServis.Models;
using KorisnikSistemaServis.Profiles;
namespace KorisnikSistemaServis.Profiles
{
    public class KorisnikProfile:Profile
    {
        public KorisnikProfile()
        {
            CreateMap<Korisnik, KorisnikDto>();
            CreateMap<KorisnikUpdateDto, Korisnik>();
            CreateMap<KorisnikCreationDto, Korisnik>();
            CreateMap<Korisnik, Korisnik>();
            CreateMap<KorisnikConfirmation, Korisnik>();
            CreateMap<Korisnik, KorisnikConfirmationDtocs>();
        }
    }
}
