using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Paiement
{
    /// <summary>
    /// PaiementDto
    /// </summary>
    public class PaiementDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// CIN
        /// </summary>
        public string? CIN { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string? Nom { get; set; }

        /// <summary>
        /// Prenom
        /// </summary>
        public string? Prenom { get; set; }

        /// <summary>
        /// Salaire
        /// </summary>
        public float? Salaire { get; set; }

        /// <summary>
        /// DatePaiement
        /// </summary>
        public DateTime? DatePaiement { get; set; }

        /// <summary>
        /// Statut
        /// </summary>
        public string? Statut { get; set; }
    }
}
