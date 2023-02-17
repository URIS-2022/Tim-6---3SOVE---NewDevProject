using System;


namespace DokumentMicroservice.Models.PredlogPlanaProjekta
{
    /// <summary>
    /// dto za potvrdu predloga plana projekta
    /// </summary>
    public class ConfirmationPredlogPlanaProjektaDto
    {

        ///<summary> 
        /// zavodni broj
        /// </summary>
        public string ZavodniBroj { get; set; }

        ///<summary>
        /// Datum predlog projekta
        /// </summary>
        public DateTime DatumPredlog { get; set; } = DateTime.Now;

    }
}
