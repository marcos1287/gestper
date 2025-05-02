using Microsoft.AspNetCore.Mvc;

namespace Gestper.Controllers
{
    public class CaracteristicasController : Controller
    {
        public IActionResult Caracteristicas()
        {
            return View();
        }
    }
}
