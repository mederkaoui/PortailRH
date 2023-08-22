using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortailRH.BLL.Dtos.Employe;

namespace PortailRH.BLL.Dtos.Shared
{
    /// <summary>
    /// DepartementDto
    /// </summary>
    public class DepartementDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string? Nom { get; set; }

        /// <summary>
        /// ICollection<FonctionDto>
        /// </summary>
        public ICollection<FonctionDto>? Fonctions { get; set; }
    }
}
