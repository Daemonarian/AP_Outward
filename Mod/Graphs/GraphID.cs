using System.Text;

namespace OutwardArchipelago.Graphs
{
    internal class GraphID
    {
        // common dialogue trees in Outward
        public static readonly GraphID Any = new(null, null);

        public static readonly GraphID Abrassar_Immaculate_Real = FromName("Dialogue_Abrassar_Immaculate_Real");
        public static readonly GraphID AbrassPillar_Neut_COW = FromName("Dialogue_AbrassPillar_Neut_COW");
        public static readonly GraphID AcquireManaBerg = FromPath("Berg/_SNPC/_DLC2_Interactions/EnableOnDLC/Auto-Hails/UNPC_DLC_Auto-Berg-Guard-Hail/DialogueTemplate/NPC/DialogueTree");
        public static readonly GraphID AcquireManaConflux = FromPath("Chersonese_Dungeon4_CommonPath/Interactions/SkillProposition/DialogueTemplate/NPC/DialogueTree");
        public static readonly GraphID AcquireManaSorobor = FromPath("Harmattan/Interactions/NPCs/NPC_Minor/UNPC_DLC_LeyLine_SkillProposition/DialogueTemplate/NPC/DialogueTree");
        public static readonly GraphID AntiqueField_Immaculate_Real = FromName("Dialogue_AntiqueField_Immaculate_Real");
        public static readonly GraphID Caldera_Evangeline_BaseBuilding = FromName("Dialogue_Caldera_Evangeline_BaseBuilding");
        public static readonly GraphID Caldera_Immaculate_Real = FromName("Dialogue_Caldera_Immaculate_Real");
        public static readonly GraphID Caldera_Josef_BaseBuilding = FromName("Dialogue_Caldera_Josef_BaseBuilding");
        public static readonly GraphID Caldera_MessengerInn_Q0 = FromName("Dialogue_Caldera_MessengerInn_Q0");
        public static readonly GraphID CalderaPillar_Neut_COW = FromName("Dialogue_CalderaPillar_Neut_COW");
        public static readonly GraphID Calixa_HK_HeroPeacemaker = FromName("Dialogue_Calixa_HK_HeroPeacemaker");
        public static readonly GraphID Calixa_HK_MouthToFeed = FromName("Dialogue_Calixa_HK_MouthToFeed");
        public static readonly GraphID Calixa_Neut_Prequest = FromName("Dialogue_Calixa_Neut_Prequest");
        public static readonly GraphID CardinalBourlamaque_HM_HallowPeacemaker = FromName("Dialogue_CardinalBourlamaque_HM_HallowPeacemaker");
        public static readonly GraphID Chersonese_Cierzo_BuracCarillon_Real = FromName("Dialogue_Chersonese_Cierzo_BuracCarillon_Real");
        public static readonly GraphID Chersonese_Immaculate_Real = FromName("Dialogue_Chersonese_Immaculate_Real");
        public static readonly GraphID ChersPillar_Neut_COW = FromName("Dialogue_ChersPillar_Neut_COW");
        public static readonly GraphID Cierzo_HelenTurnbull_Real = FromName("Dialogue_Cierzo_HelenTurnbull_Real");
        public static readonly GraphID Cyrene_HK_SandCorsairs = FromName("Dialogue_Cyrene_HK_SandCorsairs");
        public static readonly GraphID Cyrene_HK_TendTheFlame = FromName("Dialogue_Cyrene_HK_TendTheFlame");
        public static readonly GraphID ElattAltar_Neut_Prequest = FromName("Dialogue_ElattAltar_Neut_Prequest");
        public static readonly GraphID Ellinara_HM_Doubts = FromName("Dialogue_Ellinara_HM_Doubts");
        public static readonly GraphID Ellinara_HM_Questions = FromName("Dialogue_Ellinara_HM_Questions");
        public static readonly GraphID Ellinara_HM_Truth = FromName("Dialogue_Ellinara_HM_Truth");
        public static readonly GraphID Emercar_Immaculate_Real = FromName("Dialogue_Emercar_Immaculate_Real");
        public static readonly GraphID EnmerkarPillar_Neut_COW = FromName("Dialogue_EnmerkarPillar_Neut_COW");
        public static readonly GraphID GoldLich_Neut_Initial = FromName("Dialogue_GoldLich_Neut_Initial");
        public static readonly GraphID HallowedMarsh_Immaculate_Real = FromName("Dialogue_HallowedMarsh_Immaculate_Real");
        public static readonly GraphID HarmattanPillar_Neut_COW = FromName("Dialogue_HarmattanPillar_Neut_COW");
        public static readonly GraphID JadeLich_Neut_Initial = FromName("Dialogue_JadeLich_Neut_Initial");
        public static readonly GraphID MarshPillar_Neut_COW = FromName("Dialogue_MarshPillar_Neut_COW");
        public static readonly GraphID Merchant_BergAlchemist = FromName("Dialogue_Merchant_BergAlchemist");
        public static readonly GraphID Merchant_BergBlacksmith = FromName("Dialogue_Merchant_BergBlacksmith");
        public static readonly GraphID Merchant_BergFoodStore = FromName("Dialogue_Merchant_BergFoodStore");
        public static readonly GraphID Merchant_BergGeneralStore = FromName("Dialogue_Merchant_BergGeneralStore");
        public static readonly GraphID Merchant_BergKaziteAssassin = FromName("Dialogue_Merchant_BergKaziteAssassin");
        public static readonly GraphID Merchant_CierzoAlchemist = FromName("Dialogue_Merchant_CierzoAlchemist");
        public static readonly GraphID Merchant_CierzoBlacksmith = FromName("Dialogue_Merchant_CierzoBlacksmith");
        public static readonly GraphID Merchant_CierzoFishmongerA = FromName("Dialogue_Merchant_CierzoFishmongerA");
        public static readonly GraphID Merchant_CierzoGeneralStore = FromName("Dialogue_Merchant_CierzoGeneralStore");
        public static readonly GraphID Merchant_HarmattanArmor = FromName("Dialogue_Merchant_HarmattanArmor");
        public static readonly GraphID Merchant_HarmattanBlacksmith = FromName("Dialogue_Merchant_HarmattanBlacksmith");
        public static readonly GraphID Merchant_HarmattanCamp = FromName("Dialogue_Merchant_HarmattanCamp");
        public static readonly GraphID Merchant_HarmattanFood = FromName("Dialogue_Merchant_HarmattanFood");
        public static readonly GraphID Merchant_HarmattanGeneral = FromName("Dialogue_Merchant_HarmattanGeneral");
        public static readonly GraphID Merchant_HarmattanWeapons = FromName("Dialogue_Merchant_HarmattanWeapons");
        public static readonly GraphID Merchant_LevantBlacksmith = FromName("Dialogue_Merchant_LevantBlacksmith");
        public static readonly GraphID Merchant_LevantFoodStore = FromName("Dialogue_Merchant_LevantFoodStore");
        public static readonly GraphID Merchant_LevantGeneralStore = FromName("Dialogue_Merchant_LevantGeneralStore");
        public static readonly GraphID Merchant_MonsoonBlacksmith = FromName("Dialogue_Merchant_MonsoonBlacksmith");
        public static readonly GraphID Merchant_MonsoonGeneralStore = FromName("Dialogue_Merchant_MonsoonGeneralStore");
        public static readonly GraphID PrisonerD_Neut_Vendavel = FromName("Dialogue_PrisonerD_Neut_Vendavel");
        public static readonly GraphID Purifier_MercantileProvost = FromName("Dialogue_Purifier_MercantileProvost");
        public static readonly GraphID RissaAberdeen_BC_AncestralPeacemaker = FromName("Dialogue_RissaAberdeen_BC_AncestralPeacemaker");
        public static readonly GraphID RissaAberdeen_BC_AshGiant = FromName("Dialogue_RissaAberdeen_BC_AshGiant");
        public static readonly GraphID RissaAberdeen_BC_MixedLegacies = FromName("Dialogue_RissaAberdeen_BC_MixedLegacies");
        public static readonly GraphID RissaAberdeen_BC_WhisperingBones = FromName("Dialogue_RissaAberdeen_BC_WhisperingBones");
        public static readonly GraphID RissaAberdeen_Neut_Prequest = FromName("Dialogue_RissaAberdeen_Neut_Prequest");
        public static readonly GraphID SagardBattleborn_BC_AncestralPeacemaker = FromName("Dialogue_SagardBattleborn_BC_AncestralPeacemaker");
        public static readonly GraphID SagardBattleborn_BC_MixedLegacies = FromName("Dialogue_SagardBattleborn_BC_MixedLegacies");
        public static readonly GraphID SagardBattleborn_BC_WhisperingBones = FromName("Dialogue_SagardBattleborn_BC_WhisperingBones");
        public static readonly GraphID SagardBattleborn_HM_HallowPeacemaker = FromName("Dialogue_SagardBattleborn_HM_HallowPeacemaker");
        public static readonly GraphID SagardBattleborn_Neut_Initial = FromName("Dialogue_SagardBattleborn_Neut_Initial");
        public static readonly GraphID SagardBattleborn_Neut_Prequest = FromName("Dialogue_SagardBattleborn_Neut_Prequest");
        public static readonly GraphID SagardBattleborn_Neut_Purifier = FromName("Dialogue_SagardBattleborn_Neut_Purifier");
        public static readonly GraphID SimeonKing_HK_HeroPeacemaker = FromName("Dialogue_SimeonKing_HK_HeroPeacemaker");
        public static readonly GraphID SimeonKing_HK_MouthToFeed = FromName("Dialogue_SimeonKing_HK_MouthToFeed");
        public static readonly GraphID SimeonKing_HK_SandCorsair = FromName("Dialogue_SimeonKing_HK_SandCorsair");
        public static readonly GraphID SimeonKing_HK_TendFlame = FromName("Dialogue_SimeonKing_HK_TendFlame");
        public static readonly GraphID SimeonKing_HM_HallowPeacemaker = FromName("Dialogue_SimeonKing_HM_HallowPeacemaker");
        public static readonly GraphID SimeonKing_Neut_Initial = FromName("Dialogue_SimeonKing_Neut_Initial");
        public static readonly GraphID SimeonKing_Neut_Prequest = FromName("Dialogue_SimeonKing_Neut_Prequest");
        public static readonly GraphID Soeran_Neut_Initial = FromName("Dialogue_Soeran_Neut_Initial");
        public static readonly GraphID Soeran_Neut_Prequest = FromName("Dialogue_Soeran_Neut_Prequest");
        public static readonly GraphID Soroborean_ArcaneDean_Q1 = FromName("Dialogue_Soroborean_ArcaneDean_Q1");
        public static readonly GraphID Soroborean_ArcaneDean_Q3 = FromName("Dialogue_Soroborean_ArcaneDean_Q3");
        public static readonly GraphID Soroborean_ArcaneDean_Q4 = FromName("Dialogue_Soroborean_ArcaneDean_Q4");
        public static readonly GraphID Soroborean_BloodMageTrigger = FromName("Dialogue_Soroborean_BloodMageTrigger");
        public static readonly GraphID Soroborean_EngineeringDean_Q1 = FromName("Dialogue_Soroborean_EngineeringDean_Q1");
        public static readonly GraphID Soroborean_HeadMaster_PQ1 = FromName("Dialogue_Soroborean_HeadMaster_PQ1");
        public static readonly GraphID Soroborean_LichDying = FromName("Dialogue_Soroborean_LichDying");
        public static readonly GraphID Soroborean_MilitaryDean_Q2 = FromName("Dialogue_Soroborean_MilitaryDean_Q2");
        public static readonly GraphID Soroborean_MilitaryRecruiter_StartPQ1 = FromName("Dialogue_Soroborean_MilitaryRecruiter_StartPQ1");
        public static readonly GraphID Soroborean_NaturalistDean_Q1 = FromName("Dialogue_Soroborean_NaturalistDean_Q1");
        public static readonly GraphID TreasureHuntFinal = FromPath("AbrassarDungeonsSmall/Environment/AssetsSmallDungeons/7-DockStorage/Interactions/TsarStone/mdl_env_propBronzeClawSmall_c (21)/6200010_Tsar_v/DialogueTemplate_Tsar/NPC/DialogueTree");

        /// <summary>
        /// The path of the graph owner in the scene hierarchy.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The name of the dialogue tree.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphID"/> class.
        /// </summary>
        /// <param name="path">The path of the graph owner in the scene hierarchy.</param>
        /// <param name="name">The human-readable name of the dialogue tree, or <c>null</c> if not specified.</param>
        /// <remarks>
        /// The constructor is private to enforce creation through the provided factory methods:
        /// <see cref="FromPathAndName"/>, <see cref="FromContext"/>, <see cref="FromPath"/>, and <see cref="FromName"/>.
        /// Instances are effectively immutable after construction because the property setters are private.
        /// </remarks>
        private GraphID(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public static GraphID FromPathAndName(string path, string name) => new(path, name);

        public static GraphID FromContext(IGraphPatchContext context)
        {
            OutwardArchipelagoMod.Log.LogDebug($"Creating GraphID from context: Path='{context.Path}', Name='{context.Name}', Hash='{HashTree(context)}'");
            return FromPathAndName(context.Path, context.Name);
        }

        public static GraphID FromPath(string path) => FromPathAndName(path, null);

        public static GraphID FromName(string name) => FromPathAndName(null, name);

        public static GraphID FromHash(ulong hash) => FromPathAndName(null, null); // Placeholder for hash-based lookup, as the actual mapping is not provided.

        /// <summary>
        /// Computes a deterministic hash value for the specified dialogue tree.
        /// </summary>
        /// <remarks>This method can be used to uniquely identify dialogue trees for caching, comparison,
        /// or integrity checks. The hash is calculated using the FNV-1a algorithm over the UTF-8 encoding of the tree's
        /// name and serialized graph.</remarks>
        /// <param name="tree">The dialogue tree to hash. Must not be null.</param>
        /// <returns>A 64-bit unsigned integer representing the hash of the dialogue tree. The value is consistent Unity deserialized
        /// dialogue trees.</returns>
        public static ulong HashTree(IGraphPatchContext context)
        {
            var graph = context.Graph;
            var str = $"{graph.name}: {graph._serializedGraph}";
            var bytes = Encoding.UTF8.GetBytes(str);

            var hash = 0xcbf29ce484222325;
            ulong prime = 0x100000001b3;

            unchecked
            {
                foreach (var b in bytes)
                {
                    hash ^= b;
                    hash *= prime;
                }
            }

            return hash;
        }

        /// <summary>
        /// Compares this graph ID with another with some soft-equality rules.
        /// </summary>
        /// <param name="other">The other graph ID. Must not be null.</param>
        /// <returns>Whether the two IDs match.</returns>
        public bool Matches(GraphID other)
        {
            if (Path != null && other.Path != null && Path != other.Path) return false;
            if (Name != null && other.Name != null && Name != other.Name) return false;
            return true;
        }

        /// <summary>
        /// Determines whether the specified graph matches the criteria defined by this instance.
        /// </summary>
        /// <param name="context">The graph context to evaluate against the matching criteria. Cannot be null.</param>
        /// <returns>true if the graph matches the criteria; otherwise, false.</returns>
        public bool Matches(IGraphPatchContext context) => Matches(FromContext(context));

        public override string ToString() => $"{Path ?? "**"}/{Name ?? "*"}";
    }
}
