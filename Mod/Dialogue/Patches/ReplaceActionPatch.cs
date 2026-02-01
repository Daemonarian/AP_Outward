using System.Collections.Generic;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class ReplaceActionPatch : IDialoguePatch
    {
        public IReadOnlyList<IActionPatch> ActionPatches { get; set; } = new IActionPatch[] { };

        public IActionPatch ActionPatch { set => ActionPatches = new[] { value }; }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            foreach (var node in context.Tree.allNodes)
            {
                if (node is ActionNode actionNode)
                {
                    if (actionNode.action is ActionList actionList)
                    {
                        if (actionList.actions != null)
                        {
                            for (var i = 0; i < actionList.actions.Count; i++)
                            {
                                var action = actionList.actions[i];
                                if (action != null)
                                {
                                    foreach (var actionPatch in ActionPatches)
                                    {
                                        var newAction = actionPatch.BuildAction(context, action);
                                        if (newAction != null)
                                        {
                                            actionList.actions[i] = newAction;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var action = actionNode.action;
                        if (action != null)
                        {
                            foreach (var actionPatch in ActionPatches)
                            {
                                var newAction = actionPatch.BuildAction(context, action);
                                if (newAction != null)
                                {
                                    actionNode.action = newAction;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
