using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Departement
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<Fonction> Fonctions { get; set; } = new List<Fonction>();
}
