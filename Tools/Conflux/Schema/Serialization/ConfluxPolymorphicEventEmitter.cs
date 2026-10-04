using Conflux.Schema.Exceptions;
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
                ConfluxPolymorphicInfo.TryGet(eventInfo.Source.StaticType, out var polymorphicInfo))
            {
                if (!ConfluxDerivedInfo.TryGet(eventInfo.Source.Type, out var derivedInfo))
                {
                    throw new ConfluxException($"Attempted to serialize a Conflux polymorphic sub-type {eventInfo.Source.Type.Name} of {polymorphicInfo.Type.Name}, but no Conflux derived info was found.");
                }

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
                ConfluxPolymorphicInfo.TryGet(eventInfo.Source.StaticType, out _))
            {
                nextEmitter.Emit(new MappingEndEventInfo(eventInfo.Source), emitter);
            }
        }
    }
}