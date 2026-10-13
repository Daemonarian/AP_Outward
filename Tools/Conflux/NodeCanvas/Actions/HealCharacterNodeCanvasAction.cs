using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.HealCharacter")]
    [GraphVizObject(Alias = "Heal")]
    internal class HealCharacterNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("FullHealth")]
        [DefaultValue(false)]
        public BBParameter<bool> FullHealth { get; set; } = false;

        [JsonProperty("HealAmount")]
        [DefaultValue(0f)]
        public BBParameter<float> HealAmount { get; set; } = 0f;

        [JsonProperty("Target")]
        public BBParameter<UnityObject>? Target { get; set; }

        [JsonProperty("m_targetUID")]
        public string? TargetUID { get; set; }

        public override HealCharacterConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            FullHealth = FullHealth.BuildConfluxBlackboardVariableReference(graph),
            HealAmount = HealAmount.BuildConfluxBlackboardVariableReference(graph),
            Target = Target?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            TargetUID = TargetUID,
        };
    }
}
