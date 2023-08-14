using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Diplome
{
    public int Id { get; set; }

    public string CinEmployee { get; set; } = null!;

    public string? Niveau { get; set; }

    public string? Titre { get; set; }

    public virtual Employee CinEmployeeNavigation { get; set; } = null!;
}
