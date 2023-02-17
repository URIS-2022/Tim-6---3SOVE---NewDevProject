using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models
{
    /// <summary>
    ///  dto za potvrdu kreiranja dokumenta
    /// </summary>
    public class ConfirmationDokumentDto
    {
        
        public string Sablon { get; set; }

        public DateTime DatumDonosenjaDokumenta { get; set; }


    }
}
