using HarmonyLib;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Archipelago.Data;
using System;
using System.Collections.Generic;

namespace OutwardArchipelago
{
    internal class LocationCheckQuestEventAddedListener : IQuestEventAddedListener
    {
        public string OutwardEventId { get; }
        public ArchipelagoLocationData Location { get; }

        public LocationCheckQuestEventAddedListener(string outwardEventId, ArchipelagoLocationData location)
        {
            OutwardEventId = outwardEventId;
            Location = location;
        }

        public void OnQuestEventAdded(QuestEventData _eventData)
        {
            OutwardArchipelagoMod.Log.LogInfo($"LocationCheckQuestEventAddedListener received OnQuestEventAdded for EventUID = {_eventData.EventUID}.");
            if (string.Equals(_eventData.EventUID, OutwardEventId, StringComparison.Ordinal))
            {
                OutwardArchipelagoMod.Log.LogInfo($"LocationCheckQuestEventAddedListener triggered for EventUID = {OutwardEventId}.");
                ArchipelagoConnector.Instance.CompleteLocationCheck(Location);
            }
        }

        public void Register()
        {
            OutwardArchipelagoMod.Log.LogInfo($"Registering LocationCheckQuestEventAddedListener for OutwardEventId = {OutwardEventId}.");
            QuestEventManager.Instance.RegisterOnQEAddedListener(OutwardEventId, this);
        }

        private static void RegisterAll()
        {
            if (!QuestEventManager.Instance)
            {
                return;
            }

            OutwardArchipelagoMod.Log.LogInfo("Registering all LocationCheckQuestEventAddedListeners.");

            var listeners = new List<LocationCheckQuestEventAddedListener>
            {
                new(OutwardQuestEvents.Tutorial_IntroFinished, ArchipelagoLocationData.QuestMain01),
                new(OutwardQuestEvents.CallToAdventure_Completed, ArchipelagoLocationData.QuestMain02),
                new(OutwardQuestEvents.General_DoneQuest0, ArchipelagoLocationData.QuestMain03),
                new(OutwardQuestEvents.General_DoneQuest1, ArchipelagoLocationData.QuestMain04),
                new(OutwardQuestEvents.General_DoneQuest2, ArchipelagoLocationData.QuestMain05),
                new(OutwardQuestEvents.General_DoneQuest3, ArchipelagoLocationData.QuestMain06),
                new(OutwardQuestEvents.General_DoneQuest4, ArchipelagoLocationData.QuestMain07),
                new(OutwardQuestEvents.DLC2Questline_DoneQ0, ArchipelagoLocationData.QuestMain08),
                new(OutwardQuestEvents.DLC2Questline_DoneQ1, ArchipelagoLocationData.QuestMain09),
                new(OutwardQuestEvents.DLC2Questline_DoneQ2, ArchipelagoLocationData.QuestMain10),
                new(OutwardQuestEvents.DLC2Questline_DoneQ3, ArchipelagoLocationData.QuestMain11),
                new(OutwardQuestEvents.DLC2Questline_DoneQ4, ArchipelagoLocationData.QuestMain12),
                new(OutwardQuestEvents.Fraticide_EndReward, ArchipelagoLocationData.QuestParallelBloodUnderTheSun),
                new(OutwardQuestEvents.Purifier_QuestComplete, ArchipelagoLocationData.QuestParallelPurifier),
                new(OutwardQuestEvents.Vendavel_Succeeded, ArchipelagoLocationData.QuestParallelPurifier),
                new(OutwardQuestEvents.Vendavel_Failure, ArchipelagoLocationData.QuestParallelPurifier),
                new(OutwardQuestEvents.PromptsComplete_Mana, ArchipelagoLocationData.QuestMinorAcquireMana),
                new(OutwardQuestEvents.General_TsarElectricLab, ArchipelagoLocationData.QuestMinorArcaneMachine),
                new(OutwardQuestEvents.PromptsComplete_CierzoBlacksmith, ArchipelagoLocationData.QuestMinorCraftBlueSandArmor),
                new(OutwardQuestEvents.PromptsComplete_BergBlacksmith, ArchipelagoLocationData.QuestMinorCraftCopalAndPetrifiedArmor),
                new(OutwardQuestEvents.PromptsComplete_MonsoonBlacksmith, ArchipelagoLocationData.QuestMinorCraftPalladiumArmor),
                new(OutwardQuestEvents.PromptsComplete_LevantBlacksmith, ArchipelagoLocationData.QuestMinorCraftTsarAndTenebrousArmor),
                new(OutwardQuestEvents.PromptsComplete_HarmattanBlacksmith, ArchipelagoLocationData.QuestMinorCraftAntiquePlateGarbArmor),
            };

            foreach (var listener in listeners)
            {
                listener.Register();
            }
        }

        [HarmonyPatch(typeof(QuestEventManager), nameof(QuestEventManager.Awake))]
        public static class QuestEventManager_Awake
        {
            static void Postfix(QuestEventManager __instance)
            {
                OutwardArchipelagoMod.Log.LogDebug("QuestEventManager Awake postfix called.");
                RegisterAll();
            }
        }
    }
}
