using Conflux.NodeCanvas;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxStatementReference : INodeCanvasObjectBuilder<Statement>
    {
        [YamlMember(Alias = "meta")]
        public string Meta { get; set; } = string.Empty;

        [YamlMember(Alias = "text")]
        public string Text { get; set; } = string.Empty;

        public Statement BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (string.IsNullOrWhiteSpace(Meta))
            {
                throw new ConfluxValueException("The value statement.meta must be specified.");
            }

            if (string.IsNullOrWhiteSpace(Text))
            {
                throw new ConfluxValueException("The value statement.text must be specified.");
            }

            return new()
            {
                Meta = Meta,
                Text = Text,
            };
        }
    }
}
