using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class TypeContrat
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<Contrat> Contrats { get; set; } = new List<Contrat>();
}
