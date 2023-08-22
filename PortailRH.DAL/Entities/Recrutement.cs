using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Recrutement
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public DateTime? DatedCreation { get; set; }

    public int IdFonction { get; set; }

    public int IdDocument { get; set; }

    public virtual Document IdDocumentNavigation { get; set; } = null!;

    public virtual Fonction IdFonctionNavigation { get; set; } = null!;
}
