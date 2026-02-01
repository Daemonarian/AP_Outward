using System;
using System.Collections.Generic;
using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago
{
    internal class LocationCheckQuestEventAddedListener : IQuestEventAddedListener
    {
        public string OutwardEventId { get; private set; }

        public APWorld.Location Location { get; private set; }

        public int StackCount { get; private set; }

        public LocationCheckQuestEventAddedListener(string outwardEventId, APWorld.Location location, int stackCount = 1)
        {
            OutwardEventId = outwardEventId;
            Location = location;
            StackCount = stackCount;
        }

        public void OnQuestEventAdded(QuestEventData _eventData)
        {
            OutwardArchipelagoMod.Log.LogInfo($"LocationCheckQuestEventAddedListener received OnQuestEventAdded for EventUID = {_eventData.EventUID}.");
            if (string.Equals(_eventData.EventUID, OutwardEventId, StringComparison.Ordinal) && _eventData.StackCount >= StackCount)
            {
                OutwardArchipelagoMod.Log.LogInfo($"LocationCheckQuestEventAddedListener triggered for EventUID = {OutwardEventId}.");
                ArchipelagoConnector.Instance.Locations.Complete(Location);
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
                new(OutwardQuestEvents.Tutorial_IntroFinished, APWorld.Location.QuestMain01),
                new(OutwardQuestEvents.CallToAdventure_Completed, APWorld.Location.QuestMain02),
                new(OutwardQuestEvents.General_DoneQuest0, APWorld.Location.QuestMain03),
                new(OutwardQuestEvents.General_DoneQuest1, APWorld.Location.QuestMain04),
                new(OutwardQuestEvents.General_DoneQuest2, APWorld.Location.QuestMain05),
                new(OutwardQuestEvents.General_DoneQuest3, APWorld.Location.QuestMain06),
                new(OutwardQuestEvents.General_DoneQuest4, APWorld.Location.QuestMain07),
                new(OutwardQuestEvents.DLC2Questline_DoneQ0, APWorld.Location.QuestMain08),
                new(OutwardQuestEvents.DLC2Questline_DoneQ1, APWorld.Location.QuestMain09),
                new(OutwardQuestEvents.DLC2Questline_DoneQ2, APWorld.Location.QuestMain10),
                new(OutwardQuestEvents.DLC2Questline_DoneQ3, APWorld.Location.QuestMain11),
                new(OutwardQuestEvents.DLC2Questline_DoneQ4, APWorld.Location.QuestMain12),
                new(OutwardQuestEvents.Fraticide_EndReward, APWorld.Location.QuestParallelBloodUnderTheSun),
                new(OutwardQuestEvents.Purifier_QuestComplete, APWorld.Location.QuestParallelPurifier),
                new(OutwardQuestEvents.Vendavel_Succeeded, APWorld.Location.QuestParallelPurifier),
                new(OutwardQuestEvents.Vendavel_Failure, APWorld.Location.QuestParallelPurifier),
                new(OutwardQuestEvents.PromptsComplete_Mana, APWorld.Location.QuestMinorAcquireMana),
                new(OutwardQuestEvents.General_TsarElectricLab, APWorld.Location.QuestMinorArcaneMachine),
                new(OutwardQuestEvents.PromptsComplete_CierzoBlacksmith, APWorld.Location.QuestMinorCraftBlueSandArmor),
                new(OutwardQuestEvents.PromptsComplete_BergBlacksmith, APWorld.Location.QuestMinorCraftCopalAndPetrifiedArmor),
                new(OutwardQuestEvents.PromptsComplete_MonsoonBlacksmith, APWorld.Location.QuestMinorCraftPalladiumArmor),
                new(OutwardQuestEvents.PromptsComplete_LevantBlacksmith, APWorld.Location.QuestMinorCraftTsarAndTenebrousArmor),
                new(OutwardQuestEvents.PromptsComplete_HarmattanBlacksmith, APWorld.Location.QuestMinorCraftAntiquePlateGarbArmor),
                new(OutwardQuestEvents.SideQuests_SmugglerTimerWait, APWorld.Location.QuestMinorLostMerchant),
                new(OutwardQuestEvents.PromptsComplete_Water, APWorld.Location.QuestMinorPurifyTheWater),
                new(OutwardQuestEvents.SideQuests_DoneRedIdol, APWorld.Location.QuestMinorRedIdol),
                new(OutwardQuestEvents.Fraticide_SlumsGaveMoney, APWorld.Location.QuestMinorSilverForTheSlums, 5),
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
