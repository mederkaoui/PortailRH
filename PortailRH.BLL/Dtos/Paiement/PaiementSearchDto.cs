using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Paiement
{
    /// <summary>
    /// PaiementSearchDto
    /// </summary>
    public class PaiementSearchDto
    {
        /// <summary>
        /// CIN - NOM - PRENOM
        /// </summary>
        public string? SearchQuery { get; set; }

        /// <summary>
        /// Mois
        /// </summary>
        public int? Mois { get; set; } = DateTime.Now.Month;

        /// <summary>
        /// Annee
        /// </summary>
        public int? Annee { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// CurrentPage
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// ItemsPerPage
        /// </summary>
        public int ItemsPerPage { get; set; } = 10;
    }
}
