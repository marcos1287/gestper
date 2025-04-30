using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Gestper.Models;

namespace Gestper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TicketController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var connectionString = _config.GetConnectionString("DefaultConnection");
                Ticket ticket = null;

                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand("SELECT * FROM Tickets WHERE IdTicket = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ticket = new Ticket
                    {
                        IdTicket = (int)reader["IdTicket"],
                        Titulo = reader["Titulo"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        FechaCreacion = (DateTime)reader["FechaCreacion"],
                        IdUsuario = reader["IdUsuario"] != DBNull.Value ? (int)reader["IdUsuario"] : 0,
                        IdEstado = reader["IdEstado"] != DBNull.Value ? (int)reader["IdEstado"] : 0,
                        IdCategoria = reader["IdCategoria"] != DBNull.Value ? (int)reader["IdCategoria"] : 0,
                        IdPrioridad = reader["IdPrioridad"] != DBNull.Value ? (int)reader["IdPrioridad"] : 0,
                        IdDepartamento = reader["IdDepartamento"] != DBNull.Value ? (int)reader["IdDepartamento"] : 0
                    };
                }

                return ticket == null ? NotFound() : Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var tickets = new List<Ticket>();
                var connectionString = _config.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand("SELECT * FROM Tickets", conn);

                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tickets.Add(new Ticket
                    {
                        IdTicket = (int)reader["IdTicket"],
                        Titulo = reader["Titulo"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        FechaCreacion = (DateTime)reader["FechaCreacion"],
                        IdUsuario = reader["IdUsuario"] != DBNull.Value ? (int)reader["IdUsuario"] : 0,
                        IdEstado = reader["IdEstado"] != DBNull.Value ? (int)reader["IdEstado"] : 0,
                        IdCategoria = reader["IdCategoria"] != DBNull.Value ? (int)reader["IdCategoria"] : 0,
                        IdPrioridad = reader["IdPrioridad"] != DBNull.Value ? (int)reader["IdPrioridad"] : 0,
                        IdDepartamento = reader["IdDepartamento"] != DBNull.Value ? (int)reader["IdDepartamento"] : 0
                    });
                }

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
