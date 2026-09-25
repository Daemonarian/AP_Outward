using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Actions
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
