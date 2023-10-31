
using System.Collections.Generic;

public interface IConfiguration
{
    IReadOnlyCollection<KeywordGroup> GetKeywordGroups();
}
