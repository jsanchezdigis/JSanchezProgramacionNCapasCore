using System;
using System.Collections.Generic;

namespace DL;

public partial class Movimiento
{
    public int IdMovimiento { get; set; }

    public int? IdMovimientoTipo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuario { get; set; }

    public virtual MovimientoTipo? IdMovimientoTipoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
