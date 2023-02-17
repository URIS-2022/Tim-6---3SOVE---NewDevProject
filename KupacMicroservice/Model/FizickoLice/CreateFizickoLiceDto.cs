using System;
using System.ComponentModel.DataAnnotations;

namespace KupacMicroservice.model.FizickoLice
{

    /// <summary>
    /// dto za kreiranje fizickog lica
    /// </summary>
    public class CreateFizickoLiceDto
    {

        
        /// <summary>
        /// id fizicko lice
        /// </summary>
        public Guid FizickoliceId { get; set; } 

        /// <summary>
        /// id kupac
        /// </summary>
        public Guid KupacId { get; set; }

        /// <summary>
        /// ime fizicko lice
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ime fizickog lica.")]
        public string Ime { get; set; }


        /// <summary>
        /// prezime fizicko lice
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti prezime fizickog lica.")]
        public string Prezime { get; set; }

        /// <summary>
        /// jmbg fizicko lice
        /// </summary>
        public string JMBG { get; set; }

        /// <summary>
        /// adresa fizicko lice
        /// </summary>
        public string AdresaFizickoLice { get; set; }




    }




}
