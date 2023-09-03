using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Authentification
{
    /// <summary>
    /// UserDto
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Nom
        /// </summary>
        public string? Nom { get; set; }

        /// <summary>
        /// Prenom
        /// </summary>
        public string? Prenom { get; set; }

        /// <summary>
        /// EstAdministarteur
        /// </summary>
        public bool? EstAdministarteur { get; set; }

        /// <summary>
        /// Photo
        /// </summary>
        public string? Photo { get; set; }
    }
}
