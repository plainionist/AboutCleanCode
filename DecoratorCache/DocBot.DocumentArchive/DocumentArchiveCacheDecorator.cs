using System.Collections.Generic;
using DocBot.Entities;

namespace DocBot.DocumentArchive
{
    public class DocumentArchiveCacheDecorator : IDocumentArchiveClient
    {
        private IDocumentArchiveClient myImpl;
        private DocumentCache myCache;

        public DocumentArchiveCacheDecorator(IDocumentArchiveClient impl, DocumentCache cache)
        {
            myImpl = impl;
            myCache = cache;
        }

        public IReadOnlyList<Document> FindDocuments(DocumentId id)
        {
            if (!myCache.TryGetValue(id, out var documents))
            {
                documents = myImpl.FindDocuments(id);
                myCache.Add(id, documents);
            }

            return documents;
        }
    }
}