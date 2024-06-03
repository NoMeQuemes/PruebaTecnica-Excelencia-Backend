using System.ComponentModel.DataAnnotations;


namespace Prueba_Tecnica_Api.Models.Dto.Citas
{
    public class CitasCreateDto
    {
        [Required]
        public int idPaciente { get; set; }
        [Required]
        public int idDoctor { get; set; }
        [Required]
        public DateTime fechaCita { get; set; }
        public string? motivo { get; set; }
        //public int idEstado { get; set; } 
    }
}
