using System;

namespace KupacMicroservice.Entities.DataConfirmations
{
    public class PravnoLiceConfirmation
    {

        /// <summary>
        /// id fizicko lice
        /// </summary>
        public Guid PravnoliceId { get; set; }

        /// <summary>
        /// id kupac
        /// </summary>
        public Guid KupacId { get; set; }

        public string Naziv { get; set; }

        public string MaticniBroj { get; set; }
    }
}
