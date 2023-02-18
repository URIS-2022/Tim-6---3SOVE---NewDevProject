namespace PrijavaJnService.ServiceCalls.Mocks
{
    public class KupacDto
    {
        /*
        /// <summary>
        /// Id kupca
        /// </summary>
        public Guid KupacId { get; set; }
        /// <summary>
        /// Naziv kupca
        /// </summary>
        public string Naziv { get; set; }
        /// <summary>
        /// Broj telefona kupca
        /// </summary>
        public string BrojTelefona { get; set; }
        /// <summary>
        /// Emali kupca
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Broj računa kupca
        /// </summary>
        public string BrojRacuna { get; set; } */
        public Guid KupacId { get; set; }

        public string AdresaKupac { get; set; }

        public double OstvarenaPovrsina { get; set; }

        public bool ImaZabranu { get; set; }

        public int DuzinaTrajanjaZabraneGod { get; set; }

        public string BrojTelefona1 { get; set; }

        public string BrojTelefona2 { get; set; }

        public string Email { get; set; }

        public string BrojRacuna { get; set; }

        public string IznosUplata { get; set; }

        public string Prioritet { get; set; }
    }
}
