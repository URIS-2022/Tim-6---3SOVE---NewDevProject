using System;


namespace KupacMicroservice.Model.Liciter
{
    /// <summary>
    /// dto za potvrdu licitera
    /// </summary>
    public class ConfirmationLiciterDto
    {
        /// <summary>
        /// ime licitera
        /// </summary>
        public string ImeLiciter { get; set; }


        /// <summary>
        /// prezime liciter
        /// </summary>
        public string PrezimeLiciter { get; set; }

    }
}
