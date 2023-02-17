using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LicitacijaService.Models.Licitacija
{
    /// <summary>
    /// Update dto za licitacija
    /// </summary>
    public class LicitacijaUpdateDto
    {
        /// <summary>
        /// Broj licitacije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj licitacije")]
        public int BrojLicitacije { get; set; }

        /// <summary>
        /// Godina licitacije
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti godinu licitacije")]
        public int GodinaLicitacije { get; set; }

        /// <summary>
        /// Ogranicenje licitacije
        /// </summary>
        public int OgranicenjeLicitacije { get; set; }

        /// <summary>
        /// Rok licitacije
        /// </summary>
        public DateTime RokLicitacije { get; set; }

        /// <summary>
        /// Korak cene licitacije
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti korak cene licitacije")]
        public int KorakCeneLicitacije { get; set; }

        /// <summary>
        /// Id programa
        /// </summary>

        [Required(ErrorMessage = "Obavezno je uneti id programa")]
        public Guid ProgramEntitetProgramId { get; set; }

    }
}
