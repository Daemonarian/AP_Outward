using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.EventEmitters;

namespace Conflux.Schema.Serialization
{
    public class ConfluxPolymorphicEventEmitter(IEventEmitter nextEmitter) : ChainedEventEmitter(nextEmitter)
    {
        public override void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
        {
            if (eventInfo.Source is not null &&
                ConfluxDerivedInfo.TryGet(eventInfo.Source.Type, out var derivedInfo))
            {
                eventInfo.IsImplicit = true;
                nextEmitter.Emit(new MappingStartEventInfo(eventInfo.Source) { IsImplicit = true }, emitter);
                nextEmitter.Emit(new ScalarEventInfo(new ObjectDescriptor(derivedInfo.Key, typeof(string), typeof(string)))
                {
                    IsPlainImplicit = true,
                    Style = ScalarStyle.Plain
                }, emitter);
            }

            base.Emit(eventInfo, emitter);
        }

        public override void Emit(MappingEndEventInfo eventInfo, IEmitter emitter)
        {
            base.Emit(eventInfo, emitter);

            if (eventInfo.Source is not null &&
                ConfluxDerivedInfo.TryGet(eventInfo.Source.Type, out _))
            {
                nextEmitter.Emit(new MappingEndEventInfo(eventInfo.Source), emitter);
            }
        }
    }
}