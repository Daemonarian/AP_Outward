using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.Outward;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("switchArea")]
    internal class SwitchAreaConfluxAction : ConfluxAction
    {
        [YamlMember(Alias = "area")]
        public ConfluxBlackboardVariableReference<OutwardArea.AreaEnum>? Area { get; set; }

        [YamlMember(Alias = "spawnPoint")]
        [DefaultValue(0)]
        public ConfluxBlackboardVariableReference<int> SpawnPoint { get; set; } = 0;

        public override AreaSwitchPlayersNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Area = Area?.BuildBBParameter(context),
            SpawnPoint = SpawnPoint.BuildBBParameter(context),
        };
    }
}
