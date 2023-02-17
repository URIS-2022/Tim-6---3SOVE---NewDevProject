using System;


namespace DokumentMicroservice.Models.ResenjeStrucnaKomisija
{
    /// <summary>
    /// dto za potvrdu resenja strucne komisije
    /// </summary>
    public class ConfirmationResenjeStrucnaKomisijaDto
    {

        ///<summary>
        /// zavodni broj
        /// </summary>

        public string Zavodnibr { get; set; }

        ///<summary>
        /// datum resenje
        /// </summary>

        public DateTime DatumResenje { get; set; } = DateTime.Now;

    }
}
