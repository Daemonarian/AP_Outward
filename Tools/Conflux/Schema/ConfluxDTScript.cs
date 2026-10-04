using Conflux.Schema.Context;
using Conflux.Schema.DerivedDatas;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    internal class ConfluxDTScript : ConfluxScript
    {
        public override string TypeName { get => "dialogue"; set { } }

        [YamlIgnore]
        public ConfluxDTDerivedData DTDerivedData { get; set; } = new();

        [YamlMember(Alias = "data")]
        public ConfluxDTDerivedData? DTDerivedDataProxy
        {
            get => DTDerivedData.IsEmpty() ? null : DTDerivedData;
            set => DTDerivedData = value ?? new();
        }

        public override ConfluxDerivedData DerivedData
        {
            get => DTDerivedData;
            set => DTDerivedData = (ConfluxDTDerivedData)value;
        }

        protected override NodeCanvasGraphContext CreateContext() => new NodeCanvasDialogueTreeContext(this);
    }
}
