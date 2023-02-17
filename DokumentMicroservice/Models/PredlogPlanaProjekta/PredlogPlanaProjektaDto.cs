using System;
using System.Globalization;

namespace DokumentMicroservice.Models.PredlogPlanaProjekta
{

    /// <summary>
    /// model predlog plana projekta
    /// </summary>
    public class PredlogPlanaProjektaDto
    {
        ///<summary> 
        /// id predloga plana
        /// </summary>    
        public Guid PredlogId { get; set; }

        ///<summary>
        /// id dokumenta
        /// </summary>   
        public Guid DokumentId { get; set; }

        ///<summary> 
        /// zavodni broj
        /// </summary>
        public string ZavodniBr { get; set; }

        ///<summary>
        /// Datum predlog projekta
        /// </summary>
        public DateTime DatumPredlog { get; set; } = DateTime.Now;


    }
}
