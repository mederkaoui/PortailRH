using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.Absence
{
    /// <summary>
    /// AbsenceDto
    /// </summary>
    public class AbsenceDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// TypeAbsence
        /// </summary>
        public string? TypeAbsence { get; set; }

        /// <summary>
        /// DateDebut
        /// </summary>
        public DateTime? DateDebut { get; set; }

        /// <summary>
        /// DateFin
        /// </summary>
        public DateTime? DateFin { get; set; }

        /// <summary>
        /// Justifie
        /// </summary>
        public bool Justifie { get; set; }

        /// <summary>
        /// Justification
        /// </summary>
        public string? Justification { get; set; }

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
        /// DocumentNom
        /// </summary>
        public string? DocumentNom { get; set; }
    }
}
