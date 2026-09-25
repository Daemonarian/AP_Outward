using System.Collections.Immutable;
using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Nodes;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Conflux
{
    /// <summary>
    /// An intermediate representation of a partial Conflux graph.
    /// Used for translating between Conflux scripts and NodeCanvas graphs.
    /// </summary>
    internal class ConfluxGraph
    {
        /// <summary>
        /// The context needed for creating NodeCanvas objects.
        /// </summary>
        public INodeCanvasGraphContext Context { get; init; }

        /// <summary>
        /// All of the nodes in the graph.
        /// </summary>
        public IReadOnlyList<Node> Nodes { get; init; }

        /// <summary>
        /// All of the connections in the graph.
        /// </summary>
        public IReadOnlyList<Connection> Connections { get; init; }

        /// <summary>
        /// The root node of the graph.
        /// </summary>
        public Node Root { get; init; }

        /// <summary>
        /// A mapping of labels to nodes for jump instructions.
        /// </summary>
        public IReadOnlyDictionary<string, Node> Labels { get; init; }

        /// <summary>
        /// Create a Conflux graph wrapping a single node.
        /// </summary>
        /// <param name="context">The NodeCanvas graph context.</param>
        /// <param name="node">The node to wrap.</param>
        /// <returns>The wrapper Conflux graph.</returns>
        public static ConfluxGraph CreateFromNode(INodeCanvasGraphContext context, Node node)
        {
            var leaves = Enumerable.Range(0, node.OutConnectionCount).Select(_ => new LeafNode()).ToList();
            var connections = leaves.Select(leaf => context.BuildConnection(node, leaf)).ToList();
            var labels = new Dictionary<string, Node>();
            return new ConfluxGraph(context, [node, .. leaves], connections, node, labels);
        }

        /// <summary>
        /// Create an empty Conflux graph.
        /// </summary>
        /// <param name="context">The NodeCanvas graph context.</param>
        /// <returns>The empty Conflux graph.</returns>
        public static ConfluxGraph CreateEmpty(INodeCanvasGraphContext context)
        {
            return CreateFromNode(context, new LeafNode());
        }

        /// <summary>
        /// Create an empty Conflux graph with no leaf nodes, so cannot be concatenated.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ConfluxGraph CreateTerminal(INodeCanvasGraphContext context)
        {
            return CreateFromNode(context, new TerminalNode());
        }

        private ConfluxGraph(INodeCanvasGraphContext context, IEnumerable<Node> nodes, IEnumerable<Connection> connections, Node root, IReadOnlyDictionary<string, Node> labels)
        {
            // validate the inputs

            var nodeList = nodes.Distinct().ToImmutableList();
            foreach (var node in nodeList)
            {
                var nodeType = node.GetType();
                if (!nodeType.IsAssignableTo(typeof(ConfluxNode)) && !nodeType.IsAssignableTo(context.NodeBaseType))
                {
                    throw new ArgumentException($"Nodes must be of type {context.NodeBaseType}, not {node.GetType()}.", nameof(nodes));
                }
            }

            var nodeSet = nodeList.ToImmutableHashSet();
            var connectionList = connections.Distinct().ToImmutableList();
            foreach (var connection in connections)
            {
                if (!connection.GetType().IsAssignableTo(context.ConnectionBaseType))
                {
                    throw new ArgumentException($"Connections must be of type {context.ConnectionBaseType}, not {connection.GetType()}.", nameof(connections));
                }

                if (connection.SourceNode is null)
                {
                    throw new ArgumentException("The source node of all connections may not be null.", nameof(connections));
                }

                if (!nodeSet.Contains(connection.SourceNode))
                {
                    throw new ArgumentException($"The source node of all connections must be a node in the graph, not {connection.SourceNode}.", nameof(connections));
                }

                if (connection.TargetNode is null)
                {
                    throw new ArgumentException("The target node of all connections may not be null.", nameof(connections));
                }

                if (!nodeSet.Contains(connection.TargetNode))
                {
                    throw new ArgumentException($"The target node of all connections must be a node in the graph, not {connection.TargetNode}.", nameof(connections));
                }
            }

            if (!nodeSet.Contains(root))
            {
                throw new ArgumentException($"The root must be a node in the graph, not {root}.", nameof(root));
            }

            foreach (var pair in labels)
            {
                if (!nodeSet.Contains(pair.Value))
                {
                    throw new ArgumentException($"Each labeled node must be a node in the graph, not {pair.Value}.", nameof(labels));
                }
            }

            // temporarily index nodes by in-order traversal (BFS)

            var indexByNode = new Dictionary<Node, int>();

            var nodesToVisit = new Stack<Node>();
            foreach (var node in nodeList.Reverse())
            {
                nodesToVisit.Push(node);
            }

            nodesToVisit.Push(root);
            while (nodesToVisit.TryPop(out var currNode))
            {
                if (indexByNode.ContainsKey(currNode))
                {
                    continue;
                }

                indexByNode[currNode] = indexByNode.Count;

                var outConnections = connectionList.Where(c => c.SourceNode == currNode).ToImmutableList();
                foreach (var connection in outConnections.Reverse())
                {
                    if (connection.TargetNode is not null)
                    {
                        nodesToVisit.Push(connection.TargetNode);
                    }
                }
            }

            // sort nodes and connections

            nodeList = [.. nodes.OrderBy(n => indexByNode[n])];
            connectionList = [.. connections.OrderBy(c => (c.SourceNode is null ? -1 : indexByNode[c.SourceNode]))];

            // set properties

            Context = context;
            Nodes = nodeList;
            Connections = connectionList;
            Root = root;
            Labels = labels;
        }

        /// <summary>
        /// Get the node which follows the specified node in the graph.
        /// </summary>
        /// <param name="node">The parent node.</param>
        /// <param name="outConnectionIndex">The index of the children when multiple exist</param>
        /// <returns>The following node.</returns>
        /// <exception cref="Exception">If the specified following node does not exist.</exception>
        public Node GetNextNode(Node node, int outConnectionIndex = 0)
        {
            var outConnections = Connections.Where(c => c.SourceNode == node).ToImmutableList();
            return outConnections[outConnectionIndex].TargetNode ?? throw new Exception("The target node of the connection should not be null.");
        }

        /// <summary>
        /// Merge two Conflux graphs.
        /// The other graph will be a disconnected sub-graph in the new graph.
        /// </summary>
        /// <param name="other">The other Conflux graph.</param>
        /// <param name="label">An optional label for the root node of other.</param>
        /// <returns>The merged Conflux graph.</returns>
        public ConfluxGraph Merge(ConfluxGraph other, string? label = null)
        {
            // validate the input

            if (other.Context != Context)
            {
                throw new ArgumentException($"Other graph must have the same context as this graph, not {other.Context}.", nameof(other));
            }

            // merge the nodes

            var nodes = Nodes.Concat(other.Nodes).Distinct().ToList();

            // merge the connections

            var connections = Connections.Concat(other.Connections).Distinct().ToList();

            // merge the labels

            var labels = new Dictionary<string, Node>(Labels);
            foreach (var otherLabel in other.Labels)
            {
                if (labels.TryGetValue(otherLabel.Key, out var node))
                {
                    if (otherLabel.Value != node)
                    {
                        throw new ArgumentException($"Label collision cannot be resolved between \"{otherLabel.Key}\" ({node}) and \"{otherLabel.Key}\" ({otherLabel.Value}).", nameof(other));
                    }

                    labels[otherLabel.Key] = otherLabel.Value;
                }
            }

            if (label is not null)
            {
                if (labels.TryGetValue(label, out var node))
                {
                    throw new ArgumentException($"Label collision cannot be resolved between \"{label}\" ({node}) and \"{label}\" ({other.Root}).", nameof(label));
                }

                labels[label] = other.Root;
            }

            return new ConfluxGraph(Context, nodes, connections, Root, labels);
        }

        /// <summary>
        /// Creates a new Conflux Graph graph equivalent to this one with the
        /// specified other Conflux Graph attached to the specified leaf.
        /// </summary>
        /// <param name="other">The other Conflux Graph.</param>
        /// <param name="leaf">The leaf to which to attach the other graph.</param>
        /// <returns>The new conflux graph.</returns>
        public ConfluxGraph Attach(ConfluxGraph other, LeafNode leaf)
        {
            // validate the input

            if (other.Context != Context)
            {
                throw new ArgumentException($"Other graph must have same context as this graph, not {other.Context}.", nameof(other));
            }

            if (!Nodes.Contains(leaf))
            {
                throw new ArgumentException($"The specified leaf must be a leaf in the graph, not {leaf}.", nameof(leaf));
            }

            // merge the graphs

            var mergedGraph = Merge(other);

            // remove leaf from the list of nodes

            var nodes = mergedGraph.Nodes
                .Where(n => n != leaf)
                .ToList();

            // replace all connections to leaf with connections to other.Root

            var connections = mergedGraph.Connections
                .Select(connection =>
                {
                    if (connection.TargetNode != leaf)
                    {
                        return connection;
                    }

                    var sourceNode = connection.SourceNode ?? throw new Exception("The source node should not be null.");
                    return Context.BuildConnection(sourceNode, other.Root);
                })
                .ToList();

            // update the root if leaf was the root

            var root = Root == leaf ? other.Root : Root;

            // update any labels pointing to leaf

            var labels = new Dictionary<string, Node>();
            foreach (var label in mergedGraph.Labels)
            {
                if (label.Value == leaf)
                {
                    labels[label.Key] = other.Root;
                }
                else
                {
                    labels[label.Key] = label.Value;
                }
            }

            // return the new Conflux graph

            return new ConfluxGraph(Context, nodes, connections, root, labels);
        }

        /// <summary>
        /// Concatenate this graph and the other graph by attaching other to every leaf.
        /// </summary>
        /// <param name="other">The other Conflux Graph.</param>
        /// <returns>The new conflux graph.</returns>
        public ConfluxGraph Concat(ConfluxGraph other)
        {
            var leaves = Nodes.OfType<LeafNode>().ToImmutableList();

            var graph = this;
            foreach (var leaf in leaves)
            {
                graph = graph.Attach(other, leaf);
            }

            return graph;
        }

        /// <summary>
        /// Build an equivalent NodeCanvas graph object.
        /// </summary>
        /// <returns>The NodeCanvas graph.</returns>
        public GraphSerializationData BuildNodeCanvasGraph()
        {
            var graph =
                UpdateRoot()
                .ResolveLabels()
                .PruneLeaves()
                .PruneTerminals()
                .ReplaceTerminals();

            var nodeBaseType = Context.NodeBaseType;
            foreach (var node in graph.Nodes)
            {
                if (!node.GetType().IsAssignableTo(nodeBaseType))
                {
                    throw new Exception($"Node should be assignable to type {nodeBaseType}, not {node.GetType()}.");
                }
            }

            var connectionBaseType = Context.ConnectionBaseType;
            foreach (var connection in graph.Connections)
            {
                if (!connection.GetType().IsAssignableTo(connectionBaseType))
                {
                    throw new Exception($"Connection should be assignable to type {connectionBaseType}, not {connection.GetType()}.");
                }
            }

            return new GraphSerializationData
            {
                Type = Context.GraphType,
                LocalBlackboard = Context.Script.LocalBlackboard,
                DerivedData = Context.Script.DerivedData,
                Nodes = graph.Nodes.ToList(),
                Connections = graph.Connections.ToList(),
            };
        }

        /// <summary>
        /// Resolve all labels by redirecting any connections to <see cref="GoToLabelNode" />
        /// to the corresponding labeled node.
        /// </summary>
        /// <returns></returns>
        private ConfluxGraph ResolveLabels()
        {
            // filter all GoToLabelNodes from the list of nodes

            var nodes = Nodes.Where(n => n is not GoToLabelNode);

            // resolve labeled nodes first

            var labels = new Dictionary<string, Node>();
            foreach (var pair in Labels)
            {
                var node = pair.Value;

                var seenLabels = new HashSet<string>();
                while (node is GoToLabelNode goToLabelNode)
                {
                    if (!seenLabels.Add(goToLabelNode.Label))
                    {
                        throw new Exception($"Cycle of go-to statements detected involving label {goToLabelNode.Label}.");
                    }

                    node = Labels[goToLabelNode.Label];
                }

                labels[pair.Key] = node;
            }

            // resolve root

            var root = Root;
            if (root is GoToLabelNode rootGoToLabelNode)
            {
                root = labels[rootGoToLabelNode.Label];
            }

            // replace any connections to GoToLabelNodes with a connection to the labeled node

            var connections = Connections.Select(connection =>
            {
                if (connection.TargetNode is GoToLabelNode goToLabelNode)
                {
                    var sourceNode = connection.SourceNode ?? throw new Exception("Source node should not be null.");
                    var labeledNode = labels[goToLabelNode.Label];
                    return Context.BuildConnection(connection.SourceNode, labeledNode);
                }
                else
                {
                    return connection;
                }
            });

            // construct the new Conflux graph

            return new ConfluxGraph(Context, nodes, connections, root, labels);
        }

        /// <summary>
        /// Prune all leaf nodes by replacing them with terminal nodes.
        /// </summary>
        /// <returns>The pruned Conflux graph.</returns>
        private ConfluxGraph PruneLeaves()
        {
            return Concat(CreateTerminal(Context));
        }

        /// <summary>
        /// Prune unnecessary terminal nodes by simply removing them.
        /// </summary>
        /// <returns>The pruned Conflux graph.</returns>
        private ConfluxGraph PruneTerminals()
        {
            // keep any terminal nodes which are the root or labeled

            var terminalsToKeep = Labels.Values
                .Append(Root)
                .OfType<TerminalNode>()
                .ToHashSet();

            // remove any trailing connections to terminal nodes

            var connectionsToRemove = new HashSet<Connection>();
            foreach (var node in Nodes)
            {
                foreach (var connection in Connections.Where(c => c.SourceNode == node).Reverse())
                {
                    if (connection.TargetNode is not TerminalNode terminalNode || terminalsToKeep.Contains(terminalNode))
                    {
                        break;
                    }

                    connectionsToRemove.Add(connection);
                }
            }

            var connections = Connections
                .Where(c => !connectionsToRemove.Contains(c))
                .ToList();

            // remove any terminals which no longer have any edges

            var terminalsToRemove = Nodes
                .OfType<TerminalNode>()
                .Where(n => !terminalsToKeep.Contains(n) && !connections.Any(c => c.TargetNode == n))
                .ToHashSet();
            var nodes = Nodes
                .Where(n => n is not TerminalNode tn || !terminalsToRemove.Contains(tn))
                .ToList();

            // create new Conflux graph

            return new ConfluxGraph(Context, nodes, connections, Root, Labels);
        }

        /// <summary>
        /// Replace the specified nodes.
        /// </summary>
        /// <param name="replacements">A mapping of nodes to replace to their replacements.</param>
        /// <returns>The new Conflux graph.</returns>
        private ConfluxGraph ReplaceNodes(IReadOnlyDictionary<Node, Node> replacements)
        {
            var nodes = Nodes
                .Select(node => replacements.TryGetValue(node, out var replacementNode) ? replacementNode : node)
                .ToList();
            var connections = Connections
                .Select(connection =>
                {
                    if (connection.SourceNode is not null && connection.TargetNode is not null && replacements.TryGetValue(connection.SourceNode, out var replacementNode))
                    {
                        return Context.BuildConnection(replacementNode, connection.TargetNode);
                    }

                    return connection;
                })
                .Select(connection =>
                {
                    if (connection.SourceNode is not null && connection.TargetNode is not null && replacements.TryGetValue(connection.TargetNode, out var replacementNode))
                    {
                        return Context.BuildConnection(connection.SourceNode, replacementNode);
                    }

                    return connection;
                })
                .ToList();
            var root = replacements.TryGetValue(Root, out var replacementRoot) ? replacementRoot : Root;
            var labels = Labels
                .ToDictionary(pair => pair.Key, pair =>
                {
                    if (replacements.TryGetValue(pair.Value, out var replacementNode))
                    {
                        return replacementNode;
                    }

                    return pair.Value;
                });
            return new ConfluxGraph(Context, nodes, connections, root, labels);
        }

        /// <summary>
        /// Replace generic terminal nodes with context specific terminal nodes.
        /// </summary>
        /// <returns>The new Conflux graph.</returns>
        private ConfluxGraph ReplaceTerminals()
        {
            var replacements = Nodes
                .OfType<TerminalNode>()
                .ToDictionary(node => (Node)node, _ => Context.BuildTerminalNode());
            return ReplaceNodes(replacements);
        }

        /// <summary>
        /// Change the root of the Conflux graph.
        /// </summary>
        /// <returns>The new Conflux graph.</returns>
        private ConfluxGraph ChangeRoot(Node root)
        {
            if (!Nodes.Contains(root))
            {
                throw new ArgumentException($"The new root should be a node in the graph, not {root}.");
            }

            return new ConfluxGraph(Context, Nodes, Connections, root, Labels);
        }

        /// <summary>
        /// Change the root of the Conflux graph to the node labeled "start".
        /// </summary>
        /// <returns>The new Conflux graph.</returns>
        private ConfluxGraph UpdateRoot()
        {
            var root = Labels["start"];
            return ChangeRoot(root);
        }
    }
}
