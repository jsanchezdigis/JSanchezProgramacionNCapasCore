using System;
using System.Collections.Generic;

namespace DL;

public partial class MovimientoDetalle
{
    public int IdMovimientoDetalle { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? IdStatus { get; set; }

    public virtual Status? IdStatusNavigation { get; set; }
}
