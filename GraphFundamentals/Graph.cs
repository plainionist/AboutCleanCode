namespace AboutCleanCode.GraphFundamentals;

[Serializable]
public class Graph
{
    private readonly Dictionary<string, Node> myNodes;
    private readonly Dictionary<string, Edge> myEdges;

    public Graph()
    {
        myNodes = [];
        myEdges = [];
    }

    public IReadOnlyCollection<Node> Nodes { get { return myNodes.Values; } }
    public IReadOnlyCollection<Edge> Edges { get { return myEdges.Values; } }

    public bool TryAdd(Node node)
    {
        Contract.RequiresNotNull(node, "node");

        if (myNodes.ContainsKey(node.Id))
        {
            return false;
        }

        myNodes.Add(node.Id, node);

        return true;
    }

    public void Add(Node node)
    {
        if (!TryAdd(node))
        {
            throw new ArgumentException("Node already exists: " + node.Id);
        }
    }

    public bool TryAdd(Edge edge)
    {
        Contract.RequiresNotNull(edge, "edge");

        if (myEdges.ContainsKey(edge.Id))
        {
            return false;
        }

        myEdges.Add(edge.Id, edge);

        return true;
    }

    public void Add(Edge edge)
    {
        if (!TryAdd(edge))
        {
            throw new ArgumentException("Edge already exists: " + edge.Id);
        }
    }

    public Node? FindNode(string nodeId) =>
        myNodes.TryGetValue(nodeId, out var node) ? node : null;

    public Node GetNode(string nodeId)
    {
        var node = FindNode(nodeId);
        
        Contract.Requires(node != null, "Node not found: " + nodeId);

        return node!;
    }
}
