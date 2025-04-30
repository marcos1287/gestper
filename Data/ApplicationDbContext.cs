using Gestper.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestper.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Aquí defines las tablas de la base de datos.
        public DbSet<Usuario> Usuarios { get; set; }
    }
}