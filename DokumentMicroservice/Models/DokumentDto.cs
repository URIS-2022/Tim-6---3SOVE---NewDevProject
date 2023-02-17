using DokumentMicroservice.Models.Oglas;
using DokumentMicroservice.Models.PredlogPlanaProjekta;
using DokumentMicroservice.Models.ResenjeStrucnaKomisija;
using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models
{
    /// <summary>
    /// dto za dokument
    /// </summary>
    public class DokumentDto
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

        ///<summary>
        /// oglas
        /// </summary>
        public OglasDto Oglas {get; set;}

        ///<summary>
        /// predlog plana projekta
        /// </summary>
        public PredlogPlanaProjektaDto PredlogPlanaProjekta { get; set; }

        ///<summary>
        /// resenje strucne komisije
        ///</summary>
        public ResenjeStrucnaKomisijaDto ResenjeStrucneKomisije { get; set; }


    }
}
