using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Entities
{
    [Table("PredlogPlanaProjekta")]
    public class PredlogPlanaProjekta 
    {

        [Key]
        public Guid PredlogId { get; set; } = Guid.NewGuid();

        [ForeignKey("Dokument")]
        public Guid DokumentId { get; set; }

        [Required] public string ZavodniBr { get; set; }

        public DateTime DatumPredlog { get; set; } = DateTime.Now;

    }
}
