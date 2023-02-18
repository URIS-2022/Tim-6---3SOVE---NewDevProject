using System.ComponentModel.DataAnnotations;
namespace ms_parcela.Models.KatastarskaOpstinaModel
{
    public enum opstineAdd
    {
        Cantavir = 1, Backi_Vinogradi = 2, Bikovo = 3, Djudji = 4, Zednik = 5, Tavankut = 6, Bajmok = 7, Donji_Grad = 8, Stari_Grad = 9, Novi_Grad = 10, Palic = 11
    }
    public class KatastarskaOpstinaCreationDto
    {
        [Required(ErrorMessage = "Unesite naziv katastarske opstine")]
        public opstineAdd nazivKatastarskeOpstine { set; get; }
    }
}
