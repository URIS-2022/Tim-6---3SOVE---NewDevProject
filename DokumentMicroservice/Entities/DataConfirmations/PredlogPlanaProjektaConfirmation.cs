using System;



namespace DokumentMicroservice.Entities.DataConfirmations
{

    public class PredlogPlanaProjektaConfirmation
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

    }

}
