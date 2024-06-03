using System.ComponentModel.DataAnnotations;


namespace Prueba_Tecnica_Api.Models.Dto.Doctores
{
    public class DoctorUpdateDto
    {
        [Required]
        public int idDoctor { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string numCelular { get; set; }
        [Required]
        public int idEspecialidad { get; set; }



    }
}
