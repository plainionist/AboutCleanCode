using System;
using System.Collections.Generic;
using System.Linq;

public class Client
{
    private readonly IConfiguration myConfiguration;

    public Client(IConfiguration configuration)
    {
        myConfiguration = configuration;
    }

    public KeywordGroup GetKeywordGroup(ISet<Keyword> keywords)
    {
        var keywordGroups = myConfiguration.GetKeywordGroups()
            .Where(group => keywords.All(group.Matches))
            .ToList();

        if (keywordGroups.Count == 0)
        {
            return KeywordGroup.All;
        }
        else if (keywordGroups.Count > 1)
        {
            throw new ArgumentException($"Multiple keyword groups found");
        }

        return keywordGroups.First();
    }

    public string Foo(string value)
    {
        if (value.StartsWith("-"))
        {
            return null;
        }

        return value;
    }
}