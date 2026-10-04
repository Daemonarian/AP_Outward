using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Conflux.Schema.Exceptions;

namespace Conflux.Schema.Serialization
{
    /// <summary>
    /// Represents information about a Conflux polymorphic base type.
    /// </summary>
    internal class ConfluxPolymorphicInfo
    {
        /// <summary>
        /// A cache of Conflux polymorphic base types.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ConfluxPolymorphicInfo?> _cache = new();

        /// <summary>
        /// Try to get Conflux polymorphic information about a base type.
        /// </summary>
        /// <param name="type">The base type.</param>
        /// <param name="info">The Conflux polymorphic information.</param>
        /// <returns>Whether or not Conflux polymorphic information was found.</returns>
        public static bool TryGet(Type type, [NotNullWhen(true)] out ConfluxPolymorphicInfo? info)
        {
            info = _cache.GetOrAdd(type, Create);
            return info is not null;
        }

        /// <summary>
        /// Does the actual work to find the Conflux polymorphic information for a base type.
        /// </summary>
        /// <param name="type">The base type.</param>
        /// <returns>The Conflux polymorphic information, if it exists. Otherwise, null.</returns>
        private static ConfluxPolymorphicInfo? Create(Type type)
        {
            var attribute = type.GetCustomAttribute<ConfluxPolymorphicAttribute>();
            if (attribute is null)
            {
                return null;
            }

            return new ConfluxPolymorphicInfo(type, attribute);
        }

        /// <summary>
        /// The polymorphic base type.
        /// </summary>
        public Type Type { get; init; }

        /// <summary>
        /// The <see cref="ConfluxPolymorphicAttribute"/> of the polymorphic base type.
        /// </summary>
        public ConfluxPolymorphicAttribute Attribute { get; init; }

        private ConfluxPolymorphicInfo(Type type, ConfluxPolymorphicAttribute attribute)
        {
            Type = type;
            Attribute = attribute;

            _derivedInfos = new(GetDerivedInfos);
            _derivedInfosByKey = new(GetDerivedInfosByKey);
        }

        /// <summary>
        /// The derived types that derive from this polymorphic base type.
        /// </summary>
        public FrozenSet<ConfluxDerivedInfo> DerivedInfos => _derivedInfos.Value;

        /// <summary>
        /// Try to get information about a Conflux derived type by its serialization key.
        /// </summary>
        /// <param name="key">The serialization key of the derived type.</param>
        /// <param name="info">The information about the derived type.</param>
        /// <returns>Whether or not a derived type was found.</returns>
        public bool TryGetDerivedInfo(string key, [NotNullWhen(true)] out ConfluxDerivedInfo? info)
        {
            return _derivedInfosByKey.Value.TryGetValue(key, out info);
        }

        /// <summary>
        /// Lazy evaluation for <see cref="DerivedInfos"/>.
        /// </summary>
        private readonly Lazy<FrozenSet<ConfluxDerivedInfo>> _derivedInfos;

        /// <summary>
        /// Lazy evaluation for <see cref="DerivedInfosByKey"/>.
        /// </summary>
        private readonly Lazy<FrozenDictionary<string, ConfluxDerivedInfo>> _derivedInfosByKey;

        /// <summary>
        /// Does the actual work to find information about the derived types.
        /// </summary>
        /// <returns>A collection of infos for derived types associated with this base class.</returns>
        private FrozenSet<ConfluxDerivedInfo> GetDerivedInfos()
        {
            var derivedInfos = new HashSet<ConfluxDerivedInfo>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDynamic)
                {
                    continue;
                }

                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    types = [.. e.Types.OfType<Type>()];
                }

                foreach (var type in types)
                {
                    if (!type.IsSubclassOf(Type))
                    {
                        continue;
                    }

                    if (!ConfluxDerivedInfo.TryGet(type, out var info))
                    {
                        continue;
                    }

                    derivedInfos.Add(info);
                }
            }

            return derivedInfos.ToFrozenSet();
        }

        /// <summary>
        /// Does the actual work to group derived infos by their keys.
        /// </summary>
        /// <returns>A mapping for keys to derived types.</returns>
        private FrozenDictionary<string, ConfluxDerivedInfo> GetDerivedInfosByKey()
        {
            var dict = new Dictionary<string, ConfluxDerivedInfo>();
            foreach (var di in DerivedInfos)
            {
                if (dict.TryGetValue(di.Key, out var existing))
                {
                    throw new ConfluxException($"Duplicate Conflux polymorphic key \"{di.Key}\" found for base type {Type.Name}. It is used by both {existing.Type.Name} and {di.Type.Name}.");
                }

                dict[di.Key] = di;
            }

            return dict.ToFrozenDictionary();
        }
    }
}
