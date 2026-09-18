using System.Text;
using System.Text.Json;
using NodeCanvas.Tool.Schema;
using NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool
{
    internal class GraphVizConverter
    {
        public static readonly JsonSerializerOptions DefaultSerializerOptions = new()
        {
            WriteIndented = true,
            IndentCharacter = ' ',
            IndentSize = 2,
        };

        public static string ToGraphViz(GraphReplacementTemplate template)
        {
            return ToGraphViz(template.Graph);
        }

        /// <summary>
        /// Convert a NodeCanvas graph into a corresponding GraphViz
        /// language string.
        /// </summary>
        /// <param name="graph">The NodeCanvas graph.</param>
        public static string ToGraphViz(GraphSerializationData graph)
        {
            var nodeIndices = new Dictionary<Node, int>();
            foreach (var node in graph.Nodes)
            {
                nodeIndices[node] = nodeIndices.Count;
            }

            var sb = new StringBuilder();

            sb.AppendLine("digraph {");
            sb.AppendLine("  node [shape=\"box\", style=\"rounded\", fontname=\"Consolas\", margin=\"0.2,0.1\"];");
            sb.AppendLine($"  Start [shape=doublecircle, label=\"Start\"];");

            foreach (var node in graph.Nodes)
            {
                var nodeIndex = nodeIndices[node];
                var nodeLabel = node.ToGraphVizLabel();
                sb.AppendLine($"  Node{nodeIndex} [label={EscapeString(nodeLabel)}];");
            }

            if (graph.Nodes.Count > 0)
            {
                sb.AppendLine($"  Start -> Node0;");
            }

            var connectionsBySourceNode = new Dictionary<Node, List<Connection>>();
            foreach (var connection in graph.Connections)
            {
                if (connection.SourceNode is null)
                {
                    Console.Error.WriteLine("Connection has no source node.");
                    continue;
                }

                if (connection.TargetNode is null)
                {
                    Console.Error.WriteLine("Connection has no target node.");
                    continue;
                }

                if (!connectionsBySourceNode.TryGetValue(connection.SourceNode, out var nodeOutConnections))
                {
                    nodeOutConnections = [];
                    connectionsBySourceNode[connection.SourceNode] = nodeOutConnections;
                }

                var nodeOutConnectionIndex = nodeOutConnections.Count;
                nodeOutConnections.Add(connection);

                var sourceIndex = nodeIndices[connection.SourceNode];
                var targetIndex = nodeIndices[connection.TargetNode];

                var edgeLabel = "";
                if (connection.SourceNode is ConditionNode)
                {
                    edgeLabel = nodeOutConnectionIndex == 0 ? "yes" : (nodeOutConnectionIndex == 1 ? "no" : "");
                }
                else if (connection.SourceNode is MultipleChoiceNode)
                {
                    edgeLabel = $"{nodeOutConnectionIndex}";
                }

                sb.AppendLine($"  Node{sourceIndex} -> Node{targetIndex} [label=\"{edgeLabel}\"];");
            }

            foreach (var node in graph.Nodes)
            {
                if (node is GoToNode goToNode)
                {
                    if (goToNode.TargetNode is not null)
                    {
                        var sourceIndex = nodeIndices[node];
                        var targetIndex = nodeIndices[goToNode.TargetNode];
                        sb.AppendLine($"  Node{sourceIndex} -> Node{targetIndex};");
                    }
                }
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        /// <summary>
        /// Safely escape a string for use in GraphViz.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        private static string EscapeString(string label)
        {
            label = label.Replace("\\", "\\\\");
            label = label.Replace("\"", "\\\"");
            label = label.Replace("\r", "");
            label = label.Replace("\n", "\\l");
            label = label.Replace("\t", "\\t");
            return $"\"{label}\\l\"";
        }

        /// <summary>
        /// Wrap words by inserting new-line characters.
        /// </summary>
        /// <param name="text">The text to wrap.</param>
        /// <param name="maxLineLength">The maximum length of a line.</param>
        /// <returns>The wrapped version of the text.</returns>
        public static string WordWrap(string text, int maxLineLength = 50)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            var wrappedText = new StringBuilder();
            var currentLine = new StringBuilder();
            var words = text.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (currentLine.Length > 0 && currentLine.Length + word.Length + 1 >= maxLineLength)
                {
                    wrappedText.AppendLine(currentLine.ToString().TrimEnd());
                    currentLine.Clear();
                }

                currentLine.Append(word).Append(' ');
            }

            if (currentLine.Length > 0)
            {
                wrappedText.AppendLine(currentLine.ToString().TrimEnd());
            }

            return wrappedText.ToString();
        }

        /// <summary>
        /// Indents each line of some text.
        /// </summary>
        /// <param name="text">The text to indent.</param>
        /// <param name="indentAmount">The number of indentations to apply.</param>
        /// <param name="indentCharacter">The character to use for indenting.</param>
        /// <param name="indentCharacterCount">How many of the character count as a single indent.</param>
        /// <returns>The indented version of the string.</returns>
        public static string IndentLines(string text, int indentAmount = 1, bool indentFirstLine = true, char indentCharacter = ' ', int indentCharacterCount = 2)
        {
            var indentedText = new StringBuilder();
            var isFirstLine = true;
            foreach (var line in text.Split('\n'))
            {
                var doIndent = true;
                if (isFirstLine)
                {
                    isFirstLine = false;
                    if (!indentFirstLine)
                    {
                        doIndent = false;
                    }
                }

                if (!doIndent)
                {
                    indentedText.AppendLine(line.TrimEnd());
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    indentedText.AppendLine();
                }
                else
                {
                    for (var i = 0; i < indentAmount; i++)
                    {
                        for (var j = 0; j < indentCharacterCount; j++)
                        {
                            indentedText.Append(indentCharacter);
                        }
                    }

                    indentedText.AppendLine(line.TrimEnd());
                }
            }

            return indentedText.ToString().TrimEnd();
        }
    }
}
