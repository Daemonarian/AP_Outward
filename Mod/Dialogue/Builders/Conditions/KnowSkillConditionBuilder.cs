using NodeCanvas.Framework;
using NodeCanvas.Tasks.Conditions;
using OutwardArchipelago.Dialogue.Builders.BBParameters;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{
    /// <summary>
    /// Build a condition that checks whether a specified skill is known.
    /// </summary>
    internal class KnowSkillConditionBuilder : IConditionBuilder
    {
        /// <summary>
        /// The character who should know the skill.
        /// Default is the dialogue instigator.
        /// </summary>
        public IBBParameterBuilder<Character> Character { get; set; } = BBParameterBuilder.Instigator;

        /// <summary>
        /// The skill to check.
        /// </summary>
        public IBBParameterBuilder<Skill> Skill { get; set; } = null;

        /// <summary>
        /// The ID of the skill to check.
        /// Can be used in place of <see cref="Skill"/> to quickly set a fixed skill.
        /// </summary>
        public int SkillID { set => Skill = new FixedSkillBBParameterBuilder { SkillID = value }; }

        /// <summary>
        /// Should the condition be inverted?
        /// </summary>
        public bool IsInverted { get; set; } = false;

        public ConditionTask BuildCondition(IDialoguePatchContext context)
        {
            return new Condition_KnowSkill
            {
                character = Character.BuildBBParameter(context),
                skill = Skill.BuildBBParameter(context),
                invert = IsInverted,
            };
        }
    }
}
