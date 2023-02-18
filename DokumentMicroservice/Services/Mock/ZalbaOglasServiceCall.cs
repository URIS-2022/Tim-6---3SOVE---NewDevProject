using DokumentMicroservice.Services;
using System;

namespace DokumentMicroservice.Services.Mock
{
    public class ZalbaOglasServiceCall<T> : IServiceCall<T>
    {

        public async Task<T> SendGetRequestAsync(string url)
        {
            var zalba = new ZalbaDto
            {

                Naziv = "zalba1",
                Obrazlozenje = "zalba na oglas"

            };

            return await Task.FromResult((T)Convert.ChangeType(zalba, typeof(T)));
        }


    }
}





