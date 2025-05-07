using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Gestper.Services;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Gestper.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public TicketController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        private int ObtenerIdUsuarioActual()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(userId) ? 0 : int.Parse(userId);
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Categoria)
                .Include(t => t.Prioridad)
                .Include(t => t.Departamento)
                .ToListAsync();

            return View("Views/tickets/tickets.cshtml");
        }

        public IActionResult MisTickets()
        {

            int idUsuario = ObtenerIdUsuarioActual();
            if (idUsuario == 0) return Unauthorized();

            var ticketsCliente = _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Categoria)
                .Include(t => t.Prioridad)
                .Include(t => t.Departamento)
                .Where(t => t.IdUsuario == idUsuario)
                .ToList();


            if (!ticketsCliente.Any())
            {

                return RedirectToAction("Create", "Ticket");
            }

            return View(ticketsCliente);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Categoria)
                .Include(t => t.Prioridad)
                .Include(t => t.Departamento)
                .FirstOrDefaultAsync(t => t.IdTicket == id);

            if (ticket == null) return NotFound();

            ViewBag.Estados =
                new SelectList(await _context.Estados.ToListAsync(), "IdEstado", "Nombre", ticket.IdEstado);
            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "IdCategoria", "Nombre",
                ticket.IdCategoria);
            ViewBag.Prioridades = new SelectList(await _context.Prioridades.ToListAsync(), "IdPrioridad", "Nombre",
                ticket.IdPrioridad);
            ViewBag.Departamentos = new SelectList(await _context.Departamentos.ToListAsync(), "IdDepartamento",
                "Nombre", ticket.IdDepartamento);

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.IdTicket) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tickets.Any(e => e.IdTicket == id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "IdCategoria", "Nombre");
            ViewBag.Prioridades = new SelectList(_context.Prioridades, "IdPrioridad", "Nombre");
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "IdDepartamento", "Nombre");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.FechaCreacion = DateTime.Now;
                ticket.IdUsuario = ObtenerIdUsuarioActual();
                ticket.IdEstado = 1; // Estado "Abierto"

                // Buscar el trabajador (IdRol = 2) con menos tickets abiertos
                var soporteDisponible = await _context.Usuarios
                    .Where(u => u.IdRol == 2)
                    .OrderBy(u => _context.Tickets.Count(t => t.IdUsuario == u.IdUsuario && t.IdEstado != 3))
                    .FirstOrDefaultAsync();

                // Nota: Ya no se asigna ningún soporte porque no hay campo en la tabla Tickets

                _context.Add(ticket);
                await _context.SaveChangesAsync();

                // Obtener ticket completo con usuario para enviar correo
                var ticketCompleto = await _context.Tickets
                    .Include(t => t.Usuario)
                    .FirstOrDefaultAsync(t => t.IdTicket == ticket.IdTicket);

                if (ticketCompleto?.Usuario != null)
                {
                    string asunto = $"Ticket creado: {ticketCompleto.Titulo}";
                    string cuerpo = $"Hola {ticketCompleto.Usuario.Nombre},\n\nTu ticket ha sido creado con éxito.";
                    await _emailService.EnviarCorreoAsync(ticketCompleto.Usuario.Correo, asunto, cuerpo);
                }

                return RedirectToAction(nameof(MisTickets));
            }

            return View(ticket);
        }

    }
}