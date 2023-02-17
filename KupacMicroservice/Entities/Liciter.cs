using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupacMicroservice.Entities
{

    [Table("Liciter")]
    public class Liciter
    {

        [Key]
        public Guid LiciterId { get; set; } = Guid.NewGuid();

        [Required] public string ImeLiciter { get; set; }

        [Required] public string PrezimeLiciter { get; set; }

        public string JmbgLiciter { get; set; }

        public string Brojpasosa { get; set; }

        public string Drzavastranac { get; set; }

        public string AdresaLiciter { get; set; }

        [NotMapped]

        public List<Guid> Kupci { get; set; }



    }
}
