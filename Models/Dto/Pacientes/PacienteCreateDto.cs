using System.ComponentModel.DataAnnotations;


namespace Prueba_Tecnica_Api.Models.Dto.Pacientes
{
    public class PacienteCreateDto
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public DateTime fechaNacimiento { get; set; }
        [Required]
        public int idGenero { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string numCelular { get; set; }

    }
}
