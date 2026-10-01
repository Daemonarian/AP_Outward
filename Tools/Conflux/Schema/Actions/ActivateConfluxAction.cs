using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
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
        public ActivateMode Mode { get; set; } = ActivateMode.Toggle;

        public override SetObjectActiveAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Object = Object.BuildBBParameter(context),
            Mode = Mode switch
            {
                ActivateMode.Deactivate => SetObjectActiveAction.SetActiveMode.Deactivate,
                ActivateMode.Activate => SetObjectActiveAction.SetActiveMode.Activate,
                ActivateMode.Toggle => SetObjectActiveAction.SetActiveMode.Toggle,
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
