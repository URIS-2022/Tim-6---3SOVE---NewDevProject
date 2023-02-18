using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ms_zalba.Models.ZalbaModel
{
    public enum StatusZalbeConfirmDto { usvojena, odbijena, otvorena }

    public class ZalbaConfirmationDto
    {
        public Guid tipZalbe { get; set; }
        //[DataType(DataType.Date)]
        //[Column(TypeName = "date")]
        public DateTime datumPodnosenjaZalbe { get; set; }
        public string razlogZalbe { get; set; }
        public StatusZalbeConfirmDto statusZalbe { get; set; }

    }
}
