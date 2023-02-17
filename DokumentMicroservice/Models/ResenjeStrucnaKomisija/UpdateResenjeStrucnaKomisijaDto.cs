using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models.ResenjeStrucnaKomisija
{
    /// <summary>
    /// model za azuriranje resenja strucne komisije
    /// </summary>
    public class UpdateResenjeStrucnaKomisijaDto
    {

        /// <summary>
        /// id resenja strucne komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id resenja")]
        public Guid ResenjeId { get; set; }

        ///<summary>
        /// id dokumenta
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id dokumenta")]
        public Guid DokumentId { get; set; }

        ///<summary>
        /// zavodni broj
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti zavodni broj")]
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
