using System.Collections.Frozen;
using System.Reflection;

namespace Conflux.Schema.Serialization
{
    internal static class ConfluxPolymorphicLookup
    {
        public static FrozenDictionary<Type, BaseRelations> ByBaseType => _records.Value;

        private static readonly Lazy<FrozenDictionary<Type, BaseRelations>> _records = new(GetKeyTypeMappings);

        private static FrozenDictionary<Type, BaseRelations> GetKeyTypeMappings()
        {
            var allBaseRelations = new List<BaseRelations>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var confluxPolymorphicAttribute = type.GetCustomAttribute<ConfluxPolymorphicAttribute>();
                if (confluxPolymorphicAttribute is null)
                {
                    continue;
                }

                var relations = new List<Relation>();
                foreach (var subType in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (!subType.IsSubclassOf(type))
                    {
                        continue;
                    }

                    var confluxDerivedAttribute = subType.GetCustomAttribute<ConfluxDerivedAttribute>();
                    if (confluxDerivedAttribute is null)
                    {
                        continue;
                    }

                    var key = confluxDerivedAttribute.Key;
                    var relation = new Relation(type, subType, key);
                    relations.Add(relation);
                }

                var baseRelations = new BaseRelations(
                    BaseType: type,
                    ByDerivedType: relations.ToDictionary(r => r.DerivedType).ToFrozenDictionary(),
                    ByKey: relations.ToDictionary(r => r.Key).ToFrozenDictionary());
                allBaseRelations.Add(baseRelations);
            }

            return allBaseRelations.ToDictionary(r => r.BaseType).ToFrozenDictionary();
        }

        public record Relation(Type BaseType, Type DerivedType, string Key);

        public record BaseRelations(Type BaseType, FrozenDictionary<Type, Relation> ByDerivedType, FrozenDictionary<string, Relation> ByKey);
    }
}
