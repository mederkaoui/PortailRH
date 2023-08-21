using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PortailRH.BLL.Dtos.Employe
{
    /// <summary>
    /// NouvelEmployeDto
    /// </summary>
    public class NouvelEmployeDto
    {
        public required string CIN { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoName { get; set; }
        public required string Email { get; set; }
        public required DateTime DateNaissance { get; set; }
        public required string Sexe { get; set; }
        public required string Telephone { get; set; }
        public required string MatriculeCnss { get; set; }
        public required string SituationFamiliale { get; set; }
        public required int Ville { get; set; }
        public int Enfants { get; set; }
        public required string Adresse { get; set; }
        public required int Fonction { get; set; }
        public required float Salaire { get; set; }
        public required int TypeContrat { get; set; }
        public required DateTime DateEntree { get; set; }
        public DateTime? DateSortie { get; set; }
        public required int Banque { get; set; }
        public required string RIB { get; set; }
        public required string ContactUrgenceNomComplet { get; set; }
        public required string ContactUrgenceTelephone { get; set; }
        public ICollection<DiplomeDto> Diplomes { get; set; } = new List<DiplomeDto>();
        public required string NomUtilisateur { get; set; }
        public required string ModeDePasse { get; set; }
    }
}
