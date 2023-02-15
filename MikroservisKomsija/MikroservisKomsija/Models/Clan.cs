using System.ComponentModel.DataAnnotations;
namespace MikroservisKomsija.Models
{
    public class Clan
    {
        [Key]
        public int IDClan { get; set; }
        public string ImeClana { get; set; } = string.Empty;
        public string PrezimeClana { get; set; } = string.Empty;
        public string Mjesto { get; set; } = string.Empty;

        public string DatumRodjenja { get; set; } = string.Empty;

    }
}
