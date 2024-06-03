using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Api.Models.Dto.Doctores
{
    public class DoctoresDto
    {
        public int idDoctor { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string? email { get; set; }
        public string numCelular { get; set; }
        public int idEspecialidad { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public DateTime? fechaEliminacion { get; set; }

    }
}
