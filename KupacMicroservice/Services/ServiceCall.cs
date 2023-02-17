using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Net.Http.Headers;

namespace KupacMicroservice.Services
{
    public class ServiceCall : IServiceCall<T>
    {

        /// <summary>
        /// Metoda za slanje get zahteva
        /// </summary>
        /// <param name="url">Url putanja ka drugom servisu</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T> SendGetRequestAsync(string url, string token)
        {
            try
            {
                using var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }

                    return JsonConvert.DeserializeObject<T>(content);
                }
                return default;
            }
            catch (Exception e)
            {
                //await _loggerService.Log(LogLevel.Error, "SendGetRequestAsync", $"Greška prilikom komunikacije sa drugim servisom iz servisa Javno Nadmetanje. Ciljani url: {url}", e);
                return default;
            }

        }
    }
}