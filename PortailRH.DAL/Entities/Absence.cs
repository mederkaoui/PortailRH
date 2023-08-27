﻿using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Absence
{
    public int Id { get; set; }

    public int IdTypeAbsence { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public bool? Justifie { get; set; }

    public string? Justification { get; set; }

    public string CinEmploye { get; set; } = null!;

    public int? IdDocument { get; set; }

    public virtual Employe CinEmployeNavigation { get; set; } = null!;

    public virtual Document? IdDocumentNavigation { get; set; }

    public virtual TypeAbsence IdTypeAbsenceNavigation { get; set; } = null!;
}
