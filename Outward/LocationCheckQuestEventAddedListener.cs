using System;
using System.Collections.Generic;
using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago
{
    internal class LocationCheckQuestEventAddedListener : IQuestEventAddedListener
    {
        public string OutwardEventId { get; private set; }

        public long LocationId { get; private set; }

        public int StackCount { get; private set; }

        public LocationCheckQuestEventAddedListener(string outwardEventId, long locationId, int stackCount = 1)
        {
            OutwardEventId = outwardEventId;
            LocationId = locationId;
            StackCount = stackCount;
        }

        public void OnQuestEventAdded(QuestEventData _eventData)
        {
            OutwardArchipelagoMod.Log.LogInfo($"LocationCheckQuestEventAddedListener received OnQuestEventAdded for EventUID = {_eventData.EventUID}.");
            if (string.Equals(_eventData.EventUID, OutwardEventId, StringComparison.Ordinal) && _eventData.StackCount >= StackCount)
            {
                OutwardArchipelagoMod.Log.LogInfo($"LocationCheckQuestEventAddedListener triggered for EventUID = {OutwardEventId}.");
                ArchipelagoConnector.Instance.CompleteLocationCheck(LocationId);
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
                new(OutwardQuestEvents.Tutorial_IntroFinished, APWorldLocation.QuestMain01),
                new(OutwardQuestEvents.CallToAdventure_Completed, APWorldLocation.QuestMain02),
                new(OutwardQuestEvents.General_DoneQuest0, APWorldLocation.QuestMain03),
                new(OutwardQuestEvents.General_DoneQuest1, APWorldLocation.QuestMain04),
                new(OutwardQuestEvents.General_DoneQuest2, APWorldLocation.QuestMain05),
                new(OutwardQuestEvents.General_DoneQuest3, APWorldLocation.QuestMain06),
                new(OutwardQuestEvents.General_DoneQuest4, APWorldLocation.QuestMain07),
                new(OutwardQuestEvents.DLC2Questline_DoneQ0, APWorldLocation.QuestMain08),
                new(OutwardQuestEvents.DLC2Questline_DoneQ1, APWorldLocation.QuestMain09),
                new(OutwardQuestEvents.DLC2Questline_DoneQ2, APWorldLocation.QuestMain10),
                new(OutwardQuestEvents.DLC2Questline_DoneQ3, APWorldLocation.QuestMain11),
                new(OutwardQuestEvents.DLC2Questline_DoneQ4, APWorldLocation.QuestMain12),
                new(OutwardQuestEvents.Fraticide_EndReward, APWorldLocation.QuestParallelBloodUnderTheSun),
                new(OutwardQuestEvents.Purifier_QuestComplete, APWorldLocation.QuestParallelPurifier),
                new(OutwardQuestEvents.Vendavel_Succeeded, APWorldLocation.QuestParallelPurifier),
                new(OutwardQuestEvents.Vendavel_Failure, APWorldLocation.QuestParallelPurifier),
                new(OutwardQuestEvents.PromptsComplete_Mana, APWorldLocation.QuestMinorAcquireMana),
                new(OutwardQuestEvents.General_TsarElectricLab, APWorldLocation.QuestMinorArcaneMachine),
                new(OutwardQuestEvents.PromptsComplete_CierzoBlacksmith, APWorldLocation.QuestMinorCraftBlueSandArmor),
                new(OutwardQuestEvents.PromptsComplete_BergBlacksmith, APWorldLocation.QuestMinorCraftCopalAndPetrifiedArmor),
                new(OutwardQuestEvents.PromptsComplete_MonsoonBlacksmith, APWorldLocation.QuestMinorCraftPalladiumArmor),
                new(OutwardQuestEvents.PromptsComplete_LevantBlacksmith, APWorldLocation.QuestMinorCraftTsarAndTenebrousArmor),
                new(OutwardQuestEvents.PromptsComplete_HarmattanBlacksmith, APWorldLocation.QuestMinorCraftAntiquePlateGarbArmor),
                new(OutwardQuestEvents.SideQuests_SmugglerTimerWait, APWorldLocation.QuestMinorLostMerchant),
                new(OutwardQuestEvents.PromptsComplete_Water, APWorldLocation.QuestMinorPurifyTheWater),
                new(OutwardQuestEvents.SideQuests_DoneRedIdol, APWorldLocation.QuestMinorRedIdol),
                new(OutwardQuestEvents.Fraticide_SlumsGaveMoney, APWorldLocation.QuestMinorSilverForTheSlums, 5),
            };

            foreach (var listener in listeners)
            {
                listener.Register();
            }
        }

        [HarmonyPatch(typeof(QuestEventManager), nameof(QuestEventManager.Awake))]
        public static class QuestEventManager_Awake
        {
            private static void Postfix(QuestEventManager __instance)
            {
                OutwardArchipelagoMod.Log.LogDebug("QuestEventManager Awake postfix called.");
                RegisterAll();
            }
        }
    }
}
