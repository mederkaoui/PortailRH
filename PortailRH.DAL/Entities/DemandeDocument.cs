using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class DemandeDocument
{
    public int Id { get; set; }

    public string? TitreDocument { get; set; }

    public DateTime? DateDemande { get; set; }

    public string? Raison { get; set; }
}
