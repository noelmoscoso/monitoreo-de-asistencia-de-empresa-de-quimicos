using Microsoft.AspNetCore.Mvc;

namespace Asistencia_de_trabajadores.Controllers
{
    public class TrabajadorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
