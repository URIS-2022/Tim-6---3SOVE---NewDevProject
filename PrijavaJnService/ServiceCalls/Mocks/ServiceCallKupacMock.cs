using PrijavaJnService.ServiceCalls;

namespace PrijavaJnService.ServiceCalls.Mocks
{
    public class ServiceCallKupacMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            var kupac = new KupacDto
            {
                Naziv = "Mladen Mladic",
                Email = "mladen@gmail.com",
                BrojRacuna = "123123123",
                BrojTelefona = "064222222"
            };

            return await Task.FromResult((T)Convert.ChangeType(kupac, typeof(T)));
        }
    }
}
