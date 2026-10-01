namespace Conflux.Schema.Context
{
    internal interface INodeCanvasObjectBuilder<T>
    {
        abstract T BuildNodeCanvasObject(NodeCanvasGraphContext context);
    }
}
