namespace LicitacijaService.ServiceCalls
{
    public interface IServiceCall<T>
    {
        //, string token
        Task<T> SendGetRequestAsync(string url);
    }
}
