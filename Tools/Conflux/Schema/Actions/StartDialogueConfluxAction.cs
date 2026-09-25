using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("startDialogue")]
    internal class StartDialogueConfluxAction : ConfluxAction
    {
        [YamlMember(Alias = "name")]
        [ConfluxMainProperty]
        public string Name { get; set; } = string.Empty;

        public override ActionTask BuildAction(INodeCanvasGraphContext context)
        {
            var dialogueStarter = context.BuildBBParameter<int>(Name);
            return new BranchDialogue
            {
                DialogueStarter = dialogueStarter,
            };
        }
    }
}
