using System;
using System.Collections.Generic;

namespace DL;

public partial class Dependiente
{
    public int IdDependiente { get; set; }

    public string? NumeroEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string EstadoCivil { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public int? IdDependienteTipo { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string NombreEmpleado { get; set; }
    public string ApellidoPaternoEmpleado { get; set; }
    public string ApellidoMaternoEmpleado { get; set; }

    public string Tipo { get; set; }
    public virtual DependienteTipo? IdDependienteTipoNavigation { get; set; }

    public virtual Empleado? NumeroEmpleadoNavigation { get; set; }
}
