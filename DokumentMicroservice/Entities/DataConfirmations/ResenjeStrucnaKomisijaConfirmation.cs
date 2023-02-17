using System;

namespace DokumentMicroservice.Entities.DataConfirmations
{
    public class ResenjeStrucnaKomisijaConfirmation
    {
        /// <summary>
        /// id resenja strucne komisije
        /// </summary>
        public Guid ResenjeId { get; set; }

        ///<summary>
        /// id dokumenta
        /// </summary>
        public Guid DokumentId { get; set; }

        ///<summary>
        /// zavodni broj
        /// </summary>

        public string Zavodnibr { get; set; }

        
    }
}
