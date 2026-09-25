using System.Text;
using Newtonsoft.Json;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    internal abstract class Node : IGraphVizLabelable
    {
        [JsonIgnore]
        public abstract int OutConnectionCount { get; }

        public virtual string GetGraphVizShortName() => GetType().Name;

        public virtual string GetGraphVizContent() => JsonConvert.SerializeObject(this, GraphVizConverter.DefaultSerializerSettings);

        public string ToGraphVizLabel()
        {
            var label = new StringBuilder();
            label.AppendLine(GetGraphVizShortName().Trim());
            label.AppendLine(GetGraphVizContent());
            return label.ToString().TrimEnd();
        }
    }
}
