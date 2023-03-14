using System;
using System.Collections.Generic;

namespace DL;

public partial class Vigencium
{
    public int IdVigencia { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdPoliza { get; set; }

    public virtual Poliza? IdPolizaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
