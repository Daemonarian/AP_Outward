using System.ComponentModel;
using System.Runtime.Serialization;
using Conflux.GraphViz;
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
    [GraphVizObject(Alias = "SetObjectActive")]
    internal class SetObjectActiveNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("setTo")]
        [DefaultValue(SetActiveMode.Toggle)]
        public SetActiveMode Mode { get; set; } = SetActiveMode.Toggle;

        [JsonProperty("overrideAgent")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<UnityObject> Object { get; set; } = new();

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
