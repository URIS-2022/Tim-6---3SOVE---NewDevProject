namespace LicitacijaService.Models.ProgramEntitet
{
    /// <summary>
    /// Dto za program
    /// </summary>
    public class ProgramEntitetDto
    {
        /// <summary>
        /// Id programa
        /// </summary>
        public Guid ProgramId { get; set; }

        /// <summary>
        /// Maksimalna povrsina
        /// </summary>
        public int MaksimalnaPovrsina { get; set; }

        /// <summary>
        /// Krug licitacije
        /// </summary>
        public int KrugLicitacije { get; set; }
    }
}
