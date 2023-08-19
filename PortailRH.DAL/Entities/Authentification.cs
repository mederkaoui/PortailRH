using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Authentification
{
    public string CinEmploye { get; set; } = null!;

    public string NomUtilisateur { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public virtual Employe CinEmployeNavigation { get; set; } = null!;
}
