using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Entities
{
    [Table("Oglas")]
    public class Oglas 
    {

        [Key]
        public Guid OglasId { get; set; } = Guid.NewGuid();

        [ForeignKey("Dokument")]
        public Guid DokumentId { get; set; }

        [Required] public string BrNadmetanjaJmbg { get; set; }

        [Required] public string BrNadmetanjaMaticniBrojPreduzeca { get; set; }

        public Guid? zalbaID { get; set; }


    }
}
