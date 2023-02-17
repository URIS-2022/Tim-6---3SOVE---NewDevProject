using System;


namespace DokumentMicroservice.Entities.DataConfirmations
{
    public class OglasConfirmation
    {

        ///<summary>
        /// id oglasa
        /// </summary>
        public Guid OglasId { get; set; }

        ///<summary>
        /// id dokumenta
        /// </summary>
        public Guid DokumentId { get; set; } 

        ///<summary>
        /// broj nadmetanja za fizicka lica
        /// </summary>
        public string BrNadmetanjaJmbg { get; set; }

        ///<summary>
        /// broj nadmetanja za pravna lica
        /// </summary>
        public string BrNadmetanjaMaticniBrojPreduzeca { get; set; }
    }
}
