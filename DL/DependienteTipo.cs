using System;
using System.Collections.Generic;

namespace DL;

public partial class DependienteTipo
{
    public int IdDependienteTipo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Dependiente> Dependientes { get; } = new List<Dependiente>();
}
