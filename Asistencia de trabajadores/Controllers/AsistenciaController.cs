using Asistencia_de_trabajadores.Data;
using Asistencia_de_trabajadores.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Asistencia_de_trabajadores.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsistenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarEntrada()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
                return RedirectToAction("Index", "Login");

            var hoy = DateTime.Today;

            var existe = await _context.Asistencias
                .AnyAsync(a =>
                    a.id_usuario == idUsuario.Value &&
                    a.fecha == hoy);

            if (existe)
            {
                TempData["Error"] = "Ya registraste tu entrada hoy.";
                return RedirectToAction("Index", "Trabajador");
            }

            var asistencia = new Asistencia
            {
                id_usuario = idUsuario.Value,
                fecha = hoy,
                hora_entrada = DateTime.Now.TimeOfDay
            };

            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Entrada registrada correctamente.";

            return RedirectToAction("Index", "Trabajador");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarSalida()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
                return RedirectToAction("Index", "Login");

            var hoy = DateTime.Today;

            var asistencia = await _context.Asistencias
                .FirstOrDefaultAsync(a =>
                    a.id_usuario == idUsuario &&
                    a.fecha == hoy);

            if (asistencia == null)
            {
                TempData["Error"] = "Primero debes registrar tu entrada.";
                return RedirectToAction("Index", "Trabajador");
            }

            if (asistencia.hora_salida != null)
            {
                TempData["Error"] = "Ya registraste tu salida.";
                return RedirectToAction("Index", "Trabajador");
            }

            asistencia.hora_salida = DateTime.Now.TimeOfDay;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Salida registrada correctamente.";

            return RedirectToAction("Index", "Trabajador");
        }
    }
}
