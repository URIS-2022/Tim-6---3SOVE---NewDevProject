using ms_parcela.Models;

namespace ms_parcela.Models.ZonaModel
{
    public enum brojevi_zonaUpdDto
    {
        zona1, zona2, zona3, zona4
    }
    public class ZonaUpdateDto
    {
        public Guid zonaID { get; set; }

        public brojevi_zonaUpdDto brojZona { get; set; }
    }
}
