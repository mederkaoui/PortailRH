using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.DemandeDocument;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.DemandeDocumentService
{
    /// <summary>
    /// DocumentService
    /// </summary>
    public class DemandeDocumentService : IDemandeDocumentService
    {
        /// <summary>
        /// IGenericRepository<Document>
        /// </summary>
        private readonly IGenericRepository<DemandeDocument> _demandeDocumentRepository;

        /// <summary>
        /// ILogger<DocumentService>
        /// </summary>
        private readonly ILogger<DemandeDocumentService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="demandeDocumentRepository">IGenericRepository<DemandeDocument></param>
        /// <param name="logger">ILogger<DocumentService></param>
        public DemandeDocumentService(IGenericRepository<DemandeDocument> demandeDocumentRepository, ILogger<DemandeDocumentService> logger)
        {
            _demandeDocumentRepository = demandeDocumentRepository;
            _logger = logger;
        }

        //public async Task<ICollection<DemandeDocumentDto>> GetPendingRequestDocumnet()
        //{
        //    var demandes = await _demandeDocumentRepository.GetListAsync(x => x);
        //}
    }
}
