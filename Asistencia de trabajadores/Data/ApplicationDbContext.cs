using Asistencia_de_trabajadores.Models;
using Microsoft.EntityFrameworkCore;

namespace Asistencia_de_trabajadores.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Rol> Rol { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Asistencia> Asistencias { get; set; } = null!;

    }
}
