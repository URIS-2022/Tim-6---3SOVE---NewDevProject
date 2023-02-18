using DokumentMicroservice.Services.Mock;
using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models.Oglas
{
    /// <summary>
    /// model za kreiranje oglasa
    /// </summary>
    public class CreateOglasDto
    {

        /// <summary>
        /// id dokumenta
        /// </summary>
        public Guid OglasId { get; set; }

        /// <summary>
        /// id dokumenta
        /// </summary>
        public Guid DokumentId { get; set; }

        ///<summary>
        /// broj nadmetanja za fizicka lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti BrojNadmetanjaJmbg.")]
        public string BrNadmetanjaJmbg { get; set; }

        ///<summary>
        /// broj nadmetanja za pravna lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti BrojNadmetanjaMaticniBrojPreduzeca.")]
        public string BrNadmetanjaMaticniBrojPreduzeca { get; set; }

        public Guid? zalbaID { get; set; }

        public ZalbaDto Zalba { get; set; }

    }
}
