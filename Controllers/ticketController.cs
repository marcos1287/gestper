using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Gestper.Models;


namespace TuProyecto.Controllers
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
            var connectionString = _config.GetConnectionString("DefaultConnection");
            Ticket ticket = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tickets WHERE IdTicket = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ticket = new Ticket
                        {
                            IdTicket = (int)reader["IdTicket"],
                            Titulo = reader["Titulo"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            IdEstado = (int)reader["IdEstado"],
                            IdPrioridad = (int)reader["IdPrioridad"],
                            IdDepartamento = (int)reader["IdDepartamento"],
                            FechaCreacion = (DateTime)reader["FechaCreacion"]
                        };
                    }
                }
            }

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tickets = new List<Ticket>();
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tickets";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tickets.Add(new Ticket
                        {
                            IdTicket = (int)reader["IdTicket"],
                            Titulo = reader["Titulo"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            IdEstado = (int)reader["IdEstado"],
                            IdPrioridad = (int)reader["IdPrioridad"],
                            IdDepartamento = (int)reader["IdDepartamento"],
                            FechaCreacion = (DateTime)reader["FechaCreacion"]
                        });
                    }
                }
            }

            return Ok(tickets);
        }
    }
}
