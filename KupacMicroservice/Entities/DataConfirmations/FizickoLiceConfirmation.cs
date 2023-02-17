using System;

namespace KupacMicroservice.Entities.DataConfirmations
{
    public class FizickoLiceConfirmation
    {


        /// <summary>
        /// id fizicko lice
        /// </summary>
        public Guid FizickoliceId { get; set; }

        /// <summary>
        /// id kupac
        /// </summary>
        public Guid KupacId { get; set; }

        /// <summary>
        /// ime fizicko lice
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// prezime fizicko lice
        /// </summary>
        public string Prezime { get; set; }

    }
}
