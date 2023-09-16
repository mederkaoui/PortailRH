using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Absence
{
    /// <summary>
    /// EditAbsence
    /// </summary>
    public class EditAbsenceDto
    {
        /// <summary>
        /// IdTypeAbsence
        /// </summary>
        public required int IdTypeAbsence { get; set; }

        /// <summary>
        /// DateDebut
        /// </summary>
        public required DateTime DateDebut { get; set; }

        /// <summary>
        /// DateFin
        /// </summary>
        public required DateTime DateFin { get; set; }

        /// <summary>
        /// Justifie
        /// </summary>
        [BindNever]
        public bool Justifie { get; set; } = false;

        /// <summary>
        /// Justification
        /// </summary>
        public string? Justification { get; set; }

        /// <summary>
        /// CIN
        /// </summary>
        public string? CIN { get; set; }

        /// <summary>
        /// IFormFile
        /// </summary>
        public IFormFile? Document { get; set; }

        /// <summary>
        /// DocumentName
        /// </summary>
        public string? DocumentNom { get; set; }
    }
}
