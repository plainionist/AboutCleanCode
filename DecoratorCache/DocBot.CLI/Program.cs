using DocBot.DocumentArchive;

namespace DocBot.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: parse command line arguments

            // composing the application
            var cache = new DocumentCache();
            var client = new DocumentArchiveCacheDecorator(new SAPDocumentArchiveClient(), cache);
            var documentFinder = new DocumentFinder(client);

            // TODO: start document generation
        }
    }
}
