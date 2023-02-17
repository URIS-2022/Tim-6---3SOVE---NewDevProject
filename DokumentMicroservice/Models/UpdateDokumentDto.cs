using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models
{

    /// <summary>
    /// model za azuriranje dokumenta
    /// </summary>
    public class UpdateDokumentDto
    {
        /// <summary>
        /// id dokumenta
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id dokumenta.")]
        public Guid DokumentId { get; set; }

        /// <summary>
        /// sablon dokumenta
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti sablon dokumenta.")]
        public string Sablon { get; set; }

        /// <summary>
        /// datum donosenja dokumenta
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti datum donosenja dokumenta.")]
        [DataType(DataType.Date)]
        public DateTime DatumDonosenjaDokumenta { get; set; }


    }
}
