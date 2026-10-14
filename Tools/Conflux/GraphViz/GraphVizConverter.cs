using System.Collections;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Connections;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;

namespace Conflux.GraphViz
{
    internal class GraphVizConverter
    {
        public static readonly JsonSerializerSettings DefaultSerializerSettings = new()
        {
            Formatting = Formatting.Indented,
        };

        public static string ToGraphViz(GraphTemplate template)
        {
            return ToGraphViz(template.Graph);
        }

        /// <summary>
        /// Convert a NodeCanvas graph into a corresponding GraphViz
        /// language string.
        /// </summary>
        /// <param name="graph">The NodeCanvas graph.</param>
        public static string ToGraphViz(Graph graph)
        {
            var nodeIndices = new Dictionary<Node, int>();
            foreach (var node in graph.Nodes)
            {
                nodeIndices[node] = nodeIndices.Count;
            }

            var sb = new StringBuilder();

            sb.AppendLine("digraph {");
            sb.AppendLine("  node [shape=\"box\", style=\"rounded\", fontname=\"Consolas\", margin=\"0.2,0.1\"];");
            sb.AppendLine("  edge [fontname=\"Consolas\"];");
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

                var edgeLabel = connection.SourceNode.GetGraphVizOutConnectionLabel(nodeOutConnectionIndex)?.Trim();
                var edgePropertyPart = string.IsNullOrWhiteSpace(edgeLabel) ? string.Empty : $" [label={EscapeString(edgeLabel)}]";

                sb.AppendLine($"  Node{sourceIndex} -> Node{targetIndex}{edgePropertyPart};");
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
        /// Build a string representation of an object suitable for a GraphViz
        /// label.
        /// </summary>
        /// <param name="obj">The object to represent.</param>
        /// <returns>The string representation.</returns>
        public static string BuildGraphVizLabel(object? obj, bool ignoreToGraphVizLabel = false)
        {
            if (!ignoreToGraphVizLabel && obj is IGraphVizLabelable labelable)
            {
                return labelable.ToGraphVizLabel();
            }

            if (obj is null)
            {
                return "null";
            }

            if (obj.GetType().IsValueType)
            {
                return $"{obj}";
            }

            if (obj is string str)
            {
                return str;
            }

            if (obj is IEnumerable enumerable)
            {
                var label = new StringBuilder();
                label.AppendLine();

                foreach (var item in enumerable)
                {
                    var itemLabel = BuildGraphVizLabel(item);
                    label.AppendLine($"- {IndentLines(itemLabel, indentFirstLine: false)}");
                }

                return label.ToString().TrimEnd();
            }

            if (obj.GetType().IsClass)
            {
                var type = obj.GetType();
                var label = new StringBuilder();

                var name = BuildGraphVizName(obj);
                label.Append(name);

                var content = BuildGraphVizContent(obj);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    label.Append(": ").Append(content);
                }

                return label.ToString().TrimEnd();
            }

            return $"{obj}";
        }

        public static string BuildGraphVizName(object obj)
        {
            var type = obj.GetType();
            if (!type.IsClass)
            {
                throw new ConfluxException($"{nameof(obj)} should be an instance of a class, not {type}.");
            }

            var graphVizObjectAttribute = type.GetCustomAttribute<GraphVizObjectAttribute>();
            if (graphVizObjectAttribute?.Alias is not null)
            {
                return graphVizObjectAttribute.Alias;
            }

            return Regex.Replace(type.Name, @"(BT|DT|NodeCanvas)(Node|Action|Condition)$", "");
        }

        /// <summary>
        /// Build a string representation of the contents of an object.
        /// </summary>
        /// <param name="obj">The object to represent.</param>
        /// <returns>The string representation.</returns>
        public static string BuildGraphVizContent(object obj)
        {
            var type = obj.GetType();
            if (!type.IsClass)
            {
                throw new ConfluxException($"{nameof(obj)} should be an instance of a class, not {type}.");
            }

            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<GraphVizIgnoreAttribute>() is null)
                .ToImmutableList();
            var mainProp = props.SingleOrDefault(p => p?.GetCustomAttribute<GraphVizPropertyAttribute>()?.IsMainProperty == true, null);

            var label = new StringBuilder();

            if (mainProp is not null)
            {
                var propValue = mainProp.GetValue(obj);
                if (propValue is not null)
                {
                    var defaultValueAttribute = mainProp.GetCustomAttribute<DefaultValueAttribute>();
                    if (defaultValueAttribute is null || propValue != defaultValueAttribute.Value)
                    {
                        var propLabel = BuildGraphVizLabel(propValue);
                        label.Append(propLabel);
                    }
                }
            }

            label.AppendLine();

            foreach (var prop in props)
            {
                var graphVizPropertyAttribute = prop.GetCustomAttribute<GraphVizPropertyAttribute>();
                if (graphVizPropertyAttribute is not null && graphVizPropertyAttribute.IsMainProperty)
                {
                    continue;
                }

                var propValue = prop.GetValue(obj);
                if (propValue is null)
                {
                    continue;
                }

                var defaultValueAttribute = prop.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultValueAttribute is not null && Equals(propValue, defaultValueAttribute.Value))
                {
                    continue;
                }

                var propName = graphVizPropertyAttribute?.Alias is not null ?
                    graphVizPropertyAttribute.Alias :
                    prop.Name;
                var propLabel = BuildGraphVizLabel(propValue);

                label.AppendLine($"{propName}: {IndentLines(propLabel, indentFirstLine: false)}");
            }

            return label.ToString().TrimEnd();
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
