using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            return View("Views/registro/registro.layout.cshtml");
        }

        // POST: Usuario/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.IdRol = 3; // Asignamos el rol de usuario normal
                usuario.Activo = true; // Marcamos al usuario como activo

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home"); // Redirige a la página principal después de registrarse
            }

            return View("Views/registro/registro.layout.cshtml"); // Si hay errores, se vuelve a mostrar el formulario
        }

        // GET: Usuario/Login
        public IActionResult Login()
        {
            return View("Views/login/login.layout.cshtml");
        }

        // POST: Usuario/Login con LoginViewModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == model.Correo);

                if (usuario.Contrasena == model.Contrasena)
                {
                    HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);
                    HttpContext.Session.SetString("UsuarioId", usuario.IdUsuario.ToString());
                    HttpContext.Session.SetString("UsuarioRol", usuario.IdRol.ToString());

                    // Redirección según el rol
                    if (usuario.IdRol == 1)
                    {
                        return RedirectToAction("Index", "HomeControllerAdmin");
                    }
                    else if (usuario.IdRol == 2)
                    {
                        return RedirectToAction("Index", "Trabajador");
                    }
                    else
                    {
                        return RedirectToAction("Index", "CRUD");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuario no encontrado.");
                }
            }

            return View("Views/login/login.layout.cshtml", model);
        }
        
        [HttpPost]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }
    }
}