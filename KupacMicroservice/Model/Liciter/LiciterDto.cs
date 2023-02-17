using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace KupacMicroservice.Model.Liciter
{

    /// <summary>
    /// dto za licitera
    /// </summary>
    public class LiciterDto
    {

        /// <summary>
        /// id liciter
        /// </summary>
        public Guid LiciterId { get; set; }

        /// <summary>
        /// ime licitera
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ime licitera.")]
        public string ImeLiciter { get; set; }

        /// <summary>
        /// prezime lciitera
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

        /// <summary>
        /// kupci
        /// </summary>
        [JsonIgnore]
        public List<Guid> Kupci { get; set; }



    }
}
