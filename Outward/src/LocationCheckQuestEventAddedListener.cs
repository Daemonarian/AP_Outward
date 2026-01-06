using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutwardModTemplate
{
    public class LocationCheckQuestEventAddedListener : IQuestEventAddedListener
    {
        public string OutwardEventId { get; }
        public long ArchipelagoLocationId { get; }

        public LocationCheckQuestEventAddedListener(string outwardEventId, long archipelagoLocationId)
        {
            OutwardEventId = outwardEventId;
            ArchipelagoLocationId = archipelagoLocationId;
        }

        public void OnQuestEventAdded(QuestEventData _eventData)
        {
            Plugin.Log.LogInfo($"LocationCheckQuestEventAddedListener received OnQuestEventAdded for EventUID = {_eventData.EventUID}.");
            if (string.Equals(_eventData.EventUID, OutwardEventId, StringComparison.Ordinal))
            {
                Plugin.Log.LogInfo($"LocationCheckQuestEventAddedListener triggered for EventUID = {OutwardEventId}.");
                ArchipelagoConnector.Instance.CompleteLocationCheck(ArchipelagoLocationId);
            }
        }

        public void Register()
        {
            Plugin.Log.LogInfo($"Registering LocationCheckQuestEventAddedListener for OutwardEventId = {OutwardEventId}.");
            QuestEventManager.Instance.RegisterOnQEAddedListener(OutwardEventId, this);
        }

        private static void RegisterAll()
        {
            if (!QuestEventManager.Instance)
            {
                return;
            }

            Plugin.Log.LogInfo("Registering all LocationCheckQuestEventAddedListeners.");

            var listeners = new List<LocationCheckQuestEventAddedListener>
            {
                new(OutwardQuestEvents.Neutral_CallToAdventure.CallToAdventure_Completed,   ArchipelagoLocations.MAIN_QUEST_1),
                new(OutwardQuestEvents.Neutral_General.General_DoneQuest0,                  ArchipelagoLocations.MAIN_QUEST_2),
                new(OutwardQuestEvents.Neutral_General.General_DoneQuest1,                  ArchipelagoLocations.MAIN_QUEST_3),
                new(OutwardQuestEvents.Neutral_General.General_DoneQuest2,                  ArchipelagoLocations.MAIN_QUEST_4),
                new(OutwardQuestEvents.Neutral_General.General_DoneQuest3,                  ArchipelagoLocations.MAIN_QUEST_5),
                new(OutwardQuestEvents.Neutral_General.General_DoneQuest4,                  ArchipelagoLocations.MAIN_QUEST_6),
                new(OutwardQuestEvents.DLC2_Caldera_Questline.DLC2Questline_DoneQ0,         ArchipelagoLocations.MAIN_QUEST_7),
                new(OutwardQuestEvents.DLC2_Caldera_Questline.DLC2Questline_DoneQ1,         ArchipelagoLocations.MAIN_QUEST_8),
                new(OutwardQuestEvents.DLC2_Caldera_Questline.DLC2Questline_DoneQ2,         ArchipelagoLocations.MAIN_QUEST_9),
                new(OutwardQuestEvents.DLC2_Caldera_Questline.DLC2Questline_DoneQ3,         ArchipelagoLocations.MAIN_QUEST_10),
                new(OutwardQuestEvents.DLC2_Caldera_Questline.DLC2Questline_DoneQ4,         ArchipelagoLocations.MAIN_QUEST_11),
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
                Plugin.Log.LogDebug("QuestEventManager Awake postfix called.");
                RegisterAll();
            }
        }
    }
}
