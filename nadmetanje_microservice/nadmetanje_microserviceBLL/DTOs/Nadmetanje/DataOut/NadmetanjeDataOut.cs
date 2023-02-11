using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataOut
{
    public class NadmetanjeDataOut
    {
        public Guid Id { get; set; }
        public int Tip { get; set; }
        public int Status { get; set; }
        public double CenaPoHektaru { get; set; }
        public int DuzinaZakupa { get; set; }
        public string RedniBroj { get; set; }
        public Guid? EtapaId { get; set; }
        public int KrugNadmetanja { get; set; }
        public int? StatusDrugiKrug { get; set; }
}
}
