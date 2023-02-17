using System;
using System.ComponentModel.DataAnnotations;

namespace KupacMicroservice.Model.Liciter
{

    /// <summary>
    /// dto za izmenu licitera
    /// </summary>
    public class UpdateLiciterDto
    {

        /// <summary>
        /// id liciter
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id licitera.")]
        public Guid LiciterId { get; set; }

        /// <summary>
        /// ime liciter
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ime licitera.")]
        public string ImeLiciter { get; set; }

        /// <summary>
        /// prezime liciter
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti prezime licitera.")]
        public string PrezimeLiciter { get; set; }

        /// <summary>
        /// jmbg liciter
        /// </summary>
        public string JmbgLiciter { get; set; }


        /// <summary>
        /// broj pasosa
        /// </summary>
        public string Brojpasosa { get; set; }

        /// <summary>
        /// drzava stranac
        /// </summary>
        public string Drzavastranac { get; set; }


        /// <summary>
        /// adresa liciter
        /// </summary>
        public string AdresaLiciter { get; set; }




    }
}
