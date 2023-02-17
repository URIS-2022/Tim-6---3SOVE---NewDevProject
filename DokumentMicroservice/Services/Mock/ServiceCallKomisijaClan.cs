using DokumentMicroservice.Models.OtherService;
using System;


namespace DokumentMicroservice.Services.Mock
{
    public class ServiceCallKomisijaClan<T> : IServiceCall<T>
    {
        
        public async Task<T> SendGetRequestAsync(string url, string token)
        {
            var komisija = new Komisija
            {
                
                  IDKomsije= Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                  ImeKomisije="test",
                  Ovlascenje ="test",
                  OznakaKomisije= "test"
  
            };
            return await Task.FromResult((T)Convert.ChangeType(komisija, typeof(T)));

        }
    }
}


