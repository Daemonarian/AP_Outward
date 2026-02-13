using System;
using System.Collections.Generic;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Utils;
using UnityEngine;

namespace OutwardArchipelago.SkillTrainer
{
    internal class APSkill : Skill
    {
        private static readonly Lazy<Skill> _apItemPrefab = new(GetAPItemPrefab);
        public static Skill APItemPrefab => _apItemPrefab.Value;

        private static Skill GetAPItemPrefab()
        {
            var skill = (Skill)ResourcesPrefabManager.Instance.GetItemPrefab(OutwardSkill.APItem);
            _ = skill.ItemIcon; // this triggers SideLoader to apply all the skill visuals
            return skill;
        }

        private static readonly Dictionary<APWorld.Location, APSkill> _cache = new();

        public static APSkill GetPrefab(APWorld.Location location)
        {
            OutwardArchipelagoMod.Log.LogDebug($"getting prefab for location {location.Id:X16}");
            lock (_cache)
            {
                if (!_cache.TryGetValue(location, out var skill) || !skill)
                {
                    skill = Create(location);
                    _cache[location] = skill;
                }

                OutwardArchipelagoMod.Log.LogDebug($"got prefab for location {location.Id:X16}: {skill.name}");
                return skill;
            }
        }

        private static APSkill Create(APWorld.Location location)
        {
            var obj = new GameObject($"APSkill_{location.Id:X16}");
            obj.SetActive(false);

            var skill = obj.AddComponent<APSkill>();
            skill.Init(location);

            if (APItemPrefab.gameObject.activeSelf)
            {
                obj.SetActive(true);
            }

            return skill;
        }

        public APWorld.Location Location { get; private set; }

        public string HintedItemName { get; private set; } = null;

        public string HintedItemDescription { get; private set; } = null;

        private void Init(APWorld.Location location)
        {
            CloneUtils.DeepCopy(APItemPrefab, this);

            Location = location;

            ArchipelagoConnector.Instance.Hints.GetHint(location, OnHint);
        }

        public override string Name { get => string.IsNullOrEmpty(HintedItemName) ? base.Name : HintedItemName; set => base.Name = value; }

        public override string Description => string.IsNullOrEmpty(HintedItemDescription) ? base.Description : HintedItemDescription;

        private void OnHint(ArchipelagoConnector.IHint hint)
        {
            OutwardArchipelagoMod.Log.LogDebug($"APSkill.OnHint {hint.PlayerName}'s {hint.ItemName} ({hint.ItemFlagsString})");
            HintedItemName = $"{hint.PlayerName}'s {hint.ItemName}";
            HintedItemDescription = $"{hint.PlayerName}'s {hint.ItemName} ({hint.ItemFlagsString})";
        }
    }
}
