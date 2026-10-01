namespace Conflux.NodeCanvas
{
    internal interface IConfluxObjectBuilder<T>
    {
        public abstract T BuildConfluxObject(Graph graph);
    }
}
