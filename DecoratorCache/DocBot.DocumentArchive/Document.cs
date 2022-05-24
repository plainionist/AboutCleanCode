using DocBot.Entities;

namespace DocBot.DocumentArchive
{
    public class Document
    {
        public Document(DocumentId id, string title, string status)
        {
            Id = id;
            Title = title;
            Status = status;
        }

        public DocumentId Id { get; }
        public string Title { get; }
        public string Status { get; }
    }
}