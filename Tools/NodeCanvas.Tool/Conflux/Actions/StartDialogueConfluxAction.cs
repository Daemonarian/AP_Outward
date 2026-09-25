using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Actions
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
