using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica_Api.Models
{
    public class Especialidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idEspecialidad {  get; set; }
        public string nombre {  get; set; }
        public string descripcion { get; set; }

        public ICollection<Doctor> Doctores { get; set; }


    }
}
