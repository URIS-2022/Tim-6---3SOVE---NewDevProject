using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DokumentMicroservice.Entities
{
    [Table("Dokument")]
    public class Dokument
    {

        [Key]
        public Guid DokumentId { get; set; } = Guid.NewGuid();

        [Required]public string Sablon { get; set; }

        [Required] public DateTime DatumDonosenjaDokumenta { get; set; } 

        public Oglas Oglas { get; set; }

        public PredlogPlanaProjekta PredlogPlanaProjekta { get; set; }

        public ResenjeStrucnaKomisija ResenjeStrucnaKomisija { get; set; }


    }
}
