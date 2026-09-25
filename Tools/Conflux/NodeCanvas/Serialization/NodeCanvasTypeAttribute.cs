namespace Conflux.NodeCanvas.Serialization
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class NodeCanvasTypeAttribute(string typeName) : Attribute
    {
        public string TypeName { get; set; } = typeName;
    }
}
