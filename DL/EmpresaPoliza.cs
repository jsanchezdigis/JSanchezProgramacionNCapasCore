using System;
using System.Collections.Generic;

namespace DL;

public partial class EmpresaPoliza
{
    public int IdAseguradoraPoliza { get; set; }

    public int? IdAseguradora { get; set; }

    public int? IdPoliza { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Aseguradora? IdAseguradoraNavigation { get; set; }

    public virtual Poliza? IdPolizaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
