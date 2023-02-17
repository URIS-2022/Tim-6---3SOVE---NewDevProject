using DokumentMicroservice.Models.Oglas;
using DokumentMicroservice.Models.PredlogPlanaProjekta;
using DokumentMicroservice.Models.ResenjeStrucnaKomisija;
using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Entities.DataConfirmations
{
    public class DokumentConfirmation
    {
        /// <summary>
        /// Id dokumenta
        /// </summary>
        public Guid DokumentId { get; set; }

        ///<summary>
        /// sablon dokumenta
        /// </summary>
        public string Sablon { get; set; }

        ///<summary>
        /// datum donosenja dokumenta
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime DatumDonosenjaDokumenta { get; set; }
    }
}
