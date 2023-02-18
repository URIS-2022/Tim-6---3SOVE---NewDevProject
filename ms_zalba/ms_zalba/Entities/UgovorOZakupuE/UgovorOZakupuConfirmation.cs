using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ms_zalba.Entities.UgovorOZakupuE
{
    public class UgovorOZakupuConfirmation
    {
        //public Guid JavnoNadmetanje { get; set; }
        //public Guid Odluka { get; set; }
        public Guid idUgovor { get; set; }
        //public Guid lice { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime rokDospeca { get; set; }
        public int zavodniBroj { get; set; }
    }
}
