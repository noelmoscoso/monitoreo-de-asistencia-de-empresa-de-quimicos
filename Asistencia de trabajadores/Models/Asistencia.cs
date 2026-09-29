using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asistencia_de_trabajadores.Models
{
    [Table("Asistencia")]
    public class Asistencia
    {
        [Key]
        public int id_asistencia { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora_entrada { get; set; }
        public TimeSpan? hora_salida { get; set; }

        [ForeignKey("id_usuario")]
        public Usuario? Usuario { get; set; }

    }
}
