using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicitacijaService.Entities
{
    /// <summary>
    /// Predstavlja program
    /// </summary>
    public class ProgramEntitet
    {
        /// <summary>
        /// ID programa
        /// </summary>
        [Key]
        public Guid ProgramId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Maksimalna povrsina
        /// </summary>

        [Required]
        public int MaksimalnaPovrsina { get; set; }

        /// <summary>
        /// Krug licitacije
        /// </summary>

        [Required]
        public int KrugLicitacije { get; set; }

        public ICollection<Licitacija> Licitacije { get; set; }
    }
}
