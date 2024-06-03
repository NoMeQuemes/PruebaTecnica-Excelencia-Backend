using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Prueba_Tecnica_Api.Models;

namespace Prueba_Tecnica_Api.Models.Dto.Doctores
{
    public class DoctorCreateDto
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        public string? email { get; set; }
        public string numCelular { get; set; }
        [Required]
        public int idEspecialidad { get; set; }




    }
}
