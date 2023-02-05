using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceDLL.Model
{
    public class Etapa : Entity
    {
        public Guid LicitacijaId { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan VremePocetka { get; set; }
        public TimeSpan VremeZavrsetka { get; set; }
    }
}
