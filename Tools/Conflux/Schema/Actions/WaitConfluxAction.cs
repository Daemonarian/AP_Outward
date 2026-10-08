using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("wait")]
    internal class WaitConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "duration")]
        [DefaultValue(1f)]
        public ConfluxBlackboardVariableReference<float>? WaitTime { get; set; } = 1f;

        [YamlMember(Alias = "finishState")]
        [DefaultValue(FinishNode.CompactStatus.Success)]
        public FinishNode.CompactStatus FinishStatus { get; set; } = FinishNode.CompactStatus.Success;

        public override WaitNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            WaitTime = WaitTime?.BuildBBParameter(context),
            FinishState = FinishStatus,
        };
    }
}
