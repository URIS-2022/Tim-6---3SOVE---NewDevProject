using KupacMicroservice.model.FizickoLice;
using KupacMicroservice.model.PravnoLice;
using System;
using System.ComponentModel.DataAnnotations;

namespace KupacMicroservice.model.Kupac
{   

    /// <summary>
    /// dto za kreiranje kupca
    /// </summary>
    public class CreateKupacDto
    {

        /// <summary>
        /// id kupac
        /// </summary>
        public Guid KupacId { get; set; }

        /// <summary>
        /// adresa kupac
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti adresu kupca")]
        public string AdresaKupac { get; set; }

        /// <summary>
        /// ostvarena povrsina
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
        /// duzina trajanja zabrane 
        /// </summary>
        public int DuzinaTrajanjaZabraneGod { get; set; }

        /// <summary>
        /// broj telefona 1
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
        /// prioritet kupca
        /// </summary>
        public string Prioritet { get; set; }

       


    }
}
