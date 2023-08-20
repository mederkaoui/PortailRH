using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Employe
{
    /// <summary>
    /// GetEmployeDto
    /// </summary>
    public class GetEmployeDto
    {
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
        /// Photo
        /// </summary>
        public string? Photo { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Telephone
        /// </summary>
        public string? Telephone { get; set; }
    }
}
