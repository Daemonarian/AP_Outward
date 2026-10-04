using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.SetObjectActive")]
    internal class SetObjectActiveAction : ActionTask
    {
        [JsonProperty("setTo")]
        [DefaultValue(SetActiveMode.Toggle)]
        public SetActiveMode Mode { get; set; } = SetActiveMode.Toggle;

        [JsonProperty("overrideAgent")]
        public BBParameter<UnityObject> Object { get; set; } = new();

        public override string GetGraphVizShortName() => "SetObjectActive";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();
            content.AppendLine(Object.ToGraphVizLabel());
            content.AppendLine($"Mode: {Mode}");
            return content.ToString().TrimEnd();
        }

        public override ActivateConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Mode = GetConfluxActivateMode(graph, Mode),
            Object = Object.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
        };

        private static ActivateConfluxAction.ActivateMode GetConfluxActivateMode(Graph graph, SetActiveMode mode) => mode switch
        {
            SetActiveMode.Deactivate => ActivateConfluxAction.ActivateMode.Deactivate,
            SetActiveMode.Activate => ActivateConfluxAction.ActivateMode.Activate,
            SetActiveMode.Toggle => ActivateConfluxAction.ActivateMode.Toggle,
            _ => throw new ConfluxValueException($"SetObjectActiveAction mode {mode} is not supported."),
        };

        [JsonConverter(typeof(StringEnumConverter))]
        public enum SetActiveMode
        {
            [EnumMember(Value = "Deactivate")]
            Deactivate,

            [EnumMember(Value = "Activate")]
            Activate,

            [EnumMember(Value = "Toggle")]
            Toggle,
        }
    }
}
