using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Annonce
{
    public int Id { get; set; }

    public string? Titre { get; set; }

    public string? Contenu { get; set; }

    public DateTime? DateAnnonce { get; set; }
}
