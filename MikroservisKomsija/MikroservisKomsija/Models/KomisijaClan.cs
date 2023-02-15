using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikroservisKomsija.Models
{
    public class KomisijaClan
    {
        [ForeignKey("Koms")]
        public Guid IDKomsije { get; set; }

        [ForeignKey("Clans")]
        public int IDClan { get; set; }
        public Boolean IsPredsjednik { get; set; } = false;
    }
}
