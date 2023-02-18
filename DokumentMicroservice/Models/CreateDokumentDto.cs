using DokumentMicroservice.Services.Mock;
using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models
{
    /// <summary>
    /// model za kreiranje dokumenta
    /// </summary>
    public class CreateDokumentDto
    {

        /// <summary>
        /// id dokumenta
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id dokumenta.")]
        public Guid DokumentId { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti sablon oglasa.")]
        public string Sablon { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti datum donosenja oglasa.")]
        public DateTime DatumDonosenjaDokumenta { get; set; }

        



    }
}
