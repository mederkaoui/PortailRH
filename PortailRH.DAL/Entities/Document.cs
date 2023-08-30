using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Document
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public int IdTypeDocument { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual ICollection<DemandeDocument> DemandeDocuments { get; set; } = new List<DemandeDocument>();

    public virtual TypeDocument IdTypeDocumentNavigation { get; set; } = null!;

    public virtual ICollection<Recrutement> Recrutements { get; set; } = new List<Recrutement>();
}
