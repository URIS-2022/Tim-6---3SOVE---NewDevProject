using NPOI.SS.Formula.Functions;
using System;


namespace DokumentMicroservice.Services
{
    public interface IServiceCall<T>
    {

        Task<T> SendGetRequestAsync(string url);
    }
}
