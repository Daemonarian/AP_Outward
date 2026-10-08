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
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? QuestObject { get; set; }

        [YamlMember(Alias = "quest")]
        public ConfluxBlackboardVariableReference<QuestReference, ConfluxQuestReference>? Quest { get; set; }

        public override GiveQuestNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Quest = QuestObject?.BuildBBParameter(context),
            QuestRef = Quest?.BuildBBParameter(context),
        };
    }
}
