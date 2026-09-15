using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HarmonyLib;
using Newtonsoft.Json;
using NodeCanvas.Framework;
using UnityEngine;

namespace OutwardArchipelago.Graphs
{
    internal class GraphPatcher : MonoBehaviour
    {
        public static GraphPatcher Instance { get; private set; }

        /// <summary>
        /// A cache of all the serialized graph objects by their unique path in the scene hierarchy.
        /// </summary>
        private IReadOnlyDictionary<string, FrozenGraphTemplate> TemplatesByPath = null;

        protected void Awake()
        {
            if (Instance is not null)
            {
                return;
            }

            Instance = this;

            LoadCustomGraphAssets();
        }

        private void LoadCustomGraphAssets()
        {
            try
            {
                OutwardArchipelagoMod.Log.LogInfo("[GraphPatcher] Loading graph assets...");

                var graphAssetDir = Path.Combine(OutwardArchipelagoMod.Instance.ModPath, "graphs");

                var templatesByPath = new Dictionary<string, FrozenGraphTemplate>();
                foreach (var graphFile in Directory.GetFiles(graphAssetDir, "*.json", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        var name = Path.GetFileNameWithoutExtension(graphFile);
                        OutwardArchipelagoMod.Log.LogDebug($"[GraphPatcher]   Processing graph template: {name}");

                        var rawText = File.ReadAllText(graphFile);
                        var rawTemplate = JsonConvert.DeserializeObject<GraphTemplate>(rawText);
                        var template = new FrozenGraphTemplate(name, rawTemplate);

                        foreach (var unityPath in template.GraphsToReplace)
                        {
                            if (templatesByPath.TryGetValue(unityPath, out var otherTemplate))
                            {
                                OutwardArchipelagoMod.Log.LogWarning($"[GraphPatcher] Graph template \"{template.Name}\" specified a path to replace that is already in use by another template \"{otherTemplate.Name}\".");
                            }
                            else
                            {
                                templatesByPath[unityPath] = template;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        OutwardArchipelagoMod.Log.LogError($"[GraphPatcher] An error occurred while processing a graph template: {e}");
                    }
                }

                OutwardArchipelagoMod.Log.LogInfo("[GraphPatcher] All graph assets loaded!");

                TemplatesByPath = templatesByPath;
            }
            catch (Exception e)
            {
                OutwardArchipelagoMod.Log.LogError($"[GraphPatcher] An error occurred while loading custom graph assets: {e}");

                TemplatesByPath = new Dictionary<string, FrozenGraphTemplate>();
            }
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
            DumpGraph(graphOwner);

            if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled && graphOwner is not null && TemplatesByPath is not null)
            {
                var graphPath = GetGraphPath(graphOwner);
                if (TemplatesByPath.TryGetValue(graphPath, out var template))
                {
                    OutwardArchipelagoMod.Log.LogDebug($"[GraphPatcher] Replacing graph: {graphPath}");

                    var graphName = graphOwner.graph?.name ?? $"{graphOwner.name} {graphOwner.graphType.Name}";
                    var graphObjectReferences = (string.IsNullOrEmpty(graphOwner.boundGraphSerialization) ? graphOwner.boundGraphObjectReferences : graphOwner.graph?._objectReferences) ?? new();

                    graph = (Graph)ScriptableObject.CreateInstance(graphOwner.graphType);
                    graph.name = graphName;
                    graph.Deserialize(template.SerializedGraph, true, graphObjectReferences);

                    return true;
                }
            }

            graph = null;
            return false;
        }

        [Conditional("DEBUG")]
        private void DumpGraph(GraphOwner graphOwner)
        {
            if (graphOwner is null)
            {
                return;
            }

            var graphPath = GetGraphPath(graphOwner);
            var graphName = graphOwner.graph?.name ?? $"{graphOwner.name} {graphOwner.graphType.Name}";
            var serializedGraph = graphOwner.graph?.Serialize(false, graphOwner.graph._objectReferences) ?? graphOwner.boundGraphSerialization;

            OutwardArchipelagoMod.Log.LogDebug($"DUMP NodeCanvas.Framework.Graph \"{graphPath}/{graphName}\": {serializedGraph}");
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
                    if (!__instance.initialized && Instance is not null && Instance.TryGetGraphForGraphOwner(__instance, out var graph))
                    {
                        __instance.initialized = true;
                        __instance.graph = graph;
                        __instance.instances[__instance.graph] = __instance.graph;

                        return false;
                    }
                }
                catch (Exception e)
                {
                    OutwardArchipelagoMod.Log.LogError($"[GraphPatcher] Failed to load custom graph asset: {e}");
                }

                return true;
            }
        }
    }
}
