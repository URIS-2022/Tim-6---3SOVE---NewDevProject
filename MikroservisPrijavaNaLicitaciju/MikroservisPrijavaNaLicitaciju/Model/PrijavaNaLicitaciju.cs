using System.ComponentModel.DataAnnotations;
namespace MikroservisPrijavaNaLicitaciju.Model
{
    public class PrijavaNaLicitaciju
    {

        [Key]
        public Guid IDPlic { get; set; }
        public string DatumPrijave { get; set; } = string.Empty;
        public string TipPrijave { get; set; } = string.Empty;
        public string IznosDepozita { get; set; } = string.Empty;

    }
}
