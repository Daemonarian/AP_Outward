using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Conflux.Outward;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.AreaSwitchPlayers")]
    [GraphVizObject(Alias = "AreaSwitchPlayers")]
    internal class AreaSwitchPlayersNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("Area")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<OutwardArea.AreaEnum>? Area { get; set; }

        [JsonProperty("SpawnPoint")]
        [DefaultValue(0)]
        public BBParameter<int> SpawnPoint { get; set; } = 0;

        public override SwitchAreaConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Area = Area?.BuildConfluxBlackboardVariableReference(graph),
            SpawnPoint = SpawnPoint.BuildConfluxBlackboardVariableReference(graph),
        };
    }
}
