using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asistencia_de_trabajadores.Data;
using Asistencia_de_trabajadores.Models;


namespace Asistencia_de_trabajadores.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trabajadores = await _context.Usuarios.Where(u => u.Id_rol == 2 && u.Activo == true).ToListAsync();
        
            return View(trabajadores);

        }

        [HttpPost]
        public async Task<IActionResult> Crear(string nombre, string apellido, string correo, string rut, string contraseña)
        {
            var trabajador = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                rut = rut,
                Contraseña = contraseña,
                Id_rol = 2,
                Activo = true
            };

            _context.Usuarios.Add(trabajador);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id_usuario, string nombre, string apellido, string correo, string rut, string contraseña)
        {
            var trabajador = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.id_usuario == id_usuario && u.Id_rol == 2);

            if (trabajador == null) return NotFound();

            trabajador.Nombre = nombre;
            trabajador.Apellido = apellido;
            trabajador.Correo = correo;
            trabajador.rut = rut;

            if (!string.IsNullOrWhiteSpace(contraseña))
            {
                trabajador.Contraseña = contraseña;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Eliminar(int id_usuario)
        {
            var usuario = await _context.Usuarios.FindAsync(id_usuario);
            if (usuario != null)
            {
                usuario.Activo = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
