using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.EventEmitters;

namespace Conflux.Schema.Serialization
{
    internal class ConfluxMainPropertyEventEmitter(IEventEmitter nextEmitter) : ChainedEventEmitter(nextEmitter)
    {
        public override void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
        {
            if (eventInfo.Source is not null &&
                ConfluxMainPropertyInfo.TryGet(eventInfo.Source.Type, out var mainProp))
            {
                eventInfo.Tag = $"{ConfluxMainPropertyEmitter.TagPrefix}{mainProp.YamlKey}:{mainProp.IsForced}";
            }

            base.Emit(eventInfo, emitter);
        }
    }
}
