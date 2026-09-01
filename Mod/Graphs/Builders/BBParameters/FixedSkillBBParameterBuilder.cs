using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs.Builders.BBParameters
{
    /// <summary>
    /// Builds a Skill BBParameter from a fixed Skill ID.
    /// </summary>
    internal class FixedSkillBBParameterBuilder : IBBParameterBuilder<Skill>
    {
        /// <summary>
        /// The skill ID.
        /// </summary>
        public int SkillID { get; set; } = -1;

        public BBParameter<Skill> BuildBBParameter(IGraphPatchContext context)
        {
            return new BBParameterBuilder<Skill>
            {
                Value = ResourcesPrefabManager.Instance.GetItemPrefab(SkillID) as Skill,
            }.BuildBBParameter(context);
        }
    }
}
