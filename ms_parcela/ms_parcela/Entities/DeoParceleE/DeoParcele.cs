using ms_parcela.Models.ParcelaModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_parcela.Entities.DeoParceleE
{
    public class DeoParcele
    {
        [Key]
        public Guid deoParceleID { get; set; }
        public int redniBrojDelaParcele { get; set; }
        public int povrsinaDelaParcele { get; set; }
        [ForeignKey("Parcelas")]
        public Guid brojKatastraskaOpstina { get; set; }
        [ForeignKey("Parcelas")]
        public int brojParcele { get; set; }
        public Guid? NadmetanjeId { get; set; }



    }
}
