﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? DireccionWeb { get; set; }

    public string? Logo { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
