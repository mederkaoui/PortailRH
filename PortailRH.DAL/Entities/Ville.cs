using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Ville
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public int IdPays { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Pay IdPaysNavigation { get; set; } = null!;
}
