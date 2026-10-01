using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("sendLocationCheck")]
    internal class SendLocationCheckConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "location")]
        public ConfluxLocationReference Location { get; set; } = new();

        public override Action_CompleteLocationCheck BuildAction(NodeCanvasGraphContext context) => new()
        {
            Location = Location.BuildNodeCanvasObject(context),
        };
    }
}
