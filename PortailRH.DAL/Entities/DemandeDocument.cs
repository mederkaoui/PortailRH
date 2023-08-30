using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class DemandeDocument
{
    public int Id { get; set; }

    public string CinEmploye { get; set; } = null!;

    public string? TitreDocument { get; set; }

    public DateTime? DateDemande { get; set; }

    public string? Raison { get; set; }

    public int? IdDocument { get; set; }

    public string? Statut { get; set; }

    public virtual Employe CinEmployeNavigation { get; set; } = null!;

    public virtual Document? IdDocumentNavigation { get; set; }
}
