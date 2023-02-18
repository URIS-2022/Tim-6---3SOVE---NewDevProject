
using ms_parcela.Models.ExternalServices;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_parcela.Models.DeoParceleModel
{
    public class DeoParceleDto
    {
        public Guid deoParceleID { get; set; }

        public int redniBrojDelaParcele { get; set; }

        public int povrsinaDelaParcele { get; set; }
        
        public Guid brojKatastraskaOpstina { get; set; }
        
        public int brojParcele { get; set; }
        public NadmetanjeDto? Nadmetanje { get; set; }
    }
}
