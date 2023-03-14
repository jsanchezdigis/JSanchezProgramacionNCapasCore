using System;
using System.Collections.Generic;

namespace DL;

public partial class SubPoliza
{
    public byte IdSubPoliza { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; } = new List<Poliza>();
}
