using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("playAudio")]
    internal class PlayAudioConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "audioClip")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? AudioClip { get; set; }

        [YamlMember(Alias = "volume")]
        [DefaultValue(1f)]
        public float Volume { get; set; } = 1f;

        [YamlMember(Alias = "wait")]
        [DefaultValue(false)]
        public bool WaitActionFinish { get; set; } = false;

        public override PlayAudioNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            AudioClip = AudioClip?.BuildBBParameter(context),
            Volume = Volume,
            WaitActionFinish = WaitActionFinish,
        };
    }
}
