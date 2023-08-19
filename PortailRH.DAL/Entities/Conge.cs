using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Conge
{
    public int Id { get; set; }

    public string CinEmploye { get; set; } = null!;

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public string? Statut { get; set; }

    public virtual Employe CinEmployeNavigation { get; set; } = null!;
}
