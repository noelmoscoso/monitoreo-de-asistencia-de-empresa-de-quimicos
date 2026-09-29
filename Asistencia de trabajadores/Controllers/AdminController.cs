using Microsoft.AspNetCore.Mvc;

namespace Asistencia_de_trabajadores.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
