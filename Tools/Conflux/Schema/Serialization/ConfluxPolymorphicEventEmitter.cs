using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.EventEmitters;

namespace Conflux.Schema.Serialization
{
    public class ConfluxPolymorphicEventEmitter(IEventEmitter nextEmitter) : ChainedEventEmitter(nextEmitter)
    {
        public override void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
        {
            if (TryGetPolymorphicInfo(eventInfo.Source, out var polymorphicInfo))
            {
                eventInfo.IsImplicit = true;
                nextEmitter.Emit(new MappingStartEventInfo(eventInfo.Source) { IsImplicit = true }, emitter);
                nextEmitter.Emit(new ScalarEventInfo(new ObjectDescriptor(polymorphicInfo.Key, typeof(string), typeof(string)))
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

            if (TryGetPolymorphicInfo(eventInfo.Source, out var _))
            {
                nextEmitter.Emit(new MappingEndEventInfo(eventInfo.Source), emitter);
            }
        }

        private bool TryGetPolymorphicInfo(IObjectDescriptor source, out ConfluxPolymorphicLookup.Relation info)
        {
            if (source.Value is not null &&
                ConfluxPolymorphicLookup.ByBaseType.TryGetValue(source.StaticType, out var polymorphicLookup) &&
                polymorphicLookup.ByDerivedType.TryGetValue(source.Type, out var polymorphicInfo))
            {
                info = polymorphicInfo;
                return true;
            }

            info = new(typeof(Type), typeof(Type), string.Empty);
            return false;
        }
    }
}