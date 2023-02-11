using nadmetanje_microserviceBLL.DTOs.Etapa.DataIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn
{
    public class CreateEtapaAndConnectToNadmetanjaDataIn
    {
        public EtapaSaveDataIn EtapaInfos { get; set; }
        public List<Guid> NadmetanjaIds { get; set; }
    }
}
