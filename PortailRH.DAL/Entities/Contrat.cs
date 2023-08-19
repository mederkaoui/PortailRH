using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Contrat
{
    public int Id { get; set; }

    public int IdType { get; set; }

    public string CinEmploye { get; set; } = null!;

    public double? Salaire { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public virtual Employe CinEmployeNavigation { get; set; } = null!;

    public virtual TypeContrat IdTypeNavigation { get; set; } = null!;
}
