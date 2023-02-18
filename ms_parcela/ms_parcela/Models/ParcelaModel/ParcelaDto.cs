using System.ComponentModel.DataAnnotations.Schema;

namespace ms_parcela.Models.ParcelaModel
{
    public enum KulturaDto { njive, vrtovi, vocnjaci, vinogradi, livade, pasnjaci, sume, trstici }
    public enum ObradivostDto { obradivo, ostalo }
    public enum KlasaDto { I, II, III, IV, V, VI, VII, VIII }
    public enum OblikSvojineDto { privatna_svojina, drzavna_svojina_RS, drzavna_svojina, drustvena_svojina, zaduzena_svojina, mesovita_svojina, drugi_oblici }
    public enum OdvodnjavanjeDto { linijsko_odvodnjavanje, cevni_sistem }
    public class ParcelaDto
    {
        public int brojParcele { get; set; }
        public Guid brojKatastraskaOpstina { get; set; }
        public int povrsina { get; set; }
        public KulturaDto kultura { get; set; }
        public ObradivostDto obradivost { get; set; }
        public KlasaDto klasa { get; set; }
        public OblikSvojineDto oblikSvojine { get; set; }
        public OdvodnjavanjeDto odvodnjavanje { get; set; }

        //[ForeignKey("Zonas")]
        //public Guid zonaID { get; set; }
        //public IEnumerable<DeoParcele> DeoParcela { get; set; }
    }
}
