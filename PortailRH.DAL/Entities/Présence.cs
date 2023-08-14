using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Présence
{
    public int Id { get; set; }

    public string CinEmployee { get; set; } = null!;

    public DateTime? DatePresence { get; set; }

    public TimeSpan? HeureEntree { get; set; }

    public TimeSpan? HeureSortie { get; set; }

    public virtual Employee CinEmployeeNavigation { get; set; } = null!;
}
