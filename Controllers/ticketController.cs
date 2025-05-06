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
                .Include(t => t.IdEstado)
                .Include(t => t.IdCategoria)
                .Include(t => t.IdPrioridad)
                .Include(t => t.IdDepartamento)
                .ToListAsync();

            return View(tickets);
        }

        public IActionResult MisTickets()
        {
            
            int idUsuario = ObtenerIdUsuarioActual();
            if (idUsuario == 0) return Unauthorized();

            var ticketsCliente = _context.Tickets
                .Include(t => t.Estados)
                .Include(t => t.IdCategoria)
                .Include(t => t.IdPrioridad)
                .Include(t => t.IdDepartamento)
                .Where(t => t.IdUsuario == idUsuario)
                .ToList();

            
            if (!ticketsCliente.Any())
            {
               
                return RedirectToAction("Views/tickets/Create.cshtml", "Ticket");
            }

            return View(ticketsCliente);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Estados)
                .Include(t => t.Categorias)
                .Include(t => t.Prioridades)
                .Include(t => t.Departamentos)
                .FirstOrDefaultAsync(t => t.IdTicket == id);

            if (ticket == null) return NotFound();

            ViewBag.Estados = new SelectList(await _context.Estados.ToListAsync(), "IdEstado", "Nombre", ticket.IdEstado);
            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "IdCategoria", "Nombre", ticket.IdCategoria);
            ViewBag.Prioridades = new SelectList(await _context.Prioridades.ToListAsync(), "IdPrioridad", "Nombre", ticket.IdPrioridad);
            ViewBag.Departamentos = new SelectList(await _context.Departamentos.ToListAsync(), "IdDepartamento", "Nombre", ticket.IdDepartamento);

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

        public IActionResult Create()
        {
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
                ticket.IdEstado = 1; // Estado "Nuevo"

                // Buscar el usuario de soporte con menos tickets abiertos
                var soporteDisponible = await _context.Usuarios
                    .Where(u => u.IdRol == 2)
                    .OrderBy(u => _context.Tickets.Count(t => t.IdSoporteAsignado == u.IdUsuario && t.IdEstado != 3))
                    .FirstOrDefaultAsync();

                if (soporteDisponible != null)
                {
                    ticket.IdSoporteAsignado = soporteDisponible.IdUsuario;
                }

                _context.Add(ticket);
                await _context.SaveChangesAsync();

                // Obtener el ticket con las propiedades de navegación necesarias
                var ticketCompleto = await _context.Tickets
                    .Include(t => t.Usuario)
                    .Include(t => t.OperadorAsignado)
                    .FirstOrDefaultAsync(t => t.IdTicket == ticket.IdTicket);

                // Enviar correo al creador del ticket
                if (ticketCompleto.Usuario != null)
                {
                    string asunto = $"Ticket creado: {ticketCompleto.Titulo}";
                    string cuerpo = $"Hola {ticketCompleto.Usuario.Nombre},\n\nTu ticket ha sido creado con éxito.";
                    await _emailService.EnviarCorreoAsync(ticketCompleto.Usuario.Correo, asunto, cuerpo);
                }

                // Enviar correo al operador asignado
                if (ticketCompleto.OperadorAsignado != null)
                {
                    string asunto = $"Nuevo ticket asignado: {ticketCompleto.Titulo}";
                    string cuerpo = $"Hola {ticketCompleto.OperadorAsignado.Nombre},\n\nSe te ha asignado un nuevo ticket.";
                    await _emailService.EnviarCorreoAsync(ticketCompleto.OperadorAsignado.Correo, asunto, cuerpo);
                }

                return RedirectToAction(nameof(MisTickets));
            }

            return View(ticket);
        }
    }
}