using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prueba_Tecnica_Api.Models
{
    public class Genero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idGenero { get; set; }
        public string nombre { get; set; }

        [JsonIgnore]
        public ICollection<Paciente> Pacientes { get; set; }

    }
}
