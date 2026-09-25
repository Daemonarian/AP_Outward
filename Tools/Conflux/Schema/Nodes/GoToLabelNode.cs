namespace Conflux.Schema.Nodes
{
    internal class GoToLabelNode(string label) : ConfluxNode
    {
        public string Label { get; init; } = label;

        public override int OutConnectionCount => 0;
    }
}
