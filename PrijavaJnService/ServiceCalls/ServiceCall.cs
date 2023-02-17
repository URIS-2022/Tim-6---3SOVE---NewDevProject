﻿using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrijavaJnService.ServiceCalls
{
    public class ServiceCall<T> : IServiceCall<T>
    {
        //private readonly ILoggerService _loggerService;
        public ServiceCall()
        {
           // _loggerService = loggerService;
        }

        public async Task<T> SendGetRequestAsync(string url)
        {
            try
            {
                using var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
                //await _loggerService.Log(LogLevel.Error, "SendGetRequestAsync", $"Greška prilikom komunikacije sa drugim servisom iz servisa Zalba. Ciljani url: {url}", e);
                return default;
            }
        }
    }
}
