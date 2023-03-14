using System;
using System.Collections.Generic;

namespace DL;

public partial class MovimientoTipo
{
    public int IdMovimientoTipo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
}
