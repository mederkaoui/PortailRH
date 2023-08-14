using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Pay
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<Ville> Villes { get; set; } = new List<Ville>();
}
