
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gestper.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Si se accede directamente a la URL, redirigir a la página de inicio
            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe = false)
        {
            // Aquí implementarías la lógica real de autenticación
            // Este es solo un ejemplo básico
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["LoginError"] = "Usuario y contraseña son requeridos.";
                return RedirectToAction("Index", "Home");
            }

            // Simulación de autenticación (reemplazar con tu lógica real)
            if (username == "admin" && password == "admin123")
            {
                // Autenticación exitosa - aquí implementarías la lógica real con Identity o tu sistema de autenticación
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                // Autenticación fallida
                TempData["LoginError"] = "Usuario o contraseña incorrectos.";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string name, string email, string username, string password, string confirmPassword)
        {
            // Aquí implementarías la lógica real de registro
            // Este es solo un ejemplo básico
            
            if (password != confirmPassword)
            {
                TempData["RegisterError"] = "Las contraseñas no coinciden.";
                return RedirectToAction("Index", "Home");
            }

            // Simulación de registro (reemplazar con tu lógica real)
            // En una aplicación real, aquí crearías el usuario en la base de datos
            
            TempData["SuccessMessage"] = "Registro exitoso. Ahora puede iniciar sesión.";
            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Aquí implementarías la lógica real de cierre de sesión
            
            return RedirectToAction("Index", "Home");
        }
    }
}
