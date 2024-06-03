using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica_Api.Models
{
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPaciente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }

        public int idGenero { get; set; }
        [ForeignKey("idGenero")]
        public virtual Genero Generos { get; set; }

        public string? email { get; set; }
        public string numCelular { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public DateTime? fechaEliminacion { get; set; }

        public ICollection<Cita> Citas { get; set; }

    }
}
