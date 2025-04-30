using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Gestper.Models;

namespace Gestper.Controllers
{
    public class TicketController : Controller
    {
        private readonly IConfiguration _config;

        public TicketController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Views/tickets/tickets.cshtml");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Views/tickets/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ticket ticket)
        {
            if (!ModelState.IsValid)
                return View(ticket);

            var connectionString = _config.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(@"
                INSERT INTO Tickets (Titulo, Descripcion, FechaCreacion, IdUsuario, IdEstado, IdCategoria, IdPrioridad, IdDepartamento)
                VALUES (@Titulo, @Descripcion, @FechaCreacion, @IdUsuario, @IdEstado, @IdCategoria, @IdPrioridad, @IdDepartamento)", conn);

            cmd.Parameters.AddWithValue("@Titulo", ticket.Titulo);
            cmd.Parameters.AddWithValue("@Descripcion", ticket.Descripcion);
            cmd.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
            cmd.Parameters.AddWithValue("@IdUsuario", ticket.IdUsuario);
            cmd.Parameters.AddWithValue("@IdEstado", ticket.IdEstado);
            cmd.Parameters.AddWithValue("@IdCategoria", ticket.IdCategoria);
            cmd.Parameters.AddWithValue("@IdPrioridad", ticket.IdPrioridad);
            cmd.Parameters.AddWithValue("@IdDepartamento", ticket.IdDepartamento);

            conn.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("Index");
        }
    }
}

