using System;


namespace KupacMicroservice.Entities.DataConfirmations
{
    public class LiciterConfirmation
    {

        public Guid LiciterId { get; set; } 

        public string ImeLiciter { get; set; }

        public string PrezimeLiciter { get; set; }

    }
}
