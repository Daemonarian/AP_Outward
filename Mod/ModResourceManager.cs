using System.Collections.Generic;
using NodeCanvas.DialogueTrees;
using SideLoader;

namespace OutwardArchipelago
{
    internal static class ModResourceManager
    {
        public static readonly IReadOnlyDictionary<int, DialogueInfo> ItemToDialogueInfo = new Dictionary<int, DialogueInfo>
        {
            { OutwardItem.BalthyNote, new("Note", "{\"version\":2.92,\"type\":\"NodeCanvas.DialogueTrees.DialogueTreeExt\",\"translation\":{\"x\":-3953.0,\"y\":-4154.0},\"nodes\":[{\"_action\":{\"actions\":[{\"QuestEventRef\":{\"m_eventUID\":\"e3S0tVthEkey8L0oI4f8Wg\"},\"$type\":\"NodeCanvas.Tasks.Actions.SendQuestEvent\"},{\"QuestEventRef\":{\"m_eventUID\":\"75jOpzbxWk6tLFZ1W04nPg\"},\"$type\":\"NodeCanvas.Tasks.Actions.SendQuestEvent\"}],\"$type\":\"NodeCanvas.Framework.ActionList\"},\"_position\":{\"x\":4320.0,\"y\":3690.0},\"$type\":\"NodeCanvas.DialogueTrees.ActionNode\",\"$id\":\"2\"},{\"statement\":{\"_text\":\"(This fallen note seems to contain orders for the attack on Berg. Perhaps it holds a clue for what they did with the hostages.)\",\"_meta\":\"NecroNote_BC_Mix_Legacy_00\"},\"_actorName\":\"name_unpc_narrator\",\"_actorParameterID\":\"40fbaef3-4bf8-4bc9-bf2f-ae0d25ded760\",\"_position\":{\"x\":4245.0,\"y\":3810.0},\"$type\":\"NodeCanvas.DialogueTrees.StatementNodeExt\",\"$id\":\"9\"},{\"statement\":{\"_text\":\"Your first priority is the destruction of the archives room under the Blue Chamber. If you cannot ensure complete destruction of the room, at least ensure that it will be inaccessible for as much time as possible.\",\"_meta\":\"NecroNote_BC_Mix_Legacy_01\"},\"_actorName\":\"name_unpc_cluenote_01\",\"_actorParameterID\":\"ce823e8a-bbcb-4e05-9a6c-64a181ffbf74\",\"_position\":{\"x\":4170.0,\"y\":3930.0},\"$type\":\"NodeCanvas.DialogueTrees.StatementNodeExt\",\"$id\":\"11\"},{\"statement\":{\"_text\":\"Likewise, Alda, the ambassador representing Old Levant, is to be killed on sight. Use as much ammunition as you need to. She must not survive this meeting.\",\"_meta\":\"NecroNote_BC_Mix_Legacy_02\"},\"_actorName\":\"name_unpc_cluenote_01\",\"_actorParameterID\":\"ce823e8a-bbcb-4e05-9a6c-64a181ffbf74\",\"_position\":{\"x\":4155.0,\"y\":4050.0},\"$type\":\"NodeCanvas.DialogueTrees.StatementNodeExt\",\"$id\":\"13\"},{\"statement\":{\"_text\":\"If possible, capture and question the Blue Chamber historian, <color=#00CCFF><b>Balthazar Berthelot</b></color>. If he shows any sign of supporting Old Levant’s position, kill him. Use <color=#FF0000><b>the caves within the Old Burial Grounds</b></color>. There are plenty of animals that will make short work of any evidence if he needs to be disposed of.\",\"_meta\":\"NecroNote_BC_Mix_Legacy_03\"},\"_actorName\":\"name_unpc_cluenote_01\",\"_actorParameterID\":\"ce823e8a-bbcb-4e05-9a6c-64a181ffbf74\",\"_position\":{\"x\":4110.0,\"y\":4155.0},\"$type\":\"NodeCanvas.DialogueTrees.StatementNodeExt\",\"$id\":\"15\"},{\"statement\":{\"_text\":\"If the opportunity arises to kidnap a chamberlord, do so. Hold them for ransom, do not kill them unless it’s absolutely necessary. The <color=#FF0000><b>old cabin in the burned woods</b></color> to the south are equipped and prepared for such an event.\",\"_meta\":\"NecroNote_BC_Mix_Legacy_04\"},\"_actorName\":\"name_unpc_cluenote_01\",\"_actorParameterID\":\"ce823e8a-bbcb-4e05-9a6c-64a181ffbf74\",\"_position\":{\"x\":4080.0,\"y\":4290.0},\"$type\":\"NodeCanvas.DialogueTrees.StatementNodeExt\",\"$id\":\"17\"},{\"_condition\":{\"QuestEventRef\":{\"m_eventUID\":\"SV3XUncnQU60ozhj9xTRpg\"},\"$type\":\"NodeCanvas.Tasks.Conditions.Condition_QuestEventOccured\"},\"_position\":{\"x\":4125.0,\"y\":4425.0},\"$type\":\"NodeCanvas.DialogueTrees.ConditionNode\",\"$id\":\"19\"},{\"statement\":{\"_text\":\"If all goes well and you have the opportunity to capture Kirouac as well, do so. He is to be kept alive at all costs. Once war has been declared, he is to be returned home unharmed.\",\"_meta\":\"NecroNote_BC_Mix_Legacy_05\"},\"_actorName\":\"name_unpc_cluenote_01\",\"_actorParameterID\":\"ce823e8a-bbcb-4e05-9a6c-64a181ffbf74\",\"_position\":{\"x\":3915.0,\"y\":4590.0},\"$type\":\"NodeCanvas.DialogueTrees.StatementNodeExt\",\"$id\":\"22\"},{\"_position\":{\"x\":4200.0,\"y\":5040.0},\"$type\":\"NodeCanvas.DialogueTrees.FinishNode\",\"$id\":\"24\"},{\"statement\":{\"_text\":\"All messages are to go through “Commander Mantis” at his <color=#FF0000><b>forest camp near the old Cabal of Winds Temple</b></color>. After his departure, all agents are to stick to their instructions to the letter.\",\"_meta\":\"NecroNote_BC_Mix_Legacy_06\"},\"_actorName\":\"name_unpc_cluenote_01\",\"_actorParameterID\":\"ce823e8a-bbcb-4e05-9a6c-64a181ffbf74\",\"_position\":{\"x\":4245.0,\"y\":4710.0},\"$type\":\"NodeCanvas.DialogueTrees.StatementNodeExt\",\"$id\":\"25\"}],\"connections\":[{\"_sourceNode\":{\"$ref\":\"2\"},\"_targetNode\":{\"$ref\":\"9\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"9\"},\"_targetNode\":{\"$ref\":\"11\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"11\"},\"_targetNode\":{\"$ref\":\"13\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"13\"},\"_targetNode\":{\"$ref\":\"15\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"15\"},\"_targetNode\":{\"$ref\":\"17\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"17\"},\"_targetNode\":{\"$ref\":\"19\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"19\"},\"_targetNode\":{\"$ref\":\"22\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"19\"},\"_targetNode\":{\"$ref\":\"25\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"22\"},\"_targetNode\":{\"$ref\":\"24\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"},{\"_sourceNode\":{\"$ref\":\"25\"},\"_targetNode\":{\"$ref\":\"24\"},\"$type\":\"NodeCanvas.DialogueTrees.DTConnection\"}],\"localBlackboard\":{\"_name\":\"Local Blackboard\",\"_variables\":{\"gInstigator\":{\"_name\":\"gInstigator\",\"_id\":\"0483cf6b-da62-4a57-aaed-40f25b4bdf9b\",\"$type\":\"NodeCanvas.Framework.Variable`1[[Character, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]\"}}},\"derivedData\":{\"actorParameters\":[{\"_keyName\":\"name_unpc_narrator\",\"_id\":\"40fbaef3-4bf8-4bc9-bf2f-ae0d25ded760\",\"_actorObject\":1},{\"_keyName\":\"name_unpc_cluenote_01\",\"_id\":\"ce823e8a-bbcb-4e05-9a6c-64a181ffbf74\",\"_actorObject\":2}],\"$type\":\"NodeCanvas.DialogueTrees.DialogueTree+DerivedSerializationData\"}}") },
        };

        public static void Init()
        {
            SL.OnPacksLoaded += Setup;
            if (SL.PacksLoaded)
            {
                Setup();
            }
        }

        public static void Setup()
        {
            foreach (var pair in ItemToDialogueInfo)
            {
                var notePrefab = ResourcesPrefabManager.Instance.GetItemPrefab(pair.Key);

                var dialogueActor = notePrefab.GetComponent<DialogueActor>();
                dialogueActor._name = pair.Value.ActorName;

                var dialogueController = notePrefab.GetComponent<DialogueTreeController>();
                dialogueController.initialized = false;
                dialogueController.graph = null;
                dialogueController.boundGraphSerialization = pair.Value.SerializedGraph;
            }
        }

        public class DialogueInfo
        {
            private readonly string _actorName;
            private readonly string _serializedGraph;

            public DialogueInfo(string actorName, string serializedGraph)
            {
                _actorName = actorName;
                _serializedGraph = serializedGraph;
            }

            public string ActorName => _actorName;
            public string SerializedGraph => _serializedGraph;
        }
    }
}
