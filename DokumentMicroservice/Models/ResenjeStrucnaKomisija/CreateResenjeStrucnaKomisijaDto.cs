using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models.ResenjeStrucnaKomisija
{
    /// <summary>
    /// model za kreiranje resenja strucne komisije
    /// </summary>
    public class CreateResenjeStrucnaKomisijaDto
    {
        ///<summary>
        /// id dokumenta
        /// </summary>
        public Guid DokumentId { get; set; }

        ///<summary>
        /// zavodni broj
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti zavodni broj resenja strucne komisije")]
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
