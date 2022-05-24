using System.Collections.Generic;
using DocBot.Entities;

namespace DocBot.DocumentArchive
{
    public interface IDocumentArchiveClient
    {
        /// <summary>
        /// If the DocumentId does not contain a version number, documents
        /// of all versions are returned.
        /// </summary>
        IReadOnlyList<Document> FindDocuments(DocumentId id);
    }
}