using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Conge
{
    /// <summary>
    /// AddCongeDto
    /// </summary>
    public class AddCongeDto
    {
        /// <summary>
        /// CIN
        /// </summary>
        public required string CIN { get; set; }

        /// <summary>
        /// DateDebut
        /// </summary>
        public DateTime DateDebut { get; set; }

        /// <summary>
        /// DateFin
        /// </summary>
        public DateTime DateFin { get; set; }

        /// <summary>
        /// IsAdministrateur
        /// </summary>
        [BindNever]
        public bool IsAdministrateur { get; set; } = true;
    }
}
