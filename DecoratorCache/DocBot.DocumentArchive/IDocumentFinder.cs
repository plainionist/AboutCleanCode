using DocBot.Entities;

namespace DocBot.DocumentArchive
{
    public interface IDocumentFinder
    {
        /// <summary>
        /// If the DocumentId does not contain a version number, the 
        /// most recent document in state "Released" is returned.
        /// </summary>
        Document FindDocument(DocumentId dis);
    }
}