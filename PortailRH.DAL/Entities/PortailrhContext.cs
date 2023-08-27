using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PortailRH.DAL.Entities;

public partial class PortailrhContext : DbContext
{
    public PortailrhContext()
    {
    }

    public PortailrhContext(DbContextOptions<PortailrhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Absence> Absences { get; set; }

    public virtual DbSet<Annonce> Annonces { get; set; }

    public virtual DbSet<Authentification> Authentifications { get; set; }

    public virtual DbSet<Banque> Banques { get; set; }

    public virtual DbSet<Conge> Conges { get; set; }

    public virtual DbSet<Contrat> Contrats { get; set; }

    public virtual DbSet<DemandeDocument> DemandeDocuments { get; set; }

    public virtual DbSet<Departement> Departements { get; set; }

    public virtual DbSet<Diplome> Diplomes { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<Fonction> Fonctions { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<Presence> Presences { get; set; }

    public virtual DbSet<Recrutement> Recrutements { get; set; }

    public virtual DbSet<TypeAbsence> TypeAbsences { get; set; }

    public virtual DbSet<TypeContrat> TypeContrats { get; set; }

    public virtual DbSet<TypeDocument> TypeDocuments { get; set; }

    public virtual DbSet<Ville> Villes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Absence>(entity =>
        {
            entity.ToTable("ABSENCE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CinEmploye)
                .HasMaxLength(50)
                .HasColumnName("CIN_EMPLOYE");
            entity.Property(e => e.DateDebut)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT");
            entity.Property(e => e.DateFin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN");
            entity.Property(e => e.IdDocument).HasColumnName("ID_DOCUMENT");
            entity.Property(e => e.IdTypeAbsence).HasColumnName("ID_TYPE_ABSENCE");
            entity.Property(e => e.Justification)
                .HasColumnType("text")
                .HasColumnName("JUSTIFICATION");
            entity.Property(e => e.Justifie).HasColumnName("JUSTIFIE");

            entity.HasOne(d => d.CinEmployeNavigation).WithMany(p => p.Absences)
                .HasForeignKey(d => d.CinEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ABSENCE_EMPLOYE");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Absences)
                .HasForeignKey(d => d.IdDocument)
                .HasConstraintName("FK_ABSENCE_DOCUMENT");

            entity.HasOne(d => d.IdTypeAbsenceNavigation).WithMany(p => p.Absences)
                .HasForeignKey(d => d.IdTypeAbsence)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ABSENCE_TYPE_ABSENCE");
        });

        modelBuilder.Entity<Annonce>(entity =>
        {
            entity.ToTable("ANNONCE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Contenu)
                .HasColumnType("text")
                .HasColumnName("CONTENU");
            entity.Property(e => e.DateAnnonce)
                .HasColumnType("datetime")
                .HasColumnName("DATE_ANNONCE");
            entity.Property(e => e.Titre).HasColumnName("TITRE");
        });

        modelBuilder.Entity<Authentification>(entity =>
        {
            entity.HasKey(e => e.CinEmploye);

            entity.ToTable("AUTHENTIFICATION");

            entity.Property(e => e.CinEmploye)
                .HasMaxLength(50)
                .HasColumnName("CIN_EMPLOYE");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(255)
                .HasColumnName("MOT_DE_PASSE");
            entity.Property(e => e.NomUtilisateur)
                .HasMaxLength(50)
                .HasColumnName("NOM_UTILISATEUR");

            entity.HasOne(d => d.CinEmployeNavigation).WithOne(p => p.Authentification)
                .HasForeignKey<Authentification>(d => d.CinEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AUTHENTIFICATION_EMPLOYE");
        });

        modelBuilder.Entity<Banque>(entity =>
        {
            entity.ToTable("BANQUE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<Conge>(entity =>
        {
            entity.ToTable("CONGE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CinEmploye)
                .HasMaxLength(50)
                .HasColumnName("CIN_EMPLOYE");
            entity.Property(e => e.DateDebut)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT");
            entity.Property(e => e.DateFin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN");
            entity.Property(e => e.Statut)
                .HasMaxLength(50)
                .HasColumnName("STATUT");

            entity.HasOne(d => d.CinEmployeNavigation).WithMany(p => p.Conges)
                .HasForeignKey(d => d.CinEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONGE_EMPLOYE");
        });

        modelBuilder.Entity<Contrat>(entity =>
        {
            entity.ToTable("CONTRAT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CinEmploye)
                .HasMaxLength(50)
                .HasColumnName("CIN_EMPLOYE");
            entity.Property(e => e.DateDebut)
                .HasColumnType("date")
                .HasColumnName("DATE_DEBUT");
            entity.Property(e => e.DateFin)
                .HasColumnType("date")
                .HasColumnName("DATE_FIN");
            entity.Property(e => e.IdType).HasColumnName("ID_TYPE");
            entity.Property(e => e.Salaire).HasColumnName("SALAIRE");

            entity.HasOne(d => d.CinEmployeNavigation).WithMany(p => p.Contrats)
                .HasForeignKey(d => d.CinEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTRAT_EMPLOYE");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Contrats)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTRAT_TYPE_CONTRAT");
        });

        modelBuilder.Entity<DemandeDocument>(entity =>
        {
            entity.ToTable("DEMANDE_DOCUMENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateDemande)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEMANDE");
            entity.Property(e => e.Raison)
                .HasColumnType("ntext")
                .HasColumnName("RAISON");
            entity.Property(e => e.TitreDocument)
                .HasMaxLength(255)
                .HasColumnName("TITRE_DOCUMENT");
        });

        modelBuilder.Entity<Departement>(entity =>
        {
            entity.ToTable("DEPARTEMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<Diplome>(entity =>
        {
            entity.ToTable("DIPLOME");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CinEmploye)
                .HasMaxLength(50)
                .HasColumnName("CIN_EMPLOYE");
            entity.Property(e => e.Niveau)
                .HasMaxLength(50)
                .HasColumnName("NIVEAU");
            entity.Property(e => e.Titre)
                .HasMaxLength(255)
                .HasColumnName("TITRE");

            entity.HasOne(d => d.CinEmployeNavigation).WithMany(p => p.Diplomes)
                .HasForeignKey(d => d.CinEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIPLOME_EMPLOYE");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("DOCUMENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdTypeDocument).HasColumnName("ID_TYPE_DOCUMENT");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .HasColumnName("NOM");

            entity.HasOne(d => d.IdTypeDocumentNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.IdTypeDocument)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_TYPE_DOCUMENT");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.Cin);

            entity.ToTable("EMPLOYE");

            entity.Property(e => e.Cin)
                .HasMaxLength(50)
                .HasColumnName("CIN");
            entity.Property(e => e.Adresse).HasColumnName("ADRESSE");
            entity.Property(e => e.ContactUrgenceNom)
                .HasMaxLength(100)
                .HasColumnName("CONTACT_URGENCE_NOM");
            entity.Property(e => e.ContactUrgenceTelephone)
                .HasMaxLength(20)
                .HasColumnName("CONTACT_URGENCE_TELEPHONE");
            entity.Property(e => e.DateNaissance)
                .HasColumnType("date")
                .HasColumnName("DATE_NAISSANCE");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("EMAIL");
            entity.Property(e => e.EstSupprime)
                .HasDefaultValueSql("((0))")
                .HasColumnName("EST_SUPPRIME");
            entity.Property(e => e.IdBanque).HasColumnName("ID_BANQUE");
            entity.Property(e => e.IdFonction).HasColumnName("ID_FONCTION");
            entity.Property(e => e.IdVille).HasColumnName("ID_VILLE");
            entity.Property(e => e.MatriculeCnss)
                .HasMaxLength(50)
                .HasColumnName("MATRICULE_CNSS");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("NOM");
            entity.Property(e => e.NombreEnfants).HasColumnName("NOMBRE_ENFANTS");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("PHOTO");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .HasColumnName("PRENOM");
            entity.Property(e => e.Rib)
                .HasMaxLength(255)
                .HasColumnName("RIB");
            entity.Property(e => e.Sexe)
                .HasMaxLength(50)
                .HasColumnName("SEXE");
            entity.Property(e => e.SituationFamiliale)
                .HasMaxLength(20)
                .HasColumnName("SITUATION_FAMILIALE");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .HasColumnName("TELEPHONE");

            entity.HasOne(d => d.IdBanqueNavigation).WithMany(p => p.Employes)
                .HasForeignKey(d => d.IdBanque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLOYE_BANQUE");

            entity.HasOne(d => d.IdFonctionNavigation).WithMany(p => p.Employes)
                .HasForeignKey(d => d.IdFonction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLOYE_FONCTION");

            entity.HasOne(d => d.IdVilleNavigation).WithMany(p => p.Employes)
                .HasForeignKey(d => d.IdVille)
                .HasConstraintName("FK_EMPLOYE_VILLE");
        });

        modelBuilder.Entity<Fonction>(entity =>
        {
            entity.ToTable("FONCTION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdDepartement).HasColumnName("ID_DEPARTEMENT");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("NOM");

            entity.HasOne(d => d.IdDepartementNavigation).WithMany(p => p.Fonctions)
                .HasForeignKey(d => d.IdDepartement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FONCTION_DEPARTEMENTS");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.ToTable("PAIEMENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CinEmploye)
                .HasMaxLength(50)
                .HasColumnName("CIN_EMPLOYE");
            entity.Property(e => e.DatePaiement)
                .HasColumnType("datetime")
                .HasColumnName("DATE_PAIEMENT");
            entity.Property(e => e.Salaire).HasColumnName("SALAIRE");

            entity.HasOne(d => d.CinEmployeNavigation).WithMany(p => p.Paiements)
                .HasForeignKey(d => d.CinEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PAIEMENT_EMPLOYE");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.ToTable("PAYS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<Presence>(entity =>
        {
            entity.ToTable("PRESENCE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CinEmploye)
                .HasMaxLength(50)
                .HasColumnName("CIN_EMPLOYE");
            entity.Property(e => e.DatePresence)
                .HasColumnType("date")
                .HasColumnName("DATE_PRESENCE");
            entity.Property(e => e.HeureEntree).HasColumnName("HEURE_ENTREE");
            entity.Property(e => e.HeureSortie).HasColumnName("HEURE_SORTIE");

            entity.HasOne(d => d.CinEmployeNavigation).WithMany(p => p.Presences)
                .HasForeignKey(d => d.CinEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRESENCE_EMPLOYE");
        });

        modelBuilder.Entity<Recrutement>(entity =>
        {
            entity.ToTable("RECRUTEMENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DatedCreation).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.IdDocument).HasColumnName("ID_DOCUMENT");
            entity.Property(e => e.IdFonction).HasColumnName("ID_FONCTION");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("NOM");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .HasColumnName("PRENOM");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .HasColumnName("TELEPHONE");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Recrutements)
                .HasForeignKey(d => d.IdDocument)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RECRUTEMENT_DOCUMENT");

            entity.HasOne(d => d.IdFonctionNavigation).WithMany(p => p.Recrutements)
                .HasForeignKey(d => d.IdFonction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RECRUTEMENT_FONCTION");
        });

        modelBuilder.Entity<TypeAbsence>(entity =>
        {
            entity.ToTable("TYPE_ABSENCE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<TypeContrat>(entity =>
        {
            entity.ToTable("TYPE_CONTRAT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<TypeDocument>(entity =>
        {
            entity.ToTable("TYPE_DOCUMENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<Ville>(entity =>
        {
            entity.ToTable("VILLE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("NOM");

            entity.HasOne(d => d.IdPaysNavigation).WithMany(p => p.Villes)
                .HasForeignKey(d => d.IdPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VILLE_PAYS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
