using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;



namespace LicitacijaService.Models.ProgramEntitet
{
    /// <summary>
    /// Update Dto za program
    /// </summary>
    public class ProgramEntitetUpdateDto
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
