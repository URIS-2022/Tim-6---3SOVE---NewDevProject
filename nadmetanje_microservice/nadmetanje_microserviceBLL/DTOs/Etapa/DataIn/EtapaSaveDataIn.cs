using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.DTOs.Etapa.DataIn
{
    public class EtapaSaveDataIn
    {
        public Guid? Id { get; set; }
        public Guid LicitacijaId { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan VremePocetka { get; set; }
        public TimeSpan VremeZavrsetka { get; set; }
    }
}
