using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Paiement
{
    public int Id { get; set; }

    public string CinEmployee { get; set; } = null!;

    public double? Salaire { get; set; }

    public DateTime? DatePaiement { get; set; }

    public virtual Employee CinEmployeeNavigation { get; set; } = null!;
}
