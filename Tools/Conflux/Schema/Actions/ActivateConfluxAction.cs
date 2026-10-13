using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("activate")]
    internal class ActivateConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "object")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference> Object { get; set; } = new();

        [YamlMember(Alias = "mode")]
        [DefaultValue(ActivateMode.Toggle)]
        public ActivateMode Mode { get; set; } = ActivateMode.Toggle;

        public override SetObjectActiveNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Object = Object.BuildBBParameter(context),
            Mode = Mode switch
            {
                ActivateMode.Deactivate => SetObjectActiveNodeCanvasAction.SetActiveMode.Deactivate,
                ActivateMode.Activate => SetObjectActiveNodeCanvasAction.SetActiveMode.Activate,
                ActivateMode.Toggle => SetObjectActiveNodeCanvasAction.SetActiveMode.Toggle,
                _ => throw new ConfluxValueException($"Unexpected activate.mode value: {Mode}"),
            },
        };

        public enum ActivateMode
        {
            Deactivate,
            Activate,
            Toggle
        }
    }
}
