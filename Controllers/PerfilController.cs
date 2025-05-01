using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gestper.Controllers
{
    public class PerfilController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfilController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Perfil/Index
        public async Task<IActionResult> Index()
        {
            var usuarioCorreo = HttpContext.Session.GetString("UsuarioCorreo");

            if (usuarioCorreo != null)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == usuarioCorreo);

                if (usuario != null)
                {
                    return View(usuario); // Muestra la vista de perfil con los datos del usuario
                }
            }

            return RedirectToAction("Login", "Usuario"); // Si no hay usuario en sesión, redirige al login
        }
    }
}

