from dataclasses import dataclass
from Options import Choice, DeathLink, DefaultOnToggle, Option, PerGameCommonOptions

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

class WindAltarChecksOption(DefaultOnToggle):
    """
    Make the Cabal of Wind Altars for each region location checks, and add their
    corresponding boons to the item pool.
    """

    display_name = "Wind Alter checks"

@dataclass
class OutwardOptions(PerGameCommonOptions):
    death_link: DeathLink
    skill_sanity: SkillSanityChoice
    wind_altar_checks: WindAltarChecksOption
