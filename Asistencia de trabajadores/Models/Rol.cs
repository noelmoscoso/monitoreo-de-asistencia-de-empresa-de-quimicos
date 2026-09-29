using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Asistencia_de_trabajadores.Models
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        public int id_rol { get; set; }

        public string nombre_rol { get; set; } = string.Empty;

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    }
}
