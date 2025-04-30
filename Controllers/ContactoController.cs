using Microsoft.AspNetCore.Mvc;
using Gestper.Models;

namespace Gestper.Controllers
{
    public class ContactoController : Controller
    {
        [HttpGet]
        public IActionResult Contacto()
        {
            return View("Views/Contacto/contacto.cshtml"); 
        }

        [HttpPost]
        public IActionResult Contacto(ContactoViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Mensaje = "Gracias por tu mensaje. Te responderemos pronto.";
                ModelState.Clear();
                return View();
            }

            return View(model);
        }
    }
}