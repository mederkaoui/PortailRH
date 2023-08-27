using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class TypeAbsence
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();
}
