using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models.Oglas
{
    /// <summary>
    /// dto za potvrdu oglasa
    /// </summary>
    
    public class ConfirmationOglasDto
    {


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
