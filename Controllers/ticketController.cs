using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;

namespace Gestper.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para obtener el ID del usuario actual desde los claims
        private int ObtenerIdUsuarioActual()
        {
            // Accede al ID del usuario autenticado desde el claim "NameIdentifier"
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Si no se puede obtener el ID (usuario no autenticado), retorna 0
            return string.IsNullOrEmpty(userId) ? 0 : int.Parse(userId);
        }

        // Mostrar todos los tickets que corresponden al cliente
        public IActionResult MisTickets()
        {
            int idUsuario = ObtenerIdUsuarioActual(); // Llamada al método para obtener el ID del usuario actual

            // Si no se pudo obtener el ID del usuario (usuario no autenticado)
            if (idUsuario == 0)
            {
                return Unauthorized(); // O redirige según sea necesario (puedes también usar RedirectToAction)
            }

            // Filtra los tickets donde el IdUsuario sea igual al ID del usuario autenticado
            var ticketsCliente = _context.Tickets
                .Include(t => t.Estado) // Si quieres mostrar el nombre del estado
                .Where(t => t.IdUsuario == idUsuario)
                .ToList();

            // Retorna la vista con los tickets filtrados
            return View("~/Views/CRUD/crud.ticket.cliente.cshtml", ticketsCliente);
        }

        // Otros métodos del controlador Ticket (Ej. Create, Edit, Delete) pueden ir aquí
    }
}