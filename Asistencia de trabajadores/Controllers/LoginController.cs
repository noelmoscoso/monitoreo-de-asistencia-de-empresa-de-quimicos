using Asistencia_de_trabajadores.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Asistencia_de_trabajadores.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string correo, string contraseña)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo && u.Contraseña == contraseña);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }

            HttpContext.Session.SetInt32("IdUsuario", usuario.id_usuario);
            HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
            HttpContext.Session.SetInt32("IdRol", usuario.Id_rol);

            if (usuario.Id_rol == 1)
            {
                return RedirectToAction("Index", "Admin");
            }

            if (usuario.Id_rol == 2)
            {
                return RedirectToAction("Index", "Trabajador");
            }

            ViewBag.Error = "El usuario no tiene un rol válido.";
            return View();
        }

        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Login");
        }
    }
}
