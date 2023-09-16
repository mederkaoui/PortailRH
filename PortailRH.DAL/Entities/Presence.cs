using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Presence
{
    public int Id { get; set; }

    public string CinEmploye { get; set; } = null!;

    public DateTime? DatePresence { get; set; }

    public int? HeureEntree { get; set; }

    public int? HeureSortie { get; set; }

    public virtual Employe CinEmployeNavigation { get; set; } = null!;
}
