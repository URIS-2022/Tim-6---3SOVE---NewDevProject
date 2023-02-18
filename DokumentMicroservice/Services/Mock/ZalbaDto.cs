namespace DokumentMicroservice.Services.Mock
{

    public enum StatusZalbeDto { usvojena, odbijena, otvorena }
    public enum RadnjaNaOsnovuZalbeDto { JN_ide_u_drugi_krug_sa_novim_uslovima, JN_ide_u_drugi_krug_sa_starim_uslovima, JN_ne_ide_u_drugi_krug }

    public class ZalbaDto
    {



        /// <summary>
        /// Id kupca
        /// </summary>
        public Guid zalbaID { get; set; }
        /// <summary>
        /// Naziv zalbe
        /// </summary>
        public string Naziv { get; set; }
        /// <summary>
        /// obrazlozenje
        /// </summary>
        public string Obrazlozenje { get; set; }


    }
}
