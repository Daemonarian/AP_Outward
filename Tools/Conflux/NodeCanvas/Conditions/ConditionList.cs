using System.ComponentModel;
using System.Runtime.Serialization;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Framework.ConditionList")]
    internal class ConditionList : NodeCanvasCondition
    {
        [JsonProperty("checkMode")]
        [GraphVizIgnore]
        [DefaultValue(ConditionsCheckMode.AllTrueRequired)]
        public ConditionsCheckMode CheckMode { get; set; } = ConditionsCheckMode.AllTrueRequired;

        [JsonProperty("conditions")]
        [GraphVizProperty(IsMainProperty = true)]
        public List<NodeCanvasCondition> Conditions { get; set; } = [];

        public override string GetGraphVizShortName() => CheckMode switch
        {
            ConditionsCheckMode.AllTrueRequired => "And",
            ConditionsCheckMode.AnyTrueSuffice => "Or",
            _ => CheckMode.ToString(),
        };

        protected override ConfluxCondition BuildSubConfluxCondition(Graph graph) => CheckMode switch
        {
            ConditionsCheckMode.AllTrueRequired => new AndConfluxCondition
            {
                Conditions = [.. Conditions.Select(c => c.BuildConfluxCondition(graph))],
            },
            ConditionsCheckMode.AnyTrueSuffice => new OrConfluxCondition
            {
                Conditions = [.. Conditions.Select(c => c.BuildConfluxCondition(graph))],
            },
            _ => throw new ConfluxValueException($"ConditionList CheckMode {CheckMode} is not supported."),
        };

        [JsonConverter(typeof(StringEnumConverter))]
        internal enum ConditionsCheckMode
        {
            [EnumMember(Value = "AllTrueRequired")]
            AllTrueRequired,

            [EnumMember(Value = "AnyTrueSuffice")]
            AnyTrueSuffice
        }
    }
}
