using System.Collections.Frozen;
using System.ComponentModel;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Blackboards
{
    internal class ConfluxLocalBlackboardVariable
    {
        public static readonly FrozenDictionary<string, string> TypeAliasMapping = new Dictionary<string, string>
        {
            { "character", "NodeCanvas.Framework.Variable`1[[Character, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]" },
            { "dialogueStarter", "NodeCanvas.Framework.Variable`1[[DialogueStarter, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]" },
            { "merchant", "NodeCanvas.Framework.Variable`1[[Merchant, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]" },
            { "merchantFastTravel", "NodeCanvas.Framework.Variable`1[[MerchantFastTravel, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]" },
            { "trainer", "NodeCanvas.Framework.Variable`1[[Trainer, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]" },
            { "object", "NodeCanvas.Framework.Variable`1[[UnityEngine.GameObject, UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]" },
        }.ToFrozenDictionary();

        public static readonly FrozenDictionary<string, string> TypeAliasReverseMapping = TypeAliasMapping
            .ToFrozenDictionary(pair => pair.Value, pair => pair.Key);

        [YamlIgnore]
        public string Name { get; set; } = string.Empty;

        [ConfluxMainProperty]
        [YamlMember(Alias = "type")]
        public string Type { get; set; } = string.Empty;

        [YamlMember(Alias = "id")]
        public Guid? ID { get; set; }

        [YamlMember(Alias = "protected")]
        [DefaultValue(false)]
        public bool IsProtected { get; set; } = false;

        [YamlMember(Alias = "index")]
        public int? Index { get; set; }

        public Variable BuildVariable(NodeCanvasGraphContext context)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ConfluxValueException("Property variable.name must be specified.");
            }

            if (!TypeAliasMapping.TryGetValue(Type, out var fullyQualifiedType))
            {
                throw new ConfluxValueException($"The value of variable.type must be one of must be one of {string.Join(", ", TypeAliasMapping.Keys.Select(k => $"\"{k}\""))}, not {Type}.");
            }

            UnityObject? value = null;
            if (Index is not null)
            {
                value = new UnityObject
                {
                    SideCarIndex = Index.Value,
                };
            }

            return new Variable
            {
                Name = Name,
                Type = fullyQualifiedType,
                ID = ID?.ToString(),
                IsProtected = IsProtected,
                Value = value,
            };
        }
    }
}
