using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DokumentMicroservice.Models.Oglas
{
    /// <summary>
    /// model Oglasa
    /// </summary>
    public class OglasDto
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

        public string Zalba { get; set; }

    }
}
