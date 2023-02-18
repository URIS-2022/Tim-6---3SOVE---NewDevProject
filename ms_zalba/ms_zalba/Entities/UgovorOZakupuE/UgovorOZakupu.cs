using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_zalba.Entities.UgovorOZakupuE
{
    public class UgovorOZakupu
    {
      
        [Key]
        public Guid idUgovor { get; set; }
        public string tipGarancije { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime rokDospeca { get; set; }
        public int zavodniBroj { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime datumZavodjanja { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime rokZaVracanjeZemljista { get; set; }
        public string mestoPotpisivanja { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime datumPotpisa { get; set; }
    }
}
