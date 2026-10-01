using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.SetObjectActive")]
    internal class SetObjectActiveAction : ActionTask
    {
        [JsonProperty("setTo")]
        public SetActiveMode Mode { get; set; } = SetActiveMode.Deactivate;

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

        [TypeConverter(typeof(StringEnumConverter))]
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
