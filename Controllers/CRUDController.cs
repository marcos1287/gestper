using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Gestper.Controllers
{
    public class CRUDController : Controller
    {
        public IActionResult Index()
        {
            // Verificación opcional de sesión
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioCorreo")))
            {
                return RedirectToAction("Login", "Usuario");
            }

            return View("Views/CRUD/crud.index.cshtml");
        }
    }
}