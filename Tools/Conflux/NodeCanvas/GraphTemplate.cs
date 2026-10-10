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

            // post-processing to reduce number of go-tos

            foreach (var block in script.Threads.Values)
            {
                PromoteGoToStatements(block);
            }

            InlineThreads(script);

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

        /// <summary>
        /// Find blocks where every branch ends in a go-to statement to the same
        /// thread, and move the go-to statements to the end of the parent
        /// block.
        /// </summary>
        /// <param name="block">The block of statements to check.</param>
        /// <returns>Whether any changes were actually made.</returns>
        private static bool PromoteGoToStatements(ConfluxBlock block)
        {
            if (block.Statements.Count == 0)
            {
                return false;
            }

            var statement = block.Statements[^1];
            foreach (var childBlock in statement.ChildBlocks)
            {
                PromoteGoToStatements(childBlock);
            }

            var counts = new Dictionary<string, int>();
            CountTailThreadReferences(block, counts);

            var didPromote = false;
            if (counts.Count == 1)
            {
                var (label, count) = counts.Single();
                if (count > 1)
                {
                    PruneTails(block, label);
                    block.Statements.Add(new GoToConfluxStatement { Label = label });
                    didPromote = true;
                }
            }

            return didPromote;
        }

        /// <summary>
        /// Accumulate thread reference counts that occur at the tails of the
        /// given block.
        /// </summary>
        /// <param name="block">The block to search.</param>
        /// <param name="counts">The dictionary to accumulate the counts in.</param>
        private static void CountTailThreadReferences(ConfluxBlock block, Dictionary<string, int> counts)
        {
            if (block.Statements.Count == 0)
            {
                return;
            }

            var tail = block.Statements[^1];
            if (tail is GoToConfluxStatement goTo)
            {
                var label = goTo.Label;
                if (!counts.TryGetValue(label, out var count))
                {
                    count = 0;
                }

                counts[label] = count + 1;
            }

            foreach (var tailBlock in tail.ChildBlocks)
            {
                CountTailThreadReferences(tailBlock, counts);
            }
        }

        /// <summary>
        /// Remove tail go-to statements to the specified label, and append
        /// finish statements to the other tails.
        /// </summary>
        /// <param name="block">The block to search.</param>
        /// <param name="label">The go-to label.</param>
        private static void PruneTails(ConfluxBlock block, string label)
        {
            var counts = new Dictionary<string, int>();
            CountTailThreadReferences(block, counts);
            if (counts.Count == 0)
            {
                block.Statements.Add(new FinishConfluxStatement());
                return;
            }

            var tail = block.Statements[^1];
            if (tail is GoToConfluxStatement goTo && goTo.Label == label)
            {
                block.Statements.RemoveAt(block.Statements.Count - 1);
            }

            foreach (var tailBlock in tail.ChildBlocks)
            {
                PruneTails(tailBlock, label);
            }
        }

        /// <summary>
        /// Find threads which only have one go-to statement and inline them.
        /// </summary>
        /// <returns>Whether any changes were actually made.</returns>
        private static bool InlineThreads(ConfluxScript script)
        {
            var counts = new Dictionary<string, int>();
            foreach (var block in script.Threads.Values)
            {
                CountThreadReferences(block, counts);
            }

            var didInline = false;
            foreach (var (label, count) in counts)
            {
                if (count != 1)
                {
                    continue;
                }

                foreach (var block in script.Threads.Values)
                {
                    if (!script.Threads.TryGetValue(label, out var threadBlock))
                    {
                        throw new ConfluxException($"Unknown thread: {label}.");
                    }

                    InlineThread(block, label, threadBlock);
                }

                script.Threads.Remove(label);

                didInline = true;
            }

            return didInline;
        }

        /// <summary>
        /// Accumulate thread reference counts in the given block.
        /// </summary>
        /// <param name="block">The block to search.</param>
        /// <param name="counts">The dictionary to accumulate the counts in.</param>
        private static void CountThreadReferences(ConfluxBlock block, Dictionary<string, int> counts)
        {
            foreach (var statement in block.Statements)
            {
                if (statement is GoToConfluxStatement goToStatement)
                {
                    var label = goToStatement.Label;
                    if (!counts.TryGetValue(label, out var count))
                    {
                        count = 0;
                    }

                    counts[label] = count + 1;
                }

                foreach (var childBlock in statement.ChildBlocks)
                {
                    CountThreadReferences(childBlock, counts);
                }
            }
        }

        /// <summary>
        /// Replace every go-to statement to <see cref="label"/> in
        /// <see cref="block"/> with <see cref="thread"/>.
        /// </summary>
        /// <param name="block">The block to search.</param>
        /// <param name="label">The go-to label to replace.</param>
        /// <param name="thread">The replacement statements.</param>
        private static void InlineThread(ConfluxBlock block, string label, ConfluxBlock thread)
        {
            var didInline = false;
            var statements = new List<ConfluxStatement>();
            foreach (var statement in block.Statements)
            {
                if (statement is GoToConfluxStatement goToConfluxStatement &&
                    goToConfluxStatement.Label == label)
                {
                    didInline = true;
                    statements.AddRange(thread.Statements);
                }
                else
                {
                    statements.Add(statement);

                    foreach (var childBlock in statement.ChildBlocks)
                    {
                        InlineThread(childBlock, label, thread);
                    }
                }
            }

            if (didInline)
            {
                block.Statements = statements;
            }
        }
    }
}
