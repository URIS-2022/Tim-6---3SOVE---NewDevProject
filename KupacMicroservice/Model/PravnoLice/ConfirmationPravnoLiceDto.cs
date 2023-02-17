using System;


namespace KupacMicroservice.model.PravnoLice
{

    /// <summary>
    /// dto za potvrdu pravnog lica
    /// </summary>
    public class ConfirmationPravnoLiceDto
    {

        /// <summary>
        /// naziv pravnog lica
        /// </summary>
        public string Naziv { get; set; }


        /// <summary>
        /// maticni broj
        /// </summary>
        public string MaticniBroj { get; set; }

    }
}
