using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaDeVentasCafe.Models;

public partial class Mediodepago
{
    public int IdMedioDePago { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]

    public virtual ICollection<Cobranza> Cobranzas { get; set; } = new List<Cobranza>();
}
