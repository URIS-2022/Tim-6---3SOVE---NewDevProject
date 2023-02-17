using KupacMicroservice.Entities;
using KupacMicroservice.model.FizickoLice;
using KupacMicroservice.model.PravnoLice;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KupacMicroservice.model.Kupac
{

    /// <summary>
    /// dto za kupca
    /// </summary>
    public class KupacDto
    {


        /// <summary>
        /// id kupac
        /// </summary>
        public Guid KupacId { get; set; }

        /// <summary>
        /// adresa kupac
        /// </summary>
        public string AdresaKupac { get; set; }

        /// <summary>
        /// ostvarena podrska
        /// </summary>
        public double OstvarenaPovrsina { get; set; }

        /// <summary>
        /// ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }

        /// <summary>
        /// datum pocetka zabrane
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime DatumPocetkaZabrane { get; set; }

        /// <summary>
        /// duzina trajanja zabrana godine
        /// </summary>
        public int DuzinaTrajanjaZabraneGod { get; set; }

        /// <summary>
        /// broj teledona 1
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// broj telefona 2
        /// </summary>
        public string BrojTelefona2 { get; set; }

        /// <summary>
        /// email kupac
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// broj racuna
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// iznos uplata
        /// </summary>
        public string IznosUplata { get; set; }

        /// <summary>
        /// prioritet
        /// </summary>
        public string Prioritet { get; set; }

        /// <summary>
        /// fizicko lice
        /// </summary>
        public FizickoLiceDto FizickoLice { get; set; }

        /// <summary>
        /// pravno lice
        /// </summary>
        public PravnoLiceDto PravnoLice { get; set; }

        /// <summary>
        /// liciteri
        /// </summary>
        [JsonIgnore]
        public List<Guid> Liciteri { get; set; }

    }
}
