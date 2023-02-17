using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models.PredlogPlanaProjekta
{
    /// <summary>
    /// model za azuriranje predloga plana projekta
    /// </summary>
    public class UpdatePredlogPlanaProjektaDto
    {
        /// <summary>
        /// id predloga
        /// </summary>
        [Required(ErrorMessage = "Obavezno je id predloga plana projekta")]
        public Guid PredlogId { get; set; }

        ///<summary>
        /// id dokumenta
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id dokumenta")]
        public Guid DokumentId { get; set; }

        ///<summary> 
        /// zavodni broj
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti zavodni broj predloga plana projekta")]
        public string ZavodniBr { get; set; }

        ///<summary>
        /// Datum predlog projekta
        /// </summary>
        public DateTime DatumPredlog { get; set; } = DateTime.Now;
    }
}
