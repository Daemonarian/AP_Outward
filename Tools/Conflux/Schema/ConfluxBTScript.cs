using Conflux.Schema.Context;
using Conflux.Schema.DerivedDatas;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    internal class ConfluxBTScript : ConfluxScript
    {
        [YamlMember(Alias = "data")]
        public ConfluxBTDerivedData BTDerivedData { get; set; } = new();

        public override ConfluxDerivedData DerivedData => BTDerivedData;

        protected override NodeCanvasGraphContext CreateContext() => new NodeCanvasBehaviourTreeContext(this);
    }
}
