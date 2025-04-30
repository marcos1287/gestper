namespace Gestper.Models;

public class modeloticket
{
    
}

public class Ticket
{
    public int IdTicket { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public int IdEstado { get; set; }
    public int IdPrioridad { get; set; }
    public int IdDepartamento { get; set; }
    public DateTime FechaCreacion { get; set; }
}

