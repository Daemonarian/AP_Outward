using Conflux.Schema.Context;
using Conflux.Schema.DerivedDatas;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    internal class ConfluxDTScript : ConfluxScript
    {
        [YamlMember(Alias = "data")]
        public ConfluxDTDerivedData DTDerivedData { get; set; } = new();

        public override ConfluxDerivedData DerivedData => DTDerivedData;

        protected override NodeCanvasGraphContext CreateContext() => new NodeCanvasDialogueTreeContext(this);
    }
}
