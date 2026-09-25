using System.Text;
using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    [NodeCanvasType("NodeCanvas.Framework.ConditionList")]
    internal class ConditionList : ConditionTask
    {
        [JsonProperty("checkMode")]
        public ConditionsCheckMode CheckMode { get; set; } = ConditionsCheckMode.AllTrueRequired;

        [JsonProperty("conditions")]
        public List<ConditionTask> Conditions { get; set; } = [];

        public override string GetGraphVizShortName() => CheckMode switch
        {
            ConditionsCheckMode.AllTrueRequired => "And",
            ConditionsCheckMode.AnyTrueSuffice => "Or",
            _ => CheckMode.ToString(),
        };

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            content.AppendLine();

            foreach (var condition in Conditions)
            {
                var conditionLabel = condition.ToGraphVizLabel();
                conditionLabel = GraphVizConverter.IndentLines(conditionLabel, indentFirstLine: false);
                content.AppendLine($"- {conditionLabel}");
            }

            return content.ToString().TrimEnd();
        }

        internal enum ConditionsCheckMode
        {
            AllTrueRequired,
            AnyTrueSuffice
        }
    }
}
