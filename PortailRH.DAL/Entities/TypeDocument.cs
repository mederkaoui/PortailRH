using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class TypeDocument
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<DemandeDocument> DemandeDocuments { get; set; } = new List<DemandeDocument>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
