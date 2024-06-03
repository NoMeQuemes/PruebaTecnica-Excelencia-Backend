using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica_Api.Models
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCita { get; set; }


        public int idPaciente { get; set; }
        [ForeignKey("idPaciente")]
        public virtual Paciente Pacientes { get; set; }

        public int idDoctor { get; set; }
        [ForeignKey("idDoctor")]
        public virtual Doctor Doctores { get; set; }

        public DateTime fechaCita { get; set; }
        public string? motivo { get; set;  }

        public int idEstado { get; set; } // Programada - Cancelada - Completada
        [ForeignKey("idEstado")]
        public virtual Estado Estados { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public DateTime? fechaEliminacion { get; set; }

    }
}
