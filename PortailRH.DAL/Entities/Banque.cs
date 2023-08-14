using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Banque
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
