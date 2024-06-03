using System.ComponentModel.DataAnnotations;


namespace Prueba_Tecnica_Api.Models.Dto.Citas
{
    public class CitasUpdateDto
    {
        [Required]
        public int idCita { get; set; }
        [Required]
        public int idPaciente { get; set; }
        [Required]
        public int idDoctor { get; set; }
        [Required]
        public DateTime fechaCita { get; set; }
        public string motivo { get; set; }
        [Required]
        public int idEstado { get; set; } // Programada - Cancelada - Completada
    }
}
