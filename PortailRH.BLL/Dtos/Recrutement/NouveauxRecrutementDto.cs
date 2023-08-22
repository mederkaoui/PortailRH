using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Recrutement
{
    /// <summary>
    /// NouveauxRecrutementDto
    /// </summary>
    public class NouveauxRecrutementDto
    {
        /// <summary>
        /// Nom
        /// </summary>
        public required string Nom { get; set; }

        /// <summary>
        /// Prenom
        /// </summary>
        public required string Prenom { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Telephone
        /// </summary>
        public required string Telephone { get; set; }

        /// <summary>
        /// DateCreation
        /// </summary>
        public required DateTime DateCreation { get; set; }

        /// <summary>
        /// Fonction
        /// </summary>
        public required int Fonction { get; set; }

        /// <summary>
        /// Document
        /// </summary>
        public IFormFile? Document { get; set; }

        /// <summary>
        /// NomDocument
        /// </summary>
        public string? NomDocument { get; set; }
    }
}
