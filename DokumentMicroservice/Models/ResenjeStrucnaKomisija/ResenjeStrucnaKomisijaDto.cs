using System;
using System.Globalization;

namespace DokumentMicroservice.Models.ResenjeStrucnaKomisija
{
    public class ResenjeStrucnaKomisijaDto
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

        ///<summary>
        /// datum resenje
        /// </summary>
        public DateTime DatumResenje { get; set; } = DateTime.Now;

        ///<summary>
        /// ime clana komisije
        /// </summary>
        public string ImeClanaKomisije { get; set; }

        ///<summary>
        /// prezime clana komisije
        /// </summary>
        public string PrezClanaKomisije { get; set; }

        ///<summary>
        /// predsednik komisije
        /// </summary>
        public string PredsednikKomisije { get; set; }
        
    }
}
