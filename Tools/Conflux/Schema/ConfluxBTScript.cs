using Conflux.Schema.Context;
using Conflux.Schema.DerivedDatas;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    internal class ConfluxBTScript : ConfluxScript
    {
        public override string TypeName { get => "behaviour"; set { } }

        [YamlMember(Alias = "data")]
        public ConfluxBTDerivedData BTDerivedData { get; set; } = new();

        public override ConfluxDerivedData DerivedData
        {
            get => BTDerivedData;
            set => BTDerivedData = (ConfluxBTDerivedData)value;
        }
        protected override NodeCanvasGraphContext CreateContext() => new NodeCanvasBehaviourTreeContext(this);
    }
}
