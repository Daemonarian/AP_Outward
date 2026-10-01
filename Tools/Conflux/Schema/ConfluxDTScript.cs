using Conflux.Schema.Context;
using Conflux.Schema.DerivedDatas;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    internal class ConfluxDTScript : ConfluxScript
    {
        public override string TypeName { get => "dialogue"; set { } }

        [YamlMember(Alias = "data")]
        public ConfluxDTDerivedData DTDerivedData { get; set; } = new();

        public override ConfluxDerivedData DerivedData
        {
            get => DTDerivedData;
            set => DTDerivedData = (ConfluxDTDerivedData)value;
        }

        protected override NodeCanvasGraphContext CreateContext() => new NodeCanvasDialogueTreeContext(this);
    }
}
