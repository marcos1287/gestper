namespace Gestper.Models;

public class Usuario
{
    public int Idusuario { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Correo { get; set; }

    public string Contrasena { get; set; }

    public string Telefono { get; set; }

    public int IdRol { get; set; }  // Este será 3 por defecto al registrar

    public bool Activo { get; set; }
}