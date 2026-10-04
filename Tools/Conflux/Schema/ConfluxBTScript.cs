using Conflux.Schema.Context;
using Conflux.Schema.DerivedDatas;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    internal class ConfluxBTScript : ConfluxScript
    {
        public override string TypeName { get => "behaviour"; set { } }

        [YamlIgnore]
        public ConfluxBTDerivedData BTDerivedData { get; set; } = new();

        [YamlMember(Alias = "data")]
        public ConfluxBTDerivedData? BTDerivedDataProxy
        {
            get => BTDerivedData.IsEmpty() ? null : BTDerivedData;
            set => BTDerivedData = value ?? new();
        }

        public override ConfluxDerivedData DerivedData
        {
            get => BTDerivedData;
            set => BTDerivedData = (ConfluxBTDerivedData)value;
        }
        protected override NodeCanvasGraphContext CreateContext() => new NodeCanvasBehaviourTreeContext(this);
    }
}
