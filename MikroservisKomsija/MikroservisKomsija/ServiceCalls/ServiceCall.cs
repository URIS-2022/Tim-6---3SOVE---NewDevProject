﻿using Microsoft.Extensions.Logging;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MikroservisKomsija.ServiceCalls
{
    public class ServiceCall<T> : IServiceCall<T>
    {
    

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
              return default;
            }

        }
    }
}
