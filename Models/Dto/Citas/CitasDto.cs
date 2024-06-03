using System.ComponentModel.DataAnnotations;


namespace Prueba_Tecnica_Api.Models.Dto.Citas
{
    public class CitasDto
    {
        public int idCita { get; set; }
        public int idPaciente { get; set; }
        public int idDoctor { get; set; }
        public DateTime fechaCita { get; set; }
        public string? motivo { get; set; }
        public int idEstado { get; set; } // Programada - Cancelada - Completada
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public DateTime? fechaEliminacion { get; set; }
    }
}
