using System;

namespace KupacMicroservice.model.FizickoLice
{

    /// <summary>
    /// dto za fizicko lice
    /// </summary>
    public class FizickoLiceDto
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

        /// <summary>
        /// jmbg fizicko lice
        /// </summary>
        public string JMBG { get; set; }

        /// <summary>
        /// adresa fizicko lice
        /// </summary>
        public string AdresaFizickoLice { get; set; }

    }
}
