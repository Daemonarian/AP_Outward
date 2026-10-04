using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Conflux.Schema.Exceptions;

namespace Conflux.Schema.Serialization
{
    /// <summary>
    /// Information about a Conflux derived type.
    /// </summary>
    internal class ConfluxDerivedInfo
    {
        /// <summary>
        /// A cache of Conflux derived type infos.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ConfluxDerivedInfo?> _cache = new();

        /// <summary>
        /// Try to get information about a Conflux derived type.
        /// </summary>
        /// <param name="type">The derived type.</param>
        /// <param name="info">Information about Conflux derived type.</param>
        /// <returns>Whether or not Conflux derived info was found.</returns>
        public static bool TryGet(Type type, [NotNullWhen(true)] out ConfluxDerivedInfo? info)
        {
            info = _cache.GetOrAdd(type, Create);
            return info is not null;
        }

        /// <summary>
        /// Does the actual work to find information about a Conflux derived type.
        /// </summary>
        /// <param name="type">The derived type.</param>
        /// <returns>Information about the conflux derived type, if found; otherwise, null.</returns>
        private static ConfluxDerivedInfo? Create(Type type)
        {
            var attribute = type.GetCustomAttribute<ConfluxDerivedAttribute>();
            if (attribute is null)
            {
                return null;
            }

            return new ConfluxDerivedInfo(type, attribute);
        }

        /// <summary>
        /// The Conflux derived type.
        /// </summary>
        public Type Type { get; init; }

        /// <summary>
        /// The <see cref="ConfluxDerivedAttribute"/> on <see cref="Type"/>.
        /// </summary>
        public ConfluxDerivedAttribute Attribute { get; init; }

        private ConfluxDerivedInfo(Type type, ConfluxDerivedAttribute attribute)
        {
            Type = type;
            Attribute = attribute;

            _polymorphicInfo = new(GetPolymorphicInfo);
        }

        /// <summary>
        /// The key used to indentify this derived type in serialization.
        /// </summary>
        public string Key => Attribute.Key;

        /// <summary>
        /// The info about the Conflux polymorphic base type associated with this derived info.
        /// </summary>
        public ConfluxPolymorphicInfo PolymorphicInfo => _polymorphicInfo.Value;

        /// <summary>
        /// Lazy evaluation of <see cref="PolymorphicInfo"/>.
        /// </summary>
        private readonly Lazy<ConfluxPolymorphicInfo> _polymorphicInfo;

        /// <summary>
        /// Does the actual work to find information about the polymorphic base type.
        /// </summary>
        /// <returns>The polymorphic base type.</returns>
        /// <exception cref="ConfluxException">If more or less than exactly one polymorphic base type was found.</exception>
        private ConfluxPolymorphicInfo GetPolymorphicInfo()
        {
            ConfluxPolymorphicInfo? polymorphicInfo = null;
            var currBase = Type;
            while (currBase is not null)
            {
                if (ConfluxPolymorphicInfo.TryGet(currBase, out var info))
                {
                    if (polymorphicInfo is not null)
                    {
                        throw new ConfluxException($"A ConfluxDerived type must inherit from exactly one ConfluxPolymorphic type, not two: {info.Type} and {polymorphicInfo.Type}.");
                    }

                    polymorphicInfo = info;
                }

                currBase = currBase.BaseType;
            }

            if (polymorphicInfo is null)
            {
                throw new ConfluxException($"A ConfluxDerived type must inherit from a ConfluxPolymorphic type.");
            }

            return polymorphicInfo;
        }
    }
}
