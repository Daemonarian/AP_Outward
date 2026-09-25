namespace NodeCanvas.Tool.Conflux.Deserializer
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class ConfluxDerivedAttribute(string key) : Attribute
    {
        public string Key { get; init; } = key;
    }
}
