using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models.PredlogPlanaProjekta
{
    /// <summary>
    /// model za kreiranje predloga plana projekta
    /// </summary>
    public class CreatePredlogPlanaProjektaDto
    {
        ///<summary>
        /// id dokumenta
        /// </summary>
        public Guid PredlogId { get; set; }

        ///<summary>
        /// id dokumenta
        /// </summary>
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
