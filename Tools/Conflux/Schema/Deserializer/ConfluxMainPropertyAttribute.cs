namespace Conflux.Schema.Deserializer
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    internal class ConfluxMainPropertyAttribute : Attribute
    {
        /// <summary>
        /// Force deserialization of this main property even when the enclosed data in a mapping type.
        /// </summary>
        public bool Force { get; set; } = false;
    }
}
