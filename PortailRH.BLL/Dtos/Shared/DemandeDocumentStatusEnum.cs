using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Shared
{
    /// <summary>
    /// DemandeDocumentStatusEnum
    /// </summary>
    public enum DemandeDocumentStatusEnum
    {
        /// <summary>
        /// EnAttente
        /// </summary>
        EnAttente,

        /// <summary>
        /// EnCours
        /// </summary>
        EnCours,

        /// <summary>
        /// Termine
        /// </summary>
        Termine,

        /// <summary>
        /// Annule
        /// </summary>
        Annule
    }
}
