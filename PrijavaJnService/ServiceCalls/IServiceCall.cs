namespace PrijavaJnService.ServiceCalls
{
    public interface IServiceCall<T>
    {
        Task<T> SendGetRequestAsync(string url, string token);
    }
}
