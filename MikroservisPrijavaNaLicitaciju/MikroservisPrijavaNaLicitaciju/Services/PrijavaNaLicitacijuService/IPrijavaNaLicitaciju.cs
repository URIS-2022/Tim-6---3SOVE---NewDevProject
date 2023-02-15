using MikroservisPrijavaNaLicitaciju.Services;
namespace MikroservisPrijavaNaLicitaciju.Services.PrijavaNaLicitacijuService
{
    public interface IPrijavaNaLicitaciju
    {
        Task<List<PrijavaNaLicitaciju>> GetAllPLics();
        Task<PrijavaNaLicitaciju?> GetSinglePLic(Guid id);
        Task<List<PrijavaNaLicitaciju>> AddPLic(PrijavaNaLicitaciju prijavanalicitaciju);

        Task<List<PrijavaNaLicitaciju>?> UpdatePLic(Guid id, PrijavaNaLicitaciju request);
        Task<List<PrijavaNaLicitaciju>?> DeletePLic(Guid id);
    }
}
