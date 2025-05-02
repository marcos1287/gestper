using Microsoft.AspNetCore.Mvc;
namespace Gestper.Controllers;

public class NosotrosController : Controller
{
    public IActionResult Nosotros()
    {
        return View("views/Nosotros/Nosotros.cshtml");
    }
}