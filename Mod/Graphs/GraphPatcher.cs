using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HarmonyLib;
using Newtonsoft.Json;
using NodeCanvas.Framework;
using UnityEngine;

namespace OutwardArchipelago.Graphs
{
    internal class GraphPatcher
    {
        private const int MushroomShield = 2300150;
        private const int BlueSkullEffigy = 6200160;
        private static readonly Lazy<GraphPatcher> _instance = new(() => new GraphPatcher());
        public static GraphPatcher Instance => _instance.Value;

        /// <summary>
        /// A cache of all the serialized graph objects by their unique path in the scene hierarchy.
        /// </summary>
        private IReadOnlyDictionary<string, string> SerializedGraphs = null;

        public GraphPatcher()
        {
            LoadCustomGraphAssets();
        }

        private void LoadCustomGraphAssets()
        {
            OutwardArchipelagoMod.Log.LogInfo("Loading graph assets...");

            var graphAssetDir = Path.Combine(OutwardArchipelagoMod.Instance.AssetsPath, "graphs");
            var graphFiles = Directory.GetFiles(graphAssetDir, "*.json", SearchOption.AllDirectories);
            var serializedGraphs = new Dictionary<string, string>();
            foreach (var graphFile in graphFiles)
            {
                var graphName = OutwardArchipelagoMod.Instance.GetRelativePath(graphAssetDir, graphFile);
                graphName = Regex.Replace(graphName, @"\.json$", "", RegexOptions.IgnoreCase);
                graphName = graphName.Replace("\\", "/");

                if (string.Equals(graphName, "routing", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (serializedGraphs.ContainsKey(graphName))
                {
                    OutwardArchipelagoMod.Log.LogWarning($"    duplicate graph asset: {graphName}");
                    continue;
                }

                OutwardArchipelagoMod.Log.LogDebug($"  loading graph: {graphName}");

                try
                {
                    var graph = File.ReadAllText(graphFile, Encoding.UTF8);
                    serializedGraphs[graphName] = graph;
                }
                catch (Exception e)
                {
                    OutwardArchipelagoMod.Log.LogError($"Failed to load graph asset: {graphName}\n{e}");
                }
            }

            OutwardArchipelagoMod.Log.LogInfo("  loading routing info...");

            var routingPath = Path.Combine(graphAssetDir, "routing.json");
            if (File.Exists(routingPath))
            {
                var json = File.ReadAllText(routingPath, Encoding.UTF8);
                var rawManifest = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);

                foreach (var entry in rawManifest)
                {
                    var graphName = entry.Key;
                    if (serializedGraphs.TryGetValue(graphName, out var graph))
                    {
                        foreach (var graphAltName in entry.Value)
                        {
                            if (!serializedGraphs.ContainsKey(graphAltName))
                            {
                                serializedGraphs[graphAltName] = graph;
                            }
                            else
                            {
                                OutwardArchipelagoMod.Log.LogWarning($"duplicate graph asset defined in routing: {graphAltName}");
                            }
                        }
                    }
                }
            }
            else
            {
                OutwardArchipelagoMod.Log.LogWarning("graph routing file not found: assets/graphs/routing.json");
            }

            OutwardArchipelagoMod.Log.LogInfo("All graph assets loaded!");

            SerializedGraphs = serializedGraphs;
        }

        /// <summary>
        /// Try to get the custom graph asset for the specified
        /// graph owner.
        /// </summary>
        /// <param name="graphOwner">The graph owner.</param>
        /// <param name="graph">The custom graph asset.</param>
        /// <returns>Whether a custom graph asset was found.</returns>
        private bool TryGetGraphForGraphOwner(GraphOwner graphOwner, out Graph graph)
        {
            DumpGraph(graphOwner, true);

            if (/* OutwardArchipelagoMod.Instance.IsInArchipelagoGame && */ graphOwner is not null && SerializedGraphs is not null)
            {
                var graphPath = GetGraphPath(graphOwner);
                if (SerializedGraphs.TryGetValue(graphPath, out var serializedGraph))
                {
                    OutwardArchipelagoMod.Log.LogDebug($"Replacing graph with custom asset: {graphPath}");

                    var graphName = graphOwner.graph?.name ?? $"{graphOwner.name} {graphOwner.graphType.Name}";
                    var graphObjectReferences = (string.IsNullOrEmpty(graphOwner.boundGraphSerialization) ? graphOwner.boundGraphObjectReferences : graphOwner.graph?._objectReferences) ?? new();

                    graph = (Graph)ScriptableObject.CreateInstance(graphOwner.graphType);
                    graph.name = graphName;
                    graph.Deserialize(serializedGraph, true, graphObjectReferences);

                    return true;
                }
            }

            graph = null;
            return false;
        }

        [Conditional("DEBUG")]
        private void DumpGraph(GraphOwner graphOwner, bool prePatch)
        {
            if (graphOwner is null)
            {
                return;
            }

            var patchTag = prePatch ? "original" : "patched";
            var graphPath = GetGraphPath(graphOwner);
            var graphName = graphOwner.graph?.name ?? $"{graphOwner.name} {graphOwner.graphType.Name}";
            var serializedGraph = graphOwner.graph?.Serialize(false, graphOwner.graph._objectReferences) ?? graphOwner.boundGraphSerialization;

            OutwardArchipelagoMod.Log.LogDebug($"DUMP {patchTag} NodeCanvas.Framework.Graph \"{graphPath}/{graphName}\": {serializedGraph}");
        }

        /// <summary>
        /// Gets the fully-qualified path to the graph owner object
        /// in the Unity scene hierarchy. Useful for uniquely identifying
        /// graph owners.
        /// </summary>
        /// <param name="graphOwner">The Unity graph owner.</param>
        /// <returns>The path.</returns>
        private static string GetGraphPath(GraphOwner graphOwner)
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

        [HarmonyPatch(typeof(GraphOwner), nameof(GraphOwner.Initialize), new Type[] { })]
        private static class Patch_GraphOwner_Initialize
        {
            private static bool Prefix(GraphOwner __instance)
            {
                try
                {
                    if (!__instance.initialized && Instance.TryGetGraphForGraphOwner(__instance, out var graph))
                    {
                        __instance.initialized = true;
                        __instance.graph = graph;
                        __instance.instances[__instance.graph] = __instance.graph;

                        return false;
                    }
                }
                catch (Exception e)
                {
                    OutwardArchipelagoMod.Log.LogError($"failed to load custom graph asset: {e}");
                }

                return true;
            }
        }
    }
}
