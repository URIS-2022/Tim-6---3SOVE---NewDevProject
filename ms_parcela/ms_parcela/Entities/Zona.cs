using System.ComponentModel.DataAnnotations;

namespace ms_parcela.Entities
{
    public enum brojevi_zona
    {
        zona1, zona2, zona3, zona4
    }
    
    public class Zona
    {
        [Key]
        public Guid zonaID { get; set; }

        public brojevi_zona brojZona { get; set; }

    }
}
