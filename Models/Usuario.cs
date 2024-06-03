using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica_Api.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUsuario {  get; set; }
        public string nombreUsuario { get; set; }
        public string password {  get; set; }

        public int? idRoles { get; set; }
        [ForeignKey("idRoles")]
        public virtual Roles Roles { get; set; }

        public DateTime? estadoUsuario { get; set; }

    }
}
