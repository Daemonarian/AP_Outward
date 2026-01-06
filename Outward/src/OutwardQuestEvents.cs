using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using static OTWStoreAPI;

namespace OutwardModTemplate
{
    public static class OutwardQuestEvents
    {
        public static class Neutral_CallToAdventure
        {
            public static string CallToAdventure_Completed = "ZYzrMi1skUiJ4BgXXQ3sfw"; // Call to Adventure quest completion event
        }

        public static class Neutral_General
        {
            public static string General_DoneQuest0 = "P2rqNERqN0O1RhkD1ff7_w"; // Faction commitment quest completion event
            public static string General_DoneQuest1 = "HbTd6ustoU-VhQeidJcAEw"; // First quest completion event
            public static string General_DoneQuest2 = "Wl08NWMJokemPVfTEyT3UA"; // Second quest completion event
            public static string General_DoneQuest3 = "Og71f8G5a0eVmLxZB0yOKg"; // Third quest completion event
            public static string General_DoneQuest4 = "1nGk1TyMbUi3VmSdn32zCg"; // Fourth quest completion event
        }

        public static class DLC2_Caldera_Questline
        {
            public static string DLC2Questline_DoneQ0 = "uEg8NE3nckWyTs_Y7jSTrQ"; // Completed Prequest
            public static string DLC2Questline_DoneQ1 = "kXbeBrICkEWs8GS6YTfKRA"; // Completed first quest
            public static string DLC2Questline_DoneQ2 = "QVkuDH0_nkKwVNaCEy5Zww"; // Completed second quest
            public static string DLC2Questline_DoneQ3 = "hedPnIOK20iOKSVgWk8cFw"; // Completed third quest
            public static string DLC2Questline_DoneQ4 = "gSCCl5ZSXkC-2awJWrCAFw"; // Completed last quest
        }
    }
}
