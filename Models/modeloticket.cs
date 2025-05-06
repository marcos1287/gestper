using System.ComponentModel.DataAnnotations;

namespace Gestper.Models
{
    public class Ticket
    {
        [Key] 
        public int IdTicket { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }  // Clave foránea

        // Relación con el modelo Estado
        public virtual Estados Estado { get; set; }  // Propiedad de navegación

        public int IdCategoria { get; set; }
        public int IdPrioridad { get; set; }
        public int IdDepartamento { get; set; }
    }
}