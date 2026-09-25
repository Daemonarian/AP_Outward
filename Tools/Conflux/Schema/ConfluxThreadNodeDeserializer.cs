using Conflux.Schema.Statements;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    internal class ConfluxThreadNodeDeserializer : INodeDeserializer
    {
        public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value, ObjectDeserializer rootDeserializer)
        {
            // Only intercept when YamlDotNet asks for a ConfluxThread
            if (expectedType != typeof(ConfluxBlock))
            {
                value = null;
                return false;
            }

            // Tell YamlDotNet to deserialize the current YAML sequence as a List<ConfluxNode>
            var parsedList = (List<ConfluxStatement>?)nestedObjectDeserializer(reader, typeof(List<ConfluxStatement>));

            // Wrap the list in our custom class
            value = new ConfluxBlock { Statements = parsedList ?? [] };
            return true;
        }
    }
}
