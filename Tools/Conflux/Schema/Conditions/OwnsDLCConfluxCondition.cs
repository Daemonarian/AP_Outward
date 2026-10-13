using Conflux.NodeCanvas.Conditions;
using Conflux.Outward;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("ownsDLC")]
    internal class OwnsDLCConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "dlc")]
        public ConfluxBlackboardVariableReference<OutwardStore.DLCs>? DLC { get; set; }

        public override OwnsDLCNodeCanvasCondition BuildCondition(NodeCanvasGraphContext context) => new()
        {
            DLC = DLC?.BuildBBParameter(context),
        };
    }
}
