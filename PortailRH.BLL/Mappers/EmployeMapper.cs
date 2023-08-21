using PortailRH.BLL.Dtos.Employe;
using PortailRH.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Mappers
{
    public static class EmployeMapper
    {
        public static DetailsEmployeDto ToDetailsEmploye(this Employe employe)
        {
            return new DetailsEmployeDto
            {
                CIN = employe.Cin,
                Nom = employe.Nom,
                Prenom = employe.Prenom,
                Adresse = employe.Adresse,
                Photo = employe.Photo,
                Banque = employe.IdBanqueNavigation.Nom,
                RIB = employe.Rib,
                Departement = employe.IdFonctionNavigation.IdDepartementNavigation.Nom,
                Fonction = employe.IdFonctionNavigation.Nom,
                Pays = employe.IdVilleNavigation?.IdPaysNavigation.Nom,
                Ville = employe.IdVilleNavigation?.Nom,
                DateNaissance = employe.DateNaissance,
                DateEntree = employe.Contrats.OrderByDescending(x => x.DateDebut).FirstOrDefault()?.DateDebut,
                DateSortie = employe.Contrats.OrderByDescending(x => x.DateDebut).FirstOrDefault()?.DateFin,
                TypeContrat = employe.Contrats.OrderByDescending(x => x.DateDebut).FirstOrDefault()?.IdTypeNavigation.Nom,
                Telephone = employe.Telephone,
                SituationFamiliale = employe.SituationFamiliale,
                Email = employe.Email,
                Enfants = employe.NombreEnfants,
                MatriculeCnss = employe.MatriculeCnss,
                Salaire = Convert.ToSingle(employe.Contrats.OrderByDescending(x => x.DateDebut).FirstOrDefault()?.Salaire),
                Sexe = employe.Sexe,
                ContactUrgenceNomComplet = employe.ContactUrgenceNom,
                ContactUrgenceTelephone = employe.ContactUrgenceTelephone,
                Diplomes = employe.Diplomes.Select(x => new DiplomeDto
                {
                    Niveau = x.Niveau,
                    Titre = x.Titre,
                }).ToList(),
                NomUtilisateur = employe.Authentification?.NomUtilisateur,
                ModeDePasse = employe.Authentification?.MotDePasse
            };
        }
    }
}
