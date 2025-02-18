using System.Diagnostics;

namespace AboutCleanCode.GraphFundamentals;

[Serializable]
[DebuggerDisplay("{Id}")]
public class Node : IGraphItem, IEquatable<Node>
{
    public Node(string id)
    {
        Contract.RequiresNotNullNotEmpty(id, nameof(id));

        Id = id;

        In = [];
        Out = [];
    }

    public string Id { get; }

    public IList<Edge> In { get; }
    public IList<Edge> Out { get; }

    public bool Equals(Node? other) => other != null && Id == other.Id;
    public override bool Equals(object? obj) => Equals(obj as Node);
    public override int GetHashCode() => Id.GetHashCode();
}
