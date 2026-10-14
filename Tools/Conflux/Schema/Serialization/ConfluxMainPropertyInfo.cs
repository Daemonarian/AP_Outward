using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Conflux.Schema.Exceptions;
using YamlDotNet.Serialization;

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

                    var yamlMemberAttribute = property.GetCustomAttribute<YamlMemberAttribute>();
                    var yamlKey = yamlMemberAttribute?.Alias ?? ToCamelCase(property.Name);

                    mainProperty = new(property, attribute, yamlKey);
                }
            }

            return mainProperty;
        }

        /// <summary>
        /// Camel-cases a string.
        /// </summary>
        /// <param name="value">The string to camel-case.</param>
        /// <returns>
        /// A reference to the original string if no change is required;
        /// otherwise, a newly constructed string with the first character
        /// replaced with its lower-case equivalent.
        /// </returns>
        private static string ToCamelCase(string value)
        {
            if (string.IsNullOrEmpty(value) || char.IsLower(value[0]))
            {
                return value;
            }

            return char.ToLowerInvariant(value[0]) + value[1..];
        }

        /// <summary>
        /// The main property.
        /// </summary>
        public PropertyInfo Property { get; init; }

        /// <summary>
        /// The <see cref="ConfluxMainPropertyAttribute"/> on <see cref="Property"/>.
        /// </summary>
        public ConfluxMainPropertyAttribute Attribute { get; init; }

        /// <summary>
        /// The actual key used for the main property in the raw YAML file.
        /// </summary>
        public string YamlKey { get; set; }

        private ConfluxMainPropertyInfo(PropertyInfo property, ConfluxMainPropertyAttribute attribute, string yamlKey)
        {
            Property = property;
            Attribute = attribute;
            YamlKey = yamlKey;
        }

        /// <summary>
        /// Is this main property forced.
        /// </summary>
        public bool IsForced => Attribute.Force;
    }
}
