using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ms_zalba.Entities.ZalbaE
{
    public enum StatusZalbeConfirm { usvojena, odbijena, otvorena }
    public class ZalbaConfirmation
    {
        public Guid zalbaID { get; set; }
        public Guid tipZalbe { get; set; }
        //[DataType(DataType.Date)]
        //[Column(TypeName = "date")]
        public DateTime datumPodnosenjaZalbe { get; set; }
        public string razlogZalbe { get; set; }
        public StatusZalbeConfirm statusZalbe { get; set; }

    }
}
