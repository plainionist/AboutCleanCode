using System.Collections.Generic;
using System.Linq;

public class KeywordGroup
{
    private readonly IReadOnlyCollection<Keyword> myMatchingKeywords;

    public KeywordGroup(Keyword keyword, IReadOnlyCollection<Keyword> matchingKeywords)
        : this(keyword)
    {
        Contract.RequiresNotNull(matchingKeywords);

        myMatchingKeywords = matchingKeywords;
    }

    protected KeywordGroup(Keyword keyword)
    {
        Contract.RequiresNotNull(keyword);

        Keyword = keyword;
    }

    public Keyword Keyword { get; }

    public virtual bool Matches(Keyword value) => myMatchingKeywords.Contains(value);

    public static readonly KeywordGroup All = new AllKeywordGroup();
    
    private class AllKeywordGroup : KeywordGroup
    {
        public AllKeywordGroup()
        : base(Keyword.All)
        {
        }

        public override bool Matches(Keyword value) => true;
    }
}
