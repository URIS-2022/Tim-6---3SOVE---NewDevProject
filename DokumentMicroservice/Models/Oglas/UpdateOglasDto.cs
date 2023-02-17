using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models.Oglas
{
    /// <summary>
    /// model za azuriranje oglasa
    /// </summary>
    public class UpdateOglasDto
    {
        /// <summary>
        /// id oglasa
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id oglasa")]
        public Guid OglasId { get; set; }

        ///<summary>
        /// id dokumenta
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id dokumenta")]
        public Guid DokumentId { get; set; }

        /// <summary>
        /// broj nadmetanja za fizicka lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj nadmetanja za fizicka lica ")]
        public string BrNadmetanjaJmbg { get; set; }

        ///<summary>
        /// broj nadmetanja za pravna lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj nadmetanja za pravna lica")]
        public string BrNadmetanjaMaticniBrojPreduzeca { get; set; }

    }
}
