using AutoMapper;
using KorisnikSistemaServis.Entities;
using KorisnikSistemaServis.Models;
using Microsoft.Identity.Client;

namespace KorisnikSistemaServis.Profiles
{
    public class KorisnikConfirmationProfile:Profile
    {
        public KorisnikConfirmationProfile()
        {
            CreateMap<KorisnikConfirmation, KorisnikConfirmationDtocs>();
            CreateMap<Korisnik, KorisnikConfirmation>();
        }
    }
}
