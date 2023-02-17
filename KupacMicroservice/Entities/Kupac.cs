using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupacMicroservice.Entities
{
    [Table("Kupac")]
    public class Kupac
    {

        [Key]
        public Guid KupacId { get; set; } = Guid.NewGuid();

        [Required] public string AdresaKupac { get; set; } 

        public double OstvarenaPovrsina { get; set; }

        public bool ImaZabranu { get; set; }

        public DateTime DatumPocetkaZabrane { get; set; }

        public int DuzinaTrajanjaZabraneGod { get; set; }

        public string BrojTelefona1 { get; set; }

        public string BrojTelefona2 { get; set; }

        public string Email { get; set; }

        public string BrojRacuna { get; set; }

        public string IznosUplata { get; set; }

        public string Prioritet { get; set; }
        
        public FizickoLice FizickoLice { get; set; }

        public PravnoLice PravnoLice { get; set; }

        [NotMapped]
        public List<Guid> Liciteri { get; set; }

      










    }
}
