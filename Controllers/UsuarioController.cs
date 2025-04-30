using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using System.Threading.Tasks;
using Gestper.Data;

namespace Gestper.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuario/Registrar
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Usuario/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.IdRol = 3;
                usuario.Activo = true;

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(usuario);
        }
    }
}