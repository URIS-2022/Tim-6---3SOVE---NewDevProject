using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.DTOs.Etapa
{
    public class DictionaryItem<T>
    {
        public int Key { get; set; }
        public T Value { get; set; }
        public DictionaryItem() { }
    }
}
