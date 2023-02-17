using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupacMicroservice.Entities
{

    [Table("FizickoLice")]
    public class FizickoLice
    {

        [Key]
        public Guid FizickoliceId { get; set; } = Guid.NewGuid();

        [ForeignKey("Kupac")]
        public Guid KupacId { get; set; }

        [Required] public string Ime { get; set; }

        [Required] public string Prezime { get; set; }

        public string JMBG { get; set; }

        public string AdresaFizickoLice { get; set; }


    }
}
