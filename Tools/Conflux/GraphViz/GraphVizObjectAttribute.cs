namespace Conflux.GraphViz
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class GraphVizObjectAttribute : Attribute
    {
        public string? Alias { get; init; }
    }
}
