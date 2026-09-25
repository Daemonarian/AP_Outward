using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Conditions
{
    [ConfluxDerived("or")]
    internal class OrConfluxCondition : ConfluxCondition
    {
        [YamlMember(Alias = "conditions")]
        [ConfluxMainProperty]
        public List<ConfluxCondition> Conditions { get; set; } = [];

        public override ConditionTask BuildCondition(INodeCanvasGraphContext context)
        {
            var conditions = Conditions.Select(c => c.BuildCondition(context)).ToList();
            return new ConditionList
            {
                CheckMode = ConditionList.ConditionsCheckMode.AnyTrueSuffice,
                Conditions = conditions,
            };
        }
    }
}
