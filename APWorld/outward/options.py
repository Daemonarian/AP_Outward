from dataclasses import dataclass
from Options import Choice, DeathLink, Option, PerGameCommonOptions

class SkillSanityChoice(Choice):
    """
    Randomize the selection provided by skill trainers.
    - **Vanilla:** Trainers provide the same selection as in the base game.
    - **Tier One Only:** Tier one skills are added to the general item pool.
    - **Full:** All skills are added to the general item pool.
    """

    display_name = "Skill-Sanity"

    option_vanilla = 0
    option_tier_one_only = 1
    option_full = 2

    default = 2

@dataclass
class OutwardOptions(PerGameCommonOptions):
    death_link: DeathLink
    skill_sanity: SkillSanityChoice
