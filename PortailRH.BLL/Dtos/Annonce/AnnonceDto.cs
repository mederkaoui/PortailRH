using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Annonce
{
    /// <summary>
    /// AnnonceDto
    /// </summary>
    public class AnnonceDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Titre
        /// </summary>
        public string? Titre { get; set; }

        /// <summary>
        /// Contenu
        /// </summary>
        public string? Contenu { get; set; }

        /// <summary>
        /// DateAnnonce
        /// </summary>
        public DateTime? DateAnnonce { get; set; }
    }
}
