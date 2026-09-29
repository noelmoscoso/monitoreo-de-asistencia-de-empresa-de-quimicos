using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asistencia_de_trabajadores.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string rut { get; set; } = string.Empty;

        [Column("contraseña")]
        public string Contraseña { get; set; } = string.Empty;

        [Column("id_rol")]
        public int Id_rol { get; set; }

        [ForeignKey(nameof(Id_rol))]
        public Rol? Rol { get; set; }

        public bool Activo { get; set; } = true;
    }
}
