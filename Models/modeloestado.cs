using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;

public class Estados
{
    [Key]
    public int IdEstado { get; set; }
    public string NombreEstado { get; set; }
}