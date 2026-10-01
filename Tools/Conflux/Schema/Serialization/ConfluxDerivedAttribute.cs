namespace Conflux.Schema.Serialization
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class ConfluxDerivedAttribute(string key) : Attribute
    {
        public string Key { get; init; } = key;
    }
}
