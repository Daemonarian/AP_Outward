using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.DerivedDatas
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.BehaviourTree+DerivedSerializationData")]
    internal class BTDerivedData : DerivedData
    {
        [JsonProperty("repeat")]
        public bool Repeat { get; set; } = false;

        [JsonProperty("updateInterval")]
        public float UpdateInterval { get; set; } = 0f;
    }
}
