using ms_parcela.Models;

namespace ms_parcela.Models.ZonaModel
{
    public enum brojevi_zonaAddDto
    {
        zona1, zona2, zona3, zona4
    }
    public class ZonaCreationDto
    {
        public brojevi_zonaAddDto brojZona { get; set; }
    }
}
