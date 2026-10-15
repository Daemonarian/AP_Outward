using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("giveQuest")]
    internal class GiveQuestConfluxAction : ConfluxAction
    {
        [YamlMember(Alias = "questObject")]
        public ConfluxBlackboardVariableReference<ConfluxUnityObjectReference>? QuestObject { get; set; }

        [YamlMember(Alias = "quest")]
        public ConfluxBlackboardVariableReference<ConfluxQuestReference>? Quest { get; set; }

        public override GiveQuestNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Quest = QuestObject?.BuildBBParameter<UnityObject>(context),
            QuestRef = Quest?.BuildBBParameter<QuestReference>(context),
        };
    }
}
