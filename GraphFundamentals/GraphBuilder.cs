namespace AboutCleanCode.GraphFundamentals;

[Serializable]
public class GraphBuilder
{
    public Graph Graph { get; } = new Graph();

    public Node? TryAddNode(string nodeId)
    {
        var node = new Node(nodeId);

        return Graph.TryAdd(node) ? node : null;
    }

    public Edge? TryAddEdge(string sourceNodeId, string targetNodeId)
    {
        var sourceNode = GetOrCreateNode(sourceNodeId);
        var targetNode = GetOrCreateNode(targetNodeId);

        var edge = new Edge(sourceNode, targetNode);

        if (!Graph.TryAdd(edge))
        {
            return null;
        }

        edge.Source.Out.Add(edge);
        edge.Target.In.Add(edge);

        return edge;
    }

    private Node GetOrCreateNode(string nodeId)
    {
        var node = Graph.FindNode(nodeId);
        if (node == null)
        {
            node = new Node(nodeId);
            Graph.TryAdd(node);
        }

        return node;
    }
}
