using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Dtos.DemandeDocument
{
    /// <summary>
    /// UploadDocumentDto
    /// </summary>
    public class UploadDocumentDto
    {
        /// <summary>
        /// IdDemande
        /// </summary>
        public required int IdDemande { get; set; }

        /// <summary>
        /// IFormFile
        /// </summary>
        public required IFormFile Document { get; set; }

        /// <summary>
        /// DocumentNom
        /// </summary>
        public string? DocumentNom { get; set; }

        /// <summary>
        /// IdTypeDocument
        /// </summary>
        public required int IdTypeDocument { get; set; }
    }
}
