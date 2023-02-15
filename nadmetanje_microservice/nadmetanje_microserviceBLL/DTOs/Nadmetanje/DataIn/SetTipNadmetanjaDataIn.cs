using nadmetanje_microserviceDLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.DTOs.Nadmetanje.DataIn
{
    public class SetTipNadmetanjaDataIn<T>
    {
        public T Enumeracija { get; set; }
        public Guid NadmetanjeId { get; set; }
        public Guid? KupacId { get; set; }
    }
}
