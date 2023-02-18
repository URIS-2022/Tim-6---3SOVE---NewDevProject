using ms_parcela.Models.DeoParceleModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_parcela.Entities.ParcelaE
{
    public enum Kultura { njive, vrtovi, vocnjaci, vinogradi, livade, pasnjaci, sume, trstici }
    public enum Obradivost { obradivo, ostalo }
    public enum Klasa { I, II, III, IV, V, VI, VII, VIII }
    public enum OblikSvojine { privatna_svojina, drzavna_svojina_RS, drzavna_svojina, drustvena_svojina, zaduzena_svojina, mesovita_svojina, drugi_oblici }
    public enum Odvodnjavanje { linijsko_odvodnjavanje, cevni_sistem }
    public class Parcela
    {   /// <summary>
        /// id za parcelu je broj parcele + broj ko
        /// </summary>
        public int brojParcele { get; set; }

        [ForeignKey("KatastarskaOpstinas")]
        public Guid brojKatastraskaOpstina { get; set; }
        public int povrsina { get; set; }
        public Kultura kultura { get; set; }
        public Obradivost obradivost { get; set; }
        public Klasa klasa { get; set; }
        public OblikSvojine oblikSvojine { get; set; }
        public Odvodnjavanje odvodnjavanje { get; set; }

    }
}
