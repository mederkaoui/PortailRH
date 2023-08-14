using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Authentification
{
    public string CinEmployee { get; set; } = null!;

    public string NomUtilisateur { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public virtual Employee CinEmployeeNavigation { get; set; } = null!;
}
