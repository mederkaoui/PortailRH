using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Annonce
{
    /// <summary>
    /// AnnonceSearchDto
    /// </summary>
    public class AnnonceSearchDto
    {
        /// <summary>
        /// Titre
        /// </summary>
        public string? Titre { get; set; }

        /// <summary>
        /// DateAnnonce
        /// </summary>
        public DateTime? DateAnnonce { get; set; }

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
