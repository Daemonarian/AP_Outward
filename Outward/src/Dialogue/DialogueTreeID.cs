using NodeCanvas.DialogueTrees;
using System.Text;

namespace OutwardArchipelago.Dialogue
{
    internal class DialogueTreeID
    {
        // common dialogue trees in Outward
        public static readonly DialogueTreeID Caldera_Evangeline_BaseBuilding = FromName("Dialogue_Caldera_Evangeline_BaseBuilding");
        public static readonly DialogueTreeID Caldera_Josef_BaseBuilding = FromName("Dialogue_Caldera_Josef_BaseBuilding");
        public static readonly DialogueTreeID Caldera_MessengerInn_Q0 = FromName("Dialogue_Caldera_MessengerInn_Q0");
        public static readonly DialogueTreeID Calixa_HK_HeroPeacemaker = FromName("Dialogue_Calixa_HK_HeroPeacemaker");
        public static readonly DialogueTreeID Calixa_HK_MouthToFeed = FromName("Dialogue_Calixa_HK_MouthToFeed");
        public static readonly DialogueTreeID CardinalBourlamaque_HM_HallowPeacemaker = FromName("Dialogue_CardinalBourlamaque_HM_HallowPeacemaker");
        public static readonly DialogueTreeID Cyrene_HK_SandCorsairs = FromName("Dialogue_Cyrene_HK_SandCorsairs");
        public static readonly DialogueTreeID Cyrene_HK_TendTheFlame = FromName("Dialogue_Cyrene_HK_TendTheFlame");
        public static readonly DialogueTreeID Ellinara_HM_Doubts = FromName("Dialogue_Ellinara_HM_Doubts");
        public static readonly DialogueTreeID Ellinara_HM_Questions = FromName("Dialogue_Ellinara_HM_Questions");
        public static readonly DialogueTreeID Ellinara_HM_Truth = FromName("Dialogue_Ellinara_HM_Truth");
        public static readonly DialogueTreeID Merchant_CierzoAlchemist = FromName("Dialogue_Merchant_CierzoAlchemist");
        public static readonly DialogueTreeID RissaAberdeen_BC_AncestralPeacemaker = FromName("Dialogue_RissaAberdeen_BC_AncestralPeacemaker");
        public static readonly DialogueTreeID RissaAberdeen_BC_AshGiant = FromName("Dialogue_RissaAberdeen_BC_AshGiant");
        public static readonly DialogueTreeID RissaAberdeen_BC_MixedLegacies = FromName("Dialogue_RissaAberdeen_BC_MixedLegacies");
        public static readonly DialogueTreeID RissaAberdeen_BC_WhisperingBones = FromName("Dialogue_RissaAberdeen_BC_WhisperingBones");
        public static readonly DialogueTreeID RissaAberdeen_Neut_Prequest = FromName("Dialogue_RissaAberdeen_Neut_Prequest");
        public static readonly DialogueTreeID Soroborean_ArcaneDean_Q1 = FromName("Dialogue_Soroborean_ArcaneDean_Q1");
        public static readonly DialogueTreeID Soroborean_ArcaneDean_Q3 = FromName("Dialogue_Soroborean_ArcaneDean_Q3");
        public static readonly DialogueTreeID Soroborean_ArcaneDean_Q4 = FromName("Dialogue_Soroborean_ArcaneDean_Q4");
        public static readonly DialogueTreeID Soroborean_EngineeringDean_Q1 = FromName("Dialogue_Soroborean_EngineeringDean_Q1");
        public static readonly DialogueTreeID Soroborean_MilitaryDean_Q2 = FromName("Dialogue_Soroborean_MilitaryDean_Q2");
        public static readonly DialogueTreeID Soroborean_MilitaryRecruiter_StartPQ1 = FromName("Dialogue_Soroborean_MilitaryRecruiter_StartPQ1");
        public static readonly DialogueTreeID Soroborean_NaturalistDean_Q1 = FromName("Dialogue_Soroborean_NaturalistDean_Q1");

        /// <summary>
        /// The name of the dialogue tree.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The hash of the dialogue tree.
        /// </summary>
        public ulong? Hash { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogueTreeID"/> class.
        /// </summary>
        /// <param name="name">The human-readable name of the dialogue tree, or <c>null</c> if not specified.</param>
        /// <param name="hash">A deterministic 64-bit hash for the dialogue tree, or <c>null</c> if not specified.</param>
        /// <remarks>
        /// The constructor is private to enforce creation through the provided factory methods:
        /// <see cref="FromNameAndHash"/>, <see cref="FromTree"/>, <see cref="FromName"/>, and <see cref="FromHash"/>.
        /// Instances are effectively immutable after construction because the property setters are private.
        /// </remarks>
        private DialogueTreeID(string name, ulong? hash)
        {
            Name = name;
            Hash = hash;
        }

        public static DialogueTreeID FromNameAndHash(string name, ulong? hash) => new(name, hash);

        public static DialogueTreeID FromTree(DialogueTreeExt tree) => FromNameAndHash(tree.name, HashTree(tree));

        public static DialogueTreeID FromName(string name) => FromNameAndHash(name, null);

        public static DialogueTreeID FromHash(ulong hash) => FromNameAndHash(null, hash);

        /// <summary>
        /// Computes a deterministic hash value for the specified dialogue tree.
        /// </summary>
        /// <remarks>This method can be used to uniquely identify dialogue trees for caching, comparison,
        /// or integrity checks. The hash is calculated using the FNV-1a algorithm over the UTF-8 encoding of the tree's
        /// name and serialized graph.</remarks>
        /// <param name="tree">The dialogue tree to hash. Must not be null.</param>
        /// <returns>A 64-bit unsigned integer representing the hash of the dialogue tree. The value is consistent Unity deserialized
        /// dialogue trees.</returns>
        public static ulong HashTree(DialogueTreeExt tree)
        {
            var str = $"{tree.name}: {tree._serializedGraph}";
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            ulong hash = 0xcbf29ce484222325;
            ulong prime = 0x100000001b3;

            unchecked
            {
                foreach (byte b in bytes)
                {
                    hash ^= b;
                    hash *= prime;
                }
            }

            return hash;
        }

        /// <summary>
        /// Compares this dialogue tree ID with another with some soft-equality rules.
        /// </summary>
        /// <param name="other">The other dialogue tree ID. Must not be null.</param>
        /// <returns>Whether the two IDs match.</returns>
        public bool Matches(DialogueTreeID other)
        {
            if (Name != null && other.Name != null && Name != other.Name) return false;
            if (Hash != null && other.Hash != null && Hash != other.Hash) return false;
            return true;
        }

        /// <summary>
        /// Determines whether the specified dialogue tree matches the criteria defined by this instance.
        /// </summary>
        /// <param name="tree">The dialogue tree to evaluate against the matching criteria. Cannot be null.</param>
        /// <returns>true if the dialogue tree matches the criteria; otherwise, false.</returns>
        public bool Matches(DialogueTreeExt tree) => Matches(FromTree(tree));

        public override string ToString() => $"\"{Name}\" ({Hash:X16})";
    }
}
