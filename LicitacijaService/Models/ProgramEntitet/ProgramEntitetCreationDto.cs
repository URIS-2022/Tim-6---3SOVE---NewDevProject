using System.ComponentModel.DataAnnotations;

namespace LicitacijaService.Models.ProgramEntitet
{
    /// <summary>
    /// Creation Dto za program
    /// </summary>
    public class ProgramEntitetCreationDto
    {
        /// <summary>
        /// Maksimalna povrsina
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti maksimalnu povrsinu")]
        public int MaksimalnaPovrsina { get; set; }

        /// <summary>
        /// Krug licitacije
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti krug licitacije")]
        public int KrugLicitacije { get; set; }
    }
}
