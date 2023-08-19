using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Paiement
{
    public int Id { get; set; }

    public string CinEmploye { get; set; } = null!;

    public double? Salaire { get; set; }

    public DateTime? DatePaiement { get; set; }

    public virtual Employe CinEmployeNavigation { get; set; } = null!;
}
