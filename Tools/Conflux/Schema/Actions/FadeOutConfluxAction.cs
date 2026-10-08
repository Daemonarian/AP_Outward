using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("fadeOut")]
    internal class FadeOutConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "duration")]
        [DefaultValue(1f)]
        public float Duration { get; set; } = 1f;

        public override FadeOutNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            FadeTime = Duration,
        };
    }
}
