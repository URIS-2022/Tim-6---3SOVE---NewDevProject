using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupacMicroservice.Entities
{

    [Table("OvlascenoLice")]
    public class OvlascenoLice
    {

        public Guid LiciterId {get;set;}

        public Guid KupacId { get; set; }

        public Kupac Kupac { get; set; }

        public Liciter Liciter{ get; set; }

    }
}
