using System;


namespace DokumentMicroservice.Services.Mock
{
    public class Komisija
    {

        /// <summary>
        /// Id komisija
        /// </summary>
        public Guid IDKomsije { get; set; }

        /// <summary>
        /// Ime komisije
        /// </summary>
        public string ImeKomisije { get; set; }

        /// <summary>
        /// ovlascenje
        /// </summary>
        public string Ovlascenje { get; set; }

        /// <summary>
        /// Oznaka komisije
        /// </summary>
        public string OznakaKomisije { get; set; } 

    }
}
