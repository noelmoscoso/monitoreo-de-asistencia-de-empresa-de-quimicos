using Asistencia_de_trabajadores.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Asistencia_de_trabajadores.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReporteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Atrasos(DateTime? fecha)
        {
            DateTime fechaconsulta = fecha ?? DateTime.Today;
            DateTime inicio = fechaconsulta.Date;
            DateTime fin = inicio.AddDays(1);

            var atrasos = await _context.Asistencias.Include(a => a.Usuario).Where(a => a.fecha >= inicio && a.fecha < fin && a.hora_entrada > new TimeSpan(9, 30, 0)).ToListAsync();

            ViewBag.Fecha = fechaconsulta;

            return View("~/Views/Reportes/Atrasos.cshtml",atrasos);
        }

        public async Task<IActionResult> SalidasAnticipadas(DateTime? fecha)
        {
            DateTime fechaconsulta = fecha ?? DateTime.Today;
            string fechaTexto = fechaconsulta.ToString("yyyy-MM-dd");

            var asistencias = await _context.Asistencias.Include(a => a.Usuario).ToListAsync();

            var salidas = asistencias.Where(a => a.fecha.ToString("yyyy-MM-dd") == fechaTexto && a.hora_salida != null && a.hora_salida < new TimeSpan(17, 30, 0)).ToList();

            ViewBag.fecha = fechaconsulta;

            return View("~/Views/Reportes/SalidasAnticipadas.cshtml",salidas);
        }


        public async Task<IActionResult> Inasistencias(DateTime? fecha)
        {
            DateTime fechaConsulta = fecha ?? DateTime.Today;

            var trabajadores = await _context.Usuarios.Where(u => u.Id_rol == 2).Where(u => !_context.Asistencias.Any(a => a.id_usuario == u.id_usuario &&
                a.fecha == fechaConsulta)).ToListAsync();

            ViewBag.fecha = fechaConsulta;

            return View("~/Views/Reportes/Inasistencias.cshtml",trabajadores);
        }
    }
}
