using System;
using System.ComponentModel.DataAnnotations;

namespace KupacMicroservice.model.PravnoLice
{

    /// <summary>
    /// dto za kreiranje pravnog lica
    /// </summary>
    public class CreatePravnoLiceDto
    {
        /// <summary>
        /// id pravno lice
        /// </summary>
        public Guid PravnoliceId { get; set; }

        /// <summary>
        /// id kupac
        /// </summary>
        public Guid KupacId { get; set; }

        /// <summary>
        /// naziv pravnog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv pravnog lica.")]
        public string Naziv { get; set; }

        /// <summary>
        /// maticni broj pravnog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti maticni broj pravnog lica.")]
        public string MaticniBroj{ get; set; }

        /// <summary>
        /// faks pravno lice
        /// </summary>
        public string Faks { get; set; }

        /// <summary>
        /// adresa pravno lice
        /// </summary>
        public string AdresaPravnoLice { get; set; }

        /// <summary>
        /// kontakt osoba ime
        /// </summary>
        public string KontaktOsobaIme { get; set; }

        /// <summary>
        /// kontakt osoba prezime
        /// </summary>
        public string KontaktOsobaPrezime { get; set; }


    }
}
