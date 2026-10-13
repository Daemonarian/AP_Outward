using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("setDialogueInterruptable")]
    internal class SetDialogueInterruptableConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "interruptable")]
        [DefaultValue(false)]
        public bool Interruptable { get; set; } = false;

        public override SetDialogueInterruptableNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Interruptable = Interruptable,
        };
    }
}
