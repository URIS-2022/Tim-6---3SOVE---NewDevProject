namespace ms_parcela.Models.ParcelaModel
{
    public enum KulturaUpd { njive, vrtovi, vocnjaci, vinogradi, livade, pasnjaci, sume, trstici }
    public enum ObradivostUpd { obradivo, ostalo }
    public enum KlasaUpd { I, II, III, IV, V, VI, VII, VIII }
    public enum OblikSvojineUpd { privatna_svojina, drzavna_svojina_RS, drzavna_svojina, drustvena_svojina, zaduzena_svojina, mesovita_svojina, drugi_oblici }
    public enum OdvodnjavanjeUpd { linijsko_odvodnjavanje, cevni_sistem }
    public class ParcelaUpdateDto
    {
        public int brojParcele { get; set; }
        public Guid brojKatastraskaOpstina { get; set; }
        public int povrsina { get; set; }
        public KulturaUpd kultura { get; set; }
        public ObradivostUpd obradivost { get; set; }
        public KlasaUpd klasa { get; set; }
        public OblikSvojineUpd oblikSvojine { get; set; }
        public OdvodnjavanjeUpd odvodnjavanje { get; set; }

        //[ForeignKey("Zonas")]
        //public Guid zonaID { get; set; }
        //public IEnumerable<DeoParcele> DeoParcela { get; set; }
    }
}
