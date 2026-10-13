using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.SetDialogueInterruptable")]
    [GraphVizObject(Alias = "SetDialogueInterruptable")]
    internal class SetDialogueInterruptableNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("Interruptable")]
        [GraphVizProperty(IsMainProperty = true)]
        [DefaultValue(false)]
        public bool Interruptable { get; set; } = false;

        public override SetDialogueInterruptableConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Interruptable = Interruptable,
        };
    }
}
