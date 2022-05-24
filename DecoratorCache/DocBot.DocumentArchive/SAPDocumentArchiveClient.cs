using System.Collections.Generic;
using DocBot.Entities;

namespace DocBot.DocumentArchive
{
    public class SAPDocumentArchiveClient : IDocumentArchiveClient
    {
        public IReadOnlyList<Document> FindDocuments(DocumentId id)
        {
            // TODO: query SAP 
            return null;
        }
    }
}