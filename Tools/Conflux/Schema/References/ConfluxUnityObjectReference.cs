using Conflux.NodeCanvas.References;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxUnityObjectReference : INodeCanvasObjectBuilder<UnityObject>
    {
        [YamlMember(Alias = "index")]
        public int? SideCarIndex { get; set; }

        public UnityObject BuildNodeCanvasObject(NodeCanvasGraphContext context) => new()
        {
            SideCarIndex = SideCarIndex ?? throw new ConfluxValueException("The value unityObject.index must be specified."),
        };
    }
}
