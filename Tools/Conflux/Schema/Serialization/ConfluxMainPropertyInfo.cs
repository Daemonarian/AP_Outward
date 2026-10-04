using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Conflux.Schema.Exceptions;

namespace Conflux.Schema.Serialization
{
    /// <summary>
    /// Represents information about a Conflux main property.
    /// </summary>
    internal class ConfluxMainPropertyInfo
    {
        /// <summary>
        /// A cache of main property infos for time-efficiency.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ConfluxMainPropertyInfo?> _cache = new();

        /// <summary>
        /// Try to get information about a main property on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="info">The main property info.</param>
        /// <returns>Whether or not a main property was found.</returns>
        public static bool TryGet(Type type, [NotNullWhen(true)] out ConfluxMainPropertyInfo? info)
        {
            info = _cache.GetOrAdd(type, Create);
            return info is not null;
        }

        /// <summary>
        /// Does the actual work to find the main property info.
        /// </summary>
        /// <param name="type">The type that may contain a main property.</param>
        /// <returns>Information about the main property, if it exists. Otherwise, null.</returns>
        /// <exception cref="ConfluxException">If more than one main property was found for the type.</exception>
        private static ConfluxMainPropertyInfo? Create(Type type)
        {
            ConfluxMainPropertyInfo? mainProperty = null;
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var attribute = property.GetCustomAttribute<ConfluxMainPropertyAttribute>();
                if (attribute is not null)
                {
                    if (mainProperty is not null)
                    {
                        throw new ConfluxException($"Type {type} must specify at most one {nameof(ConfluxMainPropertyAttribute)}, but at least two were found: {mainProperty.Property.Name} and {property.Name}.");
                    }

                    mainProperty = new(property, attribute);
                }
            }

            return mainProperty;
        }

        /// <summary>
        /// The main property.
        /// </summary>
        public PropertyInfo Property { get; init; }

        /// <summary>
        /// The <see cref="ConfluxMainPropertyAttribute"/> on <see cref="Property"/>.
        /// </summary>
        public ConfluxMainPropertyAttribute Attribute { get; init; }

        private ConfluxMainPropertyInfo(PropertyInfo property, ConfluxMainPropertyAttribute attribute)
        {
            Property = property;
            Attribute = attribute;
        }

        /// <summary>
        /// Is this main property forced.
        /// </summary>
        public bool IsForced => Attribute.Force;
    }
}
