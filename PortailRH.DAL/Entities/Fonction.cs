using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Fonction
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public int IdDepartement { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Departement IdDepartementNavigation { get; set; } = null!;

    public virtual ICollection<Recrutement> Recrutements { get; set; } = new List<Recrutement>();
}
