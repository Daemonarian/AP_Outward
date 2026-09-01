using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs
{
    /// <summary>
    /// Provides contextual information for dialogue patching operations, including access to the dialogue tree and its
    /// nodes.
    /// </summary>
    internal class GraphPatchContext : IGraphPatchContext
    {
        public GraphOwner GraphOwner { get; private set; }

        public Graph Graph { get; private set; }

        public string Name { get; private set; }

        public string Path { get; private set; }

        public IReadOnlyDictionary<int, Node> NodesByID { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DialoguePatchContext class using the specified dialogue tree.
        /// </summary>
        /// <param name="tree">The dialogue tree to use as the context for patching operations. Cannot be null.</param>
        public GraphPatchContext(GraphOwner graphOwner)
        {

            //var serializedGraph = graphOwner.graph.Serialize(false, graphOwner.graph._objectReferences);
            //OutwardArchipelagoMod.Log.LogDebug($"graph owner initialized: \"{path}\" = {serializedGraph}");

            GraphOwner = graphOwner;
            Graph = GraphOwner.graph;
            Name = Graph.name;
            Path = GetPath(GraphOwner);
            NodesByID = Graph.allNodes.ToDictionary((node) => node.ID);
        }

        private static string GetPath(GraphOwner graphOwner)
        {
            var names = new List<string>();
            var obj = graphOwner.gameObject;
            while (obj is not null)
            {
                names.Add(obj.name);
                obj = obj.transform?.parent?.gameObject;
            }

            names.Add(SceneManagerHelper.ActiveSceneName);
            names.Reverse();

            return string.Join("/", names.Select(name => name.Replace("\\", "\\\\").Replace("/", "\\/")));
        }
    }
}
