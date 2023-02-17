using Microsoft.EntityFrameworkCore;
using System;


namespace KupacMicroservice.Entities.DataConfirmations
{
    public class KupacConfirmation
    {


        public Guid KupacId { get; set; } 

        public string AdresaKupac { get; set; }

        public string BrojTelefona1 { get; set; }

        public string Email { get; set; }

    }
}
