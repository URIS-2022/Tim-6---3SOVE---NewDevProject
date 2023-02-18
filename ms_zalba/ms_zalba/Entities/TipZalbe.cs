using System.ComponentModel.DataAnnotations;

namespace ms_zalba.Entities
{
    public class TipZalbe
    {
        [Key]
        public Guid idTipZalbe { get; set; }
        public string nazivTipaZalbe { get; set; }
    }
}
