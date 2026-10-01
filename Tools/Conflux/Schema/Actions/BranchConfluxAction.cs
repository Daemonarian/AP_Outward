using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.References;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("branch")]
    internal class BranchConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "dialogue")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference> DialogueStarter { get; set; } = new();

        [YamlMember(Alias = "wait")]
        public bool DoWait { get; set; } = true;

        public override BranchDialogue BuildAction(NodeCanvasGraphContext context) => new()
        {
            DialogueStarter = DialogueStarter.BuildBBParameter(context),
            WaitActionFinish = DoWait,
        };
    }
}
