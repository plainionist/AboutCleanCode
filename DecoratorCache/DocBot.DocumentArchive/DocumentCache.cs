
using System;
using System.Collections.Generic;
using DocBot.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace DocBot.DocumentArchive
{
    public class DocumentCache
    {
        private MemoryCache myCache = new MemoryCache(new MemoryCacheOptions());

        internal bool TryGetValue(DocumentId id, out IReadOnlyList<Document> documents)
        {
            return myCache.TryGetValue(id, out documents);
        }

        internal void Add(DocumentId id, IReadOnlyList<Document> documents)
        {
            var options = new MemoryCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromMinutes(5));
            
            myCache.Set(id, documents, options);
        }
    }
}