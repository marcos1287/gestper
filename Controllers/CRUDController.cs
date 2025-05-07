using Microsoft.AspNetCore.Mvc;
using Gestper.Data;
using Gestper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        
        private readonly ApplicationDbContext _context;

        public CRUDController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Perfil()
        {
            var usuarioCorreo = HttpContext.Session.GetString("UsuarioCorreo");
            if (string.IsNullOrEmpty(usuarioCorreo))
            {
                return RedirectToAction("Login", "Usuario");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == usuarioCorreo);

            if (usuario == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View("crud.perfil", usuario);
        }
    }
}
