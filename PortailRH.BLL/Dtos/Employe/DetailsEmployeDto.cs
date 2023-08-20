using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Employe
{
    /// <summary>
    /// DetailsEmployeDto
    /// </summary>
    public class DetailsEmployeDto
    {
        public string? CIN { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Photo { get; set; }
        public string? Email { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string? Sexe { get; set; }
        public string? Telephone { get; set; }
        public string? MatriculeCnss { get; set; }
        public string? SituationFamiliale { get; set; }
        public string? Pays { get; set; }
        public string? Ville { get; set; }
        public int? Enfants { get; set; }
        public string? Adresse { get; set; }
        public string? Departement { get; set; }
        public string? Fonction { get; set; }
        public float? Salaire { get; set; }
        public string? TypeContrat { get; set; }
        public DateTime? DateEntree { get; set; }
        public DateTime? DateSortie { get; set; }
        public string? Banque { get; set; }
        public string? RIB { get; set; }
        public string? ContactUrgenceNomComplet { get; set; }
        public string? ContactUrgenceTelephone { get; set; }
    }
}
