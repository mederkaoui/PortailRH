using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Presence
{
    public int Id { get; set; }

    public string CinEmploye { get; set; } = null!;

    public DateTime? DatePresence { get; set; }

    public TimeSpan? HeureEntree { get; set; }

    public TimeSpan? HeureSortie { get; set; }

    public virtual Employe CinEmployeNavigation { get; set; } = null!;
}
