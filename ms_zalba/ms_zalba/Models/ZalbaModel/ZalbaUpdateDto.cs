using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ms_zalba.Models.ZalbaModel
{
    public enum StatusZalbeUpd { usvojena, odbijena, otvorena }
    public enum RadnjaNaOsnovuZalbeUpd { JN_ide_u_drugi_krug_sa_novim_uslovima, JN_ide_u_drugi_krug_sa_starim_uslovima, JN_ne_ide_u_drugi_krug }
    public class ZalbaUpdateDto
    {
        public Guid zalbaID { get; set; }
        //[ForeignKey("TipZalbes")]
        public Guid tipZalbe { get; set; }
        //[DataType(DataType.Date)]
        //[Column(TypeName = "date")]
        public DateTime datumPodnosenjaZalbe { get; set; }
        public string razlogZalbe { get; set; }
        public string obrazlozenje { get; set; }
        //[DataType(DataType.Date)]
        //[Column(TypeName = "date")]
        public DateTime datumResenja { get; set; }
        public string brojResenja { get; set; }
        public StatusZalbeUpd statusZalbe { get; set; }
        public int brojNadmetanja { get; set; }
        public RadnjaNaOsnovuZalbeUpd radnjaNaOsnovuZalbe { get; set; }
        public Guid podnosilacZalbe { get; set; }
    }
}
