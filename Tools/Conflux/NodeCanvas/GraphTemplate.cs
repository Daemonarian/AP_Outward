using Conflux.NodeCanvas.Nodes;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas
{
    internal class GraphTemplate
    {
        [JsonProperty("replace")]
        public List<string> Routes { get; set; } = [];

        [JsonProperty("graph")]
        public Graph Graph { get; set; } = new();

        public ConfluxScript BuildConfluxScript()
        {
            ConfluxScript script = Graph.Type switch
            {
                Graph.GraphType.DialogueTree => new ConfluxDTScript(),
                Graph.GraphType.BehaviourTree => new ConfluxBTScript(),
                _ => throw new NotImplementedException($"Graph type {Graph.Type} is not supported."),
            };

            script.Replace = [.. Routes];
            script.DerivedData = Graph.DerivedData?.BuildConfluxDerivedData(Graph) ?? throw new ConfluxValueException("Graph.DerivedData must be specified.");
            script.LocalBlackboard = Graph.LocalBlackboard.BuildConfluxLocalBlackboard(Graph);

            // determine the root node of threads

            var nodeInConnectionCounts = new Dictionary<Node, int>();
            foreach (var node in Graph.Nodes)
            {
                nodeInConnectionCounts[node] = 0;
            }

            foreach (var connection in Graph.Connections)
            {
                if (connection.TargetNode is null)
                {
                    continue;
                }

                if (!nodeInConnectionCounts.ContainsKey(connection.TargetNode))
                {
                    nodeInConnectionCounts[connection.TargetNode] = 0;
                }

                nodeInConnectionCounts[connection.TargetNode]++;
            }

            foreach (var node in Graph.Nodes)
            {
                if (node is GoToNode goToNode)
                {
                    if (goToNode.TargetNode is null)
                    {
                        continue;
                    }

                    if (!nodeInConnectionCounts.ContainsKey(goToNode.TargetNode))
                    {
                        nodeInConnectionCounts[goToNode.TargetNode] = 0;
                    }

                    nodeInConnectionCounts[goToNode.TargetNode]++;
                }
            }

            var nodeToLabel = new Dictionary<Node, string>();
            foreach (var (node, inConnectionCount) in nodeInConnectionCounts)
            {
                if (inConnectionCount <= 1)
                {
                    continue;
                }

                nodeToLabel[node] = $"thread{nodeToLabel.Count}";
            }

            nodeToLabel[Graph.Nodes[0]] = "start";

            // now build each of the threads

            script.Threads = nodeToLabel.ToDictionary(
                pair => pair.Value,
                pair => BuildConfluxBlock(Graph, nodeToLabel, pair.Key));

            return script;
        }

        private static ConfluxBlock BuildConfluxBlock(Graph graph, IReadOnlyDictionary<Node, string> nodeToLabel, Node node, bool isRoot = true)
        {
            if (!isRoot && nodeToLabel.TryGetValue(node, out var label))
            {
                return new ConfluxBlock
                {
                    Statements = [
                        new GoToConfluxStatement
                        {
                            Label = label,
                        },
                    ],
                };
            }

            if (node is GoToNode goToNode)
            {
                var targetNode = goToNode.TargetNode ?? throw new ConfluxException("GoToNode.TargetNode must be specified.");
                return BuildConfluxBlock(graph, nodeToLabel, targetNode, false);
            }

            var children = graph.Connections
                .Where(c => c.SourceNode == node)
                .Select(c => c.TargetNode ?? throw new ConfluxException("TargetNode of connection must be specified."))
                .Select(n => BuildConfluxBlock(graph, nodeToLabel, n, false))
                .ToList();
            return node.BuildConfluxBlock(graph, children);
        }
    }
}
