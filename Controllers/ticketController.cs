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
            var correo = HttpContext.Session.GetString("UsuarioCorreo");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == correo);
            return usuario?.IdUsuario ?? 0;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Categoria)
                .Include(t => t.Prioridad)
                .Include(t => t.Departamento)
                .ToListAsync();

            return View();
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

            return View("Views/CRUD/crud.ticket.cshtml", ticketsCliente);
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

        public IActionResult Create()
        {
            var estados = _context.Estados.ToList();
            var categorias = _context.Categorias.ToList();
            var prioridades = _context.Prioridades.ToList();
            var departamentos = _context.Departamentos.ToList();

            if (!estados.Any() || !categorias.Any() || !prioridades.Any() || !departamentos.Any())
            {
                return RedirectToAction("Error", "Home");
            }

            ViewBag.Estados = new SelectList(estados, "IdEstado", "NombreEstado");
            ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "Nombre");
            ViewBag.Prioridades = new SelectList(prioridades, "IdPrioridad", "NombrePrioridad");
            ViewBag.Departamentos = new SelectList(departamentos, "IdDepartamento", "Nombre");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.FechaCreacion = DateTime.Now;
                ticket.IdEstado = 1; // Estado "Abierto"
                ticket.IdPrioridad = 2; // Media (asignado automáticamente)

                var correo = HttpContext.Session.GetString("UsuarioCorreo");
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

                if (usuario == null)
                {
                    TempData["LoginError"] = "Debe iniciar sesión para crear un ticket.";
                    return RedirectToAction("Index", "Home");
                }

                ticket.IdUsuario = usuario.IdUsuario;

                // Asignar trabajador con menos tickets abiertos
                var tecnico = await _context.Usuarios
                    .Where(u => u.IdRol == 2)
                    .OrderBy(u => _context.Tickets.Count(t => t.IdSoporteAsignado == u.IdUsuario && t.IdEstado != 3))
                    .FirstOrDefaultAsync();

                if (tecnico != null)
                {
                    ticket.IdSoporteAsignado = tecnico.IdUsuario;
                }

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Ticket creado exitosamente.";
                return RedirectToAction("MisTickets");
            }

            // Solo recargas los combos visibles
            ViewBag.Categorias =
                new SelectList(_context.Categorias.ToList(), "IdCategoria", "Nombre", ticket.IdCategoria);
            ViewBag.Departamentos = new SelectList(_context.Departamentos.ToList(), "IdDepartamento", "Nombre",
                ticket.IdDepartamento);

            return View(ticket);
        }
    }
}