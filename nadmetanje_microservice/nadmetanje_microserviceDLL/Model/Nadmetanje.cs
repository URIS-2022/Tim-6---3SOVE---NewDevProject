using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceDLL.Model
{
    public class Nadmetanje : Entity
    {
        public TipNadmetanja Tip { get; set; }
        public StatusNadmetanja Status { get; set; }
        public double CenaPoHektaru { get; set; }
        public int DuzinaZakupa { get; set; }
        public string RedniBroj { get; set; }
        public Etapa Etapa { get; set; }
        public Guid? EtapaId { get; set; }
        public KrugNadmetanja KrugNadmetanja { get; set; }
        public StatusDrugiKrug? StatusDrugiKrug { get; set; }
        public Guid? KupacId { get; set; }

    }

    public enum TipNadmetanja
    {
        JavnoNadmetanje = 1,
        OtvaranjeZatvorenihPonuda = 2,
    }

    public enum StatusNadmetanja
    {
        Aktivno = 1,
        ZavrsenoUpsesno = 2,
        ZavrsenoNeuspesno = 3,
        ZavrsenoIzuzeto = 4
    }

    public enum KrugNadmetanja
    {
        Prvi = 1,
        Drugi = 2,
    }

    public enum StatusDrugiKrug
    {
        NoviUsloviNedefinisani = 1,
        NoviUslovi = 2,
        BezNovihUslova = 3,
    }
}
