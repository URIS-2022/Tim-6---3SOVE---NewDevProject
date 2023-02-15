using AutoMapper;
using KorisnikSistemaServis.Entities;

namespace KorisnikSistemaServis.Data
{
    public class KorisnikRepository: IKorisnikRepository
    {
        private readonly KorisnikContext context;
        private readonly IMapper mapper;
        public static List<Korisnik> Korisnici { get; set; } = new List<Korisnik>();

        public KorisnikRepository(KorisnikContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public KorisnikConfirmation CreateKorisnik(Korisnik korisnik)
        {
            var userEntity = context.Add(korisnik);
            return mapper.Map<KorisnikConfirmation>(userEntity.Entity);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void DeleteKorisnik(Guid korisnikId)
        {
            var user = GetKorisnikById(korisnikId);
            context.Remove(user);
        }

        public List<Korisnik> GetAllKorisnik(string korisnickoIme = null)
        {
            return context.Korisnici.Where(k => korisnickoIme == null || k.KorisnickoIme == korisnickoIme).ToList();
        }

        public Korisnik GetKorisnikById(Guid korisnikId)
        {
            return context.Korisnici.FirstOrDefault(k => k.KorisnikId == korisnikId);
        }

        public void UpdateKorisnik(Korisnik korisnik)
        {
            //throw new NotImplementedException();
        }

        public Korisnik GetKorisnikByKorisnickoIme (string korisnickoIme)
        {
            return context.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnickoIme);
        }

    }
}
