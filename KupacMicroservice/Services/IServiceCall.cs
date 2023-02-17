﻿using System;


namespace KupacMicroservice.Services
{

    /// <summary>
    /// Interfejs za komunikaciju sa drugim servisima
    /// </summary>
    /// <typeparam name="T">Generic</typeparam>
    public interface IServiceCall<T>
    {
        /// <summary>
        /// Metoda za slanje get zahteva
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<T> SendGetRequestAsync(string url, string token);
    }
}
