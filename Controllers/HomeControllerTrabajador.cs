using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Gestper.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrabajadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Estado)
                .ToListAsync();

            return View("Index_Trabajador", tickets);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Estado)
                .FirstOrDefaultAsync(t => t.IdTicket == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View("Detalle_Trabajador", ticket);
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