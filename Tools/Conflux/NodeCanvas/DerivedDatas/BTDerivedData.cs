using System.ComponentModel;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.DerivedDatas;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.DerivedDatas
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.BehaviourTree+DerivedSerializationData")]
    internal class BTDerivedData : DerivedData
    {
        [JsonProperty("repeat")]
        [DefaultValue(false)]
        public bool Repeat { get; set; } = false;

        [JsonProperty("updateInterval")]
        [DefaultValue(0f)]
        public float UpdateInterval { get; set; } = 0f;

        public override ConfluxBTDerivedData BuildConfluxDerivedData(Graph graph) => new()
        {
            DoRepeat = Repeat,
            UpdateInterval = UpdateInterval,
        };
    }
}
