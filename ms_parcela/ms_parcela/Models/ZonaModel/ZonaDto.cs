using ms_parcela.Models;

namespace ms_parcela.Models.ZonaModel
{
    public enum brojevi_zonaDto
    {
        zona1, zona2, zona3, zona4
    }
    public class ZonaDto
    {
        public Guid zonaID { get; set; }

        public brojevi_zonaDto brojZona { get; set; }

        //public IEnumerable<Parcela> Parcela { get; set; }
    }
}
