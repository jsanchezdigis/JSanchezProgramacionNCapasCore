using System;
using System.Collections.Generic;

namespace DL;

public partial class Poliza
{
    public int IdPoliza { get; set; }

    public string Nombre { get; set; } = null!;

    public byte? IdSubPoliza { get; set; }

    public string? NumeroPoliza { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<EmpresaPoliza> EmpresaPolizas { get; } = new List<EmpresaPoliza>();

    public virtual SubPoliza? IdSubPolizaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Vigencium> Vigencia { get; } = new List<Vigencium>();
}
