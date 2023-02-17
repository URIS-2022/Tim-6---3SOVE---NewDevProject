using System;



namespace KupacMicroservice.model.FizickoLice
{
    /// <summary>
    /// dto za potvrdu fizickog lica
    /// </summary>
    public class ConfirmationFizickoLiceDto
    {

       /// <summary>
       /// Ime fizickog lica
       /// </summary>
       public string Ime { get; set; }

       /// <summary>
       /// Prezime fizickog lica
       /// </summary>
       public string Prezime { get; set; }

    }
}
