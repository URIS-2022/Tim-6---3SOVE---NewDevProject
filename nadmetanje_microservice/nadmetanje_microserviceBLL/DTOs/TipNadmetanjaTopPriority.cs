using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.DTOs
{
    public static class TipNadmetanjaTopPriority
    {
        public static TipNadmetanja? TipNadmetanjaTop { get; set; } = null;
        public static double? CenaPoHektaru { get; set; } = null;
        public static int? DuzinaZakupa { get; set; } = null;
    }
}
