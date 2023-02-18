using PrijavaJnService.ServiceCalls;

namespace PrijavaJnService.ServiceCalls.Mocks
{
    public class ServiceCallKupacMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url, string token)
        {
            var kupac = new KupacDto
            {
                AdresaKupac = "Zrenjanin bb",
                OstvarenaPovrsina = 44.00,
                ImaZabranu = true,
                DuzinaTrajanjaZabraneGod = 2,
                BrojTelefona1 = "32422",
                BrojTelefona2 = "31231211",
                Email = "pera@gmail.com",
                BrojRacuna = "43241-3213-111",
                IznosUplata = "4999",
                Prioritet = "Hitno"
            };

            return await Task.FromResult((T)Convert.ChangeType(kupac, typeof(T)));
        }
    }
}
