using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.Services.Interfaces
{
    public interface IHttpService<T>
    {
        Task<T?> SendGetRequestAsync(string url, string token);
    }
}
