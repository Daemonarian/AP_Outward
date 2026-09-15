using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NodeCanvas.Framework.Internal;
using ParadoxNotion.Serialization;

namespace OutwardArchipelago.Graphs
{
    internal class FrozenGraphTemplate
    {
        private readonly string _name;

        private readonly IReadOnlyCollection<string> _graphsToReplace;

        private readonly string _serializedGraph;

        public string Name => _name;

        public IReadOnlyCollection<string> GraphsToReplace => _graphsToReplace;

        public string SerializedGraph => _serializedGraph;

        public FrozenGraphTemplate(string name, GraphTemplate template)
        {
            _name = name;
            _graphsToReplace = template.PathsToReplace.ToHashSet();
            _serializedGraph = template.Graph.ToString();

            ValidateTemplate();
        }

        [Conditional("DEBUG")]
        private void ValidateTemplate()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    throw new ArgumentException("Parameter may not be null or whitespace", nameof(Name));
                }

                JSONSerializer.Deserialize<GraphSerializationData>(SerializedGraph, new());
            }
            catch (Exception e)
            {
                OutwardArchipelagoMod.Log.LogError($"[GraphPatcher] Graph template failed validation: {Name}\n{e}");
            }
        }
    }
}
