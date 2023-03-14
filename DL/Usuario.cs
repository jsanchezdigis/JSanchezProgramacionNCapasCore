using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string UserNombre { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string? Curp { get; set; }

    public string? Imagen { get; set; }

    public byte? IdRol { get; set; }

    public bool? Status { get; set; }
    public string NombreRol { get; set; }
    public int IdDireccion { get; set; }
    public string Calle { get; set; }
    public string NumeroInterior { get; set; }
    public string NumeroExterior { get; set; }
    public int IdColonia { get; set; }
    public string NombreColonia { get; set; }
    public int IdMunicipio { get; set; }
    public string NombreMunicipio { get; set; }
    public int IdEstado { get; set; }
    public string NombreEstado { get; set; }
    public int IdPais { get; set; }
    public virtual ICollection<Aseguradora> Aseguradoras { get; } = new List<Aseguradora>();

    public virtual ICollection<Direccion> Direccions { get; } = new List<Direccion>();

    public virtual ICollection<EmpresaPoliza> EmpresaPolizas { get; } = new List<EmpresaPoliza>();

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();

    public virtual ICollection<Poliza> Polizas { get; } = new List<Poliza>();

    public virtual ICollection<Vigencium> Vigencia { get; } = new List<Vigencium>();
}
