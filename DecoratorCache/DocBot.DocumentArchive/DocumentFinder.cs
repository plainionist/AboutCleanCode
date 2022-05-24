using System;
using System.Collections.Generic;
using System.Linq;
using DocBot.Entities;

namespace DocBot.DocumentArchive
{
    public class DocumentFinder : IDocumentFinder
    {
        private string[] ValidReleasedStates = new string[] { "FA", "R5", "PF" };

        private readonly IDocumentArchiveClient myDocumentArchive;

        public DocumentFinder(IDocumentArchiveClient documentArchive)
        {
            myDocumentArchive = documentArchive;
        }

        public Document FindDocument(DocumentId id)
        {
            var documents = myDocumentArchive.FindDocuments(id);

            if (!documents.Any())
            {
                throw new Exception($"Could not find document '{id}'");
            }

            if (id.IsExplictVersion)
            {
                return documents.First();
            }

            return GetLatestReleasedVersion(documents, id);
        }

        private Document GetLatestReleasedVersion(IReadOnlyList<Document> documents, DocumentId id)
        {
            var releasedDocuments = documents
                .Where(s => ValidReleasedStates.Contains(s.Status))
                .ToList();

            if (!releasedDocuments.Any())
            {
                throw new Exception($"Could not find a released document '{id}'");
            }

            return releasedDocuments.OrderByDescending(s => s.Id.Version).First();
        }
    }
}