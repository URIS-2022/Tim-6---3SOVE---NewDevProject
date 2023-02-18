namespace ms_parcela.Models.ParcelaModel
{
    public enum KulturaAdd { njive, vrtovi, vocnjaci, vinogradi, livade, pasnjaci, sume, trstici }
    public enum ObradivostAdd { obradivo, ostalo }
    public enum KlasaAdd { I, II, III, IV, V, VI, VII, VIII }
    public enum OblikSvojineAdd { privatna_svojina, drzavna_svojina_RS, drzavna_svojina, drustvena_svojina, zaduzena_svojina, mesovita_svojina, drugi_oblici }
    public enum OdvodnjavanjaAdd { linijsko_odvodnjavanje, cevni_sistem }
    public class ParcelaCreationDto
    {
        public int povrsina { get; set; }
        public KulturaAdd kultura { get; set; }
        public ObradivostAdd obradivost { get; set; }
        public KlasaAdd klasa { get; set; }
        public OblikSvojineAdd oblikSvojine { get; set; }
        public OdvodnjavanjaAdd odvodnjavanje { get; set; }
    }
}
