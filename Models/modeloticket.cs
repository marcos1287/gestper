using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestper.Models
{
    public class Ticket
    {
        [Key]
        public int IdTicket { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int IdEstado { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        [Required]
        public int IdPrioridad { get; set; }

        [Required]
        public int IdDepartamento { get; set; }

        // Relaciones (navegación)
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }

        [ForeignKey("IdCategoria")]
        public virtual Categoria Categoria { get; set; }

        [ForeignKey("IdPrioridad")]
        public virtual Prioridad Prioridad { get; set; }

        [ForeignKey("IdDepartamento")]
        public virtual Departamento Departamento { get; set; }
    }
}