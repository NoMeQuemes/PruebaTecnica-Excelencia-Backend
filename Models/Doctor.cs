using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica_Api.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDoctor { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string? email { get; set; }
        public string numCelular { get; set; }

        public int idEspecialidad { get; set; }
        [ForeignKey("idEspecialidad")]
        public virtual Especialidad Especialidades { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public DateTime? fechaEliminacion { get; set; }

        public ICollection<Cita> Citas { get; set; }


    }
}
