using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceDLL.Model
{
    public class Entity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
