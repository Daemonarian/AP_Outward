using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("sendLocationCheck")]
    internal class SendLocationCheckConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "location")]
        public string LocationKey { get; set; } = string.Empty;

        public override ActionTask BuildAction(INodeCanvasGraphContext context)
        {
            return new Action_CompleteLocationCheck
            {
                Location = new()
                {
                    Key = LocationKey,
                },
            };
        }
    }
}
