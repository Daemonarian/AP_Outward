from dataclasses import dataclass
from Options import Choice, DeathLink, DefaultOnToggle, PerGameCommonOptions, Range, Toggle

class GoalOption(Choice):
    """
    Choose which goal should count as completing your game. For a first
    playthrough, we recommend *Main Quest 7* which marks the end of the
    original Outward story line with one of the *Peacemaker* quests or the
    *A House Divided* quest.
    - **Main Quest N:** Complete the *n*-th main quest in Outward: Definitive
    Edition. *Main quest 7* is the end of the original story line, and *Main
    quest 12* is the end of the Definitive Edition story line. These could be
    different quests depending on which faction you choose, but all factions
    have the same number of quests.
    - **Blood Under the Sun Quest:** Complete the parallel quest *Blood Under
    the Sun* by finding the culprit behind the assassination of Prince Jaden.
    - **Purifier Quest:** Complete the parallel quest *Purifier* by
    investigating rumors of trouble in the *Hallowed Marsh*.
    - **Vendavel Quest:** Complete the parallel quest *Vendavel Quest* by
    preventing the destruction of *Cierzo*.
    - **Rust and Vengeance:** Complete the parallel quest *Rust and Vengeance*
    by foiling the *Rust Lich*'s plans to destroy Harmattan.
    """

    display_name = "Goal"

    option_main_quest_1 = 0
    option_main_quest_2 = 1
    option_main_quest_3 = 2
    option_main_quest_4 = 3
    option_main_quest_5 = 4
    option_main_quest_6 = 5
    option_main_quest_7 = 6
    option_main_quest_8 = 7
    option_main_quest_9 = 8
    option_main_quest_10 = 9
    option_main_quest_11 = 10
    option_main_quest_12 = 11

    option_blood_under_the_sun_quest = 12
    option_purifier_quest = 13
    option_vendavel_quest = 14
    option_rust_and_vengeance_quest = 15

    default = 6

class SkillSanityChoice(Choice):
    """
    Randomize the selection provided by skill trainers.
    - **Vanilla:** Trainers provide the same selection as in the base game.
    - **Tier One Only:** Tier one skills are added to the general item pool.
    - **Full:** All skills are added to the general item pool.
    """

    display_name = "SkillSanity"

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

class NumBreakthoughPointsOption(Range):
    """
    Set the number of available breakthrough points.
    A value of 11 means that all skill trainer skills are potentially available.
    """

    display_name = "Number of Available Breakthough Points"

    range_start = 0
    range_end = 11

    default = 3

class BreakthroughPointChecksOption(Toggle):
    """
    Add breakthough points to the item pool, as well as additional checks for
    interacting with each skill trainer.
    """

    display_name = "Breakthough Point checks"

@dataclass
class OutwardOptions(PerGameCommonOptions):
    death_link: DeathLink
    goal: GoalOption
    skillsanity: SkillSanityChoice
    wind_altar_checks: WindAltarChecksOption
    num_breakthough_points: NumBreakthoughPointsOption
    breakthrough_point_checks: BreakthroughPointChecksOption
