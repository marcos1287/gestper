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

            return View(usuario); // Si hay errores, se vuelve a mostrar el formulario
        }

        // GET: Usuario/Login
        public IActionResult Login()
        {
            return View("Views/login/login.layout.cshtml");
        }

        // POST: Usuario/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Buscar al usuario en la base de datos por correo
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == model.Correo);

                // Si el usuario existe
                if (usuario != null)
                {
                    // Verificar la contraseña (deberías encriptar las contraseñas en un entorno real)
                    if (usuario.Contrasena == model.Contrasena)
                    {
                        // Aquí puedes guardar información del usuario en la sesión, cookies o JWT

                        // Redirigir a la página principal o dashboard si el login es exitoso
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Si la contraseña no es correcta
                        ModelState.AddModelError(string.Empty, "Contraseña incorrecta.");
                    }
                }
                else
                {
                    // Si no se encuentra al usuario
                    ModelState.AddModelError(string.Empty, "Usuario no encontrado.");
                }
            }

            // Si algo falla, volvemos a mostrar el formulario de login con el error
            return View(model);
        }
    }
}
