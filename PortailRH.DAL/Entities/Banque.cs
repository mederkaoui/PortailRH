using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Banque
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}
