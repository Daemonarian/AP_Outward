namespace Conflux.GraphViz
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class GraphVizPropertyAttribute : Attribute
    {
        public string? Alias { get; init; }

        public bool IsMainProperty { get; init; } = false;
    }
}
