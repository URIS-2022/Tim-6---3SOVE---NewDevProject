using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_zalba.Entities.ZalbaE
{
    public enum StatusZalbe { usvojena, odbijena, otvorena }
    public enum RadnjaNaOsnovuZalbe { JN_ide_u_drugi_krug_sa_novim_uslovima, JN_ide_u_drugi_krug_sa_starim_uslovima, JN_ne_ide_u_drugi_krug }
    public class Zalba
    {

        public Guid zalbaID { get; set; }
        [ForeignKey("TipZalbes")]
        public Guid tipZalbe { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime datumPodnosenjaZalbe { get; set; }
        public string razlogZalbe { get; set; }
        public string obrazlozenje { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime datumResenja { get; set; }
        public string brojResenja { get; set; }
        public StatusZalbe statusZalbe { get; set; }
        public int brojNadmetanja { get; set; }
        public RadnjaNaOsnovuZalbe radnjaNaOsnovuZalbe { get; set; }
        public Guid podnosilacZalbe { get; set; }
    }
}
