using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gestper.Models;

namespace Gestper.Controllers
{
    public class HomeControllerTrabajador : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeControllerTrabajador(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var tickets = new List<TicketAdmin>
            {
                new TicketAdmin()
                {
                    Id = 0,
                    NombreConsulta = "NOMBRE CONSULTA",
                    Descripcion = "Breve descripción para la consulta",
                    Estado = "Nuevo",
                    Prioridad = "Alta",
                    Departamento = "Soporte",
                    CreadoPor = "Usuario1",
                    FechaCreacion = DateTime.Now
                }
            };

            return View("Index_Trabajador", tickets);
        }

        public IActionResult Detalle(int id)
        {
            var ticket = new TicketAdmin(); // Ticket vacío o uno cargado desde la base de datos
            return View("Detalle_Trabajador", ticket); // Aquí usamos el nuevo nombre de la vista
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}