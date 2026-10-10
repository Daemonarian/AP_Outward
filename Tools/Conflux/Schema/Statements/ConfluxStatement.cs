using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxStatement
    {
        [YamlIgnore]
        public virtual IReadOnlyList<ConfluxBlock> ChildBlocks
        {
            get => [];

            set
            {
                if (value.Count != 0)
                {
                    throw new ConfluxValueException($"Too many child blocks. Zero were expected, not {value.Count}.");
                }
            }
        }

        public abstract ConfluxGraph BuildGraph(NodeCanvasGraphContext context);
    }
}
