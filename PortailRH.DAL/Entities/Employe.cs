using System;
using System.Collections.Generic;

namespace PortailRH.DAL.Entities;

public partial class Employe
{
    public string Cin { get; set; } = null!;

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Email { get; set; }

    public DateTime? DateNaissance { get; set; }

    public string? Sexe { get; set; }

    public string? Telephone { get; set; }

    public string? MatriculeCnss { get; set; }

    public string? SituationFamiliale { get; set; }

    public int? NombreEnfants { get; set; }

    public string? Photo { get; set; }

    public string? Adresse { get; set; }

    public int? IdVille { get; set; }

    public int IdFonction { get; set; }

    public int IdBanque { get; set; }

    public string? Rib { get; set; }

    public string? ContactUrgenceNom { get; set; }

    public string? ContactUrgenceTelephone { get; set; }

    public bool? EstSupprime { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual Authentification? Authentification { get; set; }

    public virtual ICollection<Conge> Conges { get; set; } = new List<Conge>();

    public virtual ICollection<Contrat> Contrats { get; set; } = new List<Contrat>();

    public virtual ICollection<Diplome> Diplomes { get; set; } = new List<Diplome>();

    public virtual Banque IdBanqueNavigation { get; set; } = null!;

    public virtual Fonction IdFonctionNavigation { get; set; } = null!;

    public virtual Ville? IdVilleNavigation { get; set; }

    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();

    public virtual ICollection<Presence> Presences { get; set; } = new List<Presence>();
}
