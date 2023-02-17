using System;
using System.ComponentModel.DataAnnotations;

namespace DokumentMicroservice.Models.OtherService
{
    public class Komisija
    {
        
            [Key]
            public Guid IDKomsije { get; set; }
            public string ImeKomisije { get; set; } = string.Empty;
            public string Ovlascenje { get; set; } = string.Empty;
            public string OznakaKomisije { get; set; } = string.Empty;
        


    }
}
