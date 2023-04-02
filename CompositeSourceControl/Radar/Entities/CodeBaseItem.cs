namespace Radar.Entities;

public sealed record CodeBaseItem 
{
    public CodeBaseItem(CodeBase codeBase, string relativePath)
    {
        Contract.RequiresNotNull(codeBase, nameof(codeBase)); 
        Contract.RequiresNotNull(relativePath, nameof(relativePath));

        CodeBase = codeBase;
        RelativePath = relativePath.Replace('\\', '/').TrimStart('/');
    }

    public CodeBase CodeBase { get; }
    public string RelativePath { get; }
}
