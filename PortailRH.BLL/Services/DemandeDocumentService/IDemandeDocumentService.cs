using PortailRH.BLL.Dtos.DemandeDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.DemandeDocumentService
{
    /// <summary>
    /// IDemandeDocumentService
    /// </summary>
    public interface IDemandeDocumentService
    {
        public Task<DemandeDocumentPaginatedListDto> GetRequestedDocumnet(DemandeDocumentSearchDto searchDto);
        public Task<DemandeDocumentDto> GetDetailsDemande(int id);
        public Task<ICollection<TypeDocumentDto>> GetTypesDocument();
        public Task AddedDocument(UploadDocumentDto uploadDocumentDto);
    }
}
