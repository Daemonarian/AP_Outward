using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("removeQuestEvent")]
    internal class RemoveQuestEventConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "questEvent")]
        public ConfluxQuestEventReference? QuestEvent { get; set; }

        [YamlMember(Alias = "removeAll")]
        [DefaultValue(false)]
        public bool RemoveAllStack { get; set; } = false;

        [YamlMember(Alias = "amount")]
        [DefaultValue(1)]
        public int StackDecreaseAmount { get; set; } = 1;

        public override RemoveQuestEventNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            QuestEventRef = QuestEvent?.BuildNodeCanvasObject(context),
            RemoveAllStack = RemoveAllStack,
            StackDecreaseAmount = StackDecreaseAmount,
        };
    }
}
