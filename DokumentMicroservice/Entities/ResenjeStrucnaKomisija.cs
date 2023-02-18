using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DokumentMicroservice.Entities
{
    [Table("ResenjeStrucnaKomisije")]
    public class ResenjeStrucnaKomisija 
    {
        [Key]
        public Guid ResenjeId { get; set; } = Guid.NewGuid();

        [ForeignKey("Dokument")]
        public Guid DokumentId { get; set; }

        [Required] public string Zavodnibr { get; set; }

        public DateTime DatumResenje { get; set; } = DateTime.Now;

        public string? ImeClanaKomisije { get; set; }

        public string? PrezClanaKomisije { get; set; }

        public string? PredsednikKomisije { get; set; }


    }
}
