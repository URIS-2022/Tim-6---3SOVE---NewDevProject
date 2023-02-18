namespace ms_parcela.ServiceCalls
{
    public interface IServiceCall<T>
    {
        Task<T> SendGetRequestAsync(string url, string token);    
    }
}
