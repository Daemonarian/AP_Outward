using Conflux.NodeCanvas.DerivedDatas;
using Conflux.Schema.Context;
using YamlDotNet.Serialization;

namespace Conflux.Schema.DerivedDatas
{
    internal class ConfluxBTDerivedData : ConfluxDerivedData
    {
        [YamlMember(Alias = "repeat")]
        public bool DoRepeat { get; set; } = false;

        [YamlMember(Alias = "updateInterval")]
        public float UpdateInterval { get; set; } = 0f;

        public override BTDerivedData BuildDerivedData(NodeCanvasGraphContext context) => new()
        {
            Repeat = DoRepeat,
            UpdateInterval = UpdateInterval,
        };
    }
}
