using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupacMicroservice.Entities
{
    [Table("PravnoLice")]
    public class PravnoLice 
    {

        [Key]
        public Guid PravnoliceId { get; set; } = Guid.NewGuid();

        [ForeignKey("Kupac")]
        public Guid KupacId { get; set; }

        [Required]public string Naziv { get; set; }

        [Required]public string MaticniBroj { get; set; }

        public string Faks { get; set; }

        public string KontaktOsobaIme { get; set; }

        public string KontaktOsobaPrezime { get; set; }

        public string AdresaPravnoLice { get; set; } 

    }
}
