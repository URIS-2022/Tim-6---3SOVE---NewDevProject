using System;


namespace KupacMicroservice.model.Kupac
{
    /// <summary>
    ///  dto za potvrdu kupca
    /// </summary>
    public class ConfirmationKupacDto
    {

        /// <summary>
        /// adresa kupac
        /// </summary>
        public string AdresaKupac { get; set; }

        /// <summary>
        /// br telefon1
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// email kupac
        /// </summary>
        public string Email { get; set; }




    }
}
