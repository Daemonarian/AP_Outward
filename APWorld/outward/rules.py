r"""
Contains all the logic pertaining accessibility rules for Outward Archipelago.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

from .events import OutwardEventName, OutwardEventGroup
from .locations import OutwardLocationName, OutwardLocationGroup
from .items import OutwardItemName, OutwardItemGroup

if TYPE_CHECKING:
    from . import OutwardWorld

def add_world_rules(world: OutwardWorld) -> None:
    r"""
    Add all rules to the world.
    """
    
    # main quest events
        
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_01_START, OutwardEventName.MAIN_QUEST_01_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_02_START, OutwardEventName.MAIN_QUEST_02_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_START, OutwardEventName.MAIN_QUEST_03_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_04_START, OutwardEventName.MAIN_QUEST_04_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_05_START, OutwardEventName.MAIN_QUEST_05_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_06_START, OutwardEventName.MAIN_QUEST_06_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_07_START, OutwardEventName.MAIN_QUEST_07_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_08_START, OutwardEventName.MAIN_QUEST_08_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_09_START, OutwardEventName.MAIN_QUEST_09_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_10_START, OutwardEventName.MAIN_QUEST_10_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_11_START, OutwardEventName.MAIN_QUEST_11_PREREQ)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_12_START, OutwardEventName.MAIN_QUEST_12_PREREQ)
        
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_01_COMPLETE, OutwardEventName.MAIN_QUEST_01_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_02_COMPLETE, OutwardEventName.MAIN_QUEST_02_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_COMPLETE, OutwardEventName.MAIN_QUEST_03_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_04_COMPLETE, OutwardEventName.MAIN_QUEST_04_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_05_COMPLETE, OutwardEventName.MAIN_QUEST_05_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_06_COMPLETE, OutwardEventName.MAIN_QUEST_06_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_07_COMPLETE, OutwardEventName.MAIN_QUEST_07_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_08_COMPLETE, OutwardEventName.MAIN_QUEST_08_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_09_COMPLETE, OutwardEventName.MAIN_QUEST_09_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_10_COMPLETE, OutwardEventName.MAIN_QUEST_10_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_11_COMPLETE, OutwardEventName.MAIN_QUEST_11_START)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_12_COMPLETE, OutwardEventName.MAIN_QUEST_12_START)
        
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_02_PREREQ, OutwardEventName.MAIN_QUEST_01_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_PREREQ, OutwardEventName.MAIN_QUEST_02_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_04_PREREQ, OutwardEventName.MAIN_QUEST_03_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_05_PREREQ, OutwardEventName.MAIN_QUEST_04_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_06_PREREQ, OutwardEventName.MAIN_QUEST_05_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_07_PREREQ, OutwardEventName.MAIN_QUEST_06_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_08_PREREQ, OutwardEventName.MAIN_QUEST_07_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_09_PREREQ, OutwardEventName.MAIN_QUEST_08_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_10_PREREQ, OutwardEventName.MAIN_QUEST_09_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_11_PREREQ, OutwardEventName.MAIN_QUEST_10_COMPLETE)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_12_PREREQ, OutwardEventName.MAIN_QUEST_11_COMPLETE)
        
    # quest licenses

    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_PREREQ, OutwardItemName.QUEST_LICENSE, 1)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_04_PREREQ, OutwardItemName.QUEST_LICENSE, 2)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_05_PREREQ, OutwardItemName.QUEST_LICENSE, 3)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_06_PREREQ, OutwardItemName.QUEST_LICENSE, 4)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_07_PREREQ, OutwardItemName.QUEST_LICENSE, 5)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_08_PREREQ, OutwardItemName.QUEST_LICENSE, 6)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_09_PREREQ, OutwardItemName.QUEST_LICENSE, 7)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_10_PREREQ, OutwardItemName.QUEST_LICENSE, 8)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_11_PREREQ, OutwardItemName.QUEST_LICENSE, 9)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_12_PREREQ, OutwardItemName.QUEST_LICENSE, 10)

    # quest completion events

    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_01, OutwardEventName.MAIN_QUEST_01_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_02, OutwardEventName.MAIN_QUEST_02_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_03, OutwardEventName.MAIN_QUEST_03_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_04, OutwardEventName.MAIN_QUEST_04_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_05, OutwardEventName.MAIN_QUEST_05_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_06, OutwardEventName.MAIN_QUEST_06_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_07, OutwardEventName.MAIN_QUEST_07_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_08, OutwardEventName.MAIN_QUEST_08_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_09, OutwardEventName.MAIN_QUEST_09_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_10, OutwardEventName.MAIN_QUEST_10_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_11, OutwardEventName.MAIN_QUEST_11_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_12, OutwardEventName.MAIN_QUEST_12_COMPLETE)

    world.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_BLOOD_UNDER_THE_SUN, OutwardEventName.MAIN_QUEST_04_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_PURIFIER, OutwardEventName.MAIN_QUEST_02_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_VENDAVEL_QUEST, OutwardEventName.MAIN_QUEST_02_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_1, OutwardEventName.MAIN_QUEST_04_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_2, OutwardEventName.MAIN_QUEST_04_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_3, OutwardEventName.MAIN_QUEST_04_COMPLETE)

    # Looking to the Future

    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_START, OutwardEventName.MAIN_QUEST_03_PREREQ, do_require_exists=False)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_ENROLLMENT_START, OutwardEventName.MAIN_QUEST_03_PREREQ, do_require_exists=False)

    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_START, OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_START, do_require_exists=False)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_START, OutwardEventName.MAIN_QUEST_ENROLLMENT_START, do_require_exists=False)
        
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_BC_COMPLETE, OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_START, do_require_exists=False)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_HK_COMPLETE, OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_START, do_require_exists=False)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_HM_COMPLETE, OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_START, do_require_exists=False)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_ENROLLMENT_COMPLETE, OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_START, do_require_exists=False)

    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_COMPLETE, OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_BC_COMPLETE, do_require_exists=False)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_COMPLETE, OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_HK_COMPLETE, do_require_exists=False)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_COMPLETE, OutwardEventName.MAIN_QUEST_LOOKING_TO_THE_FUTURE_HM_COMPLETE, do_require_exists=False)
    world.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_COMPLETE, OutwardEventName.MAIN_QUEST_ENROLLMENT_COMPLETE, do_require_exists=False)

    # useful items

    world.add_location_item_requirement(OutwardLocationName.SPAWN_ANGLER_SHIELD, OutwardItemName.SLUMBERING_SHIELD)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_BRAND, OutwardItemName.STRANGE_RUSTED_SWORD)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_DISTORTED_EXPERIMENT, OutwardItemName.EXPERIMENTAL_CHAKRAM)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_DUTY, OutwardItemName.RUINED_HALBERD)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_GEPS_BLADE, OutwardItemName.MYSTERIOUS_LONG_BLADE)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_GHOST_PARALLEL, OutwardItemName.DEPOWERED_BLUDGEON)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_GILDED_SHIVER_OF_TRAMONTANE, OutwardItemName.SCARRED_DAGGER)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_GRIND, OutwardItemName.FOSSILIZED_GREATAXE)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_MURMURE, OutwardItemName.CEREMONIAL_BOW)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_PEARLESCENT_MAIL, OutwardEventName.MAIN_QUEST_05_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_RED_LADYS_DAGGER, OutwardItemName.SCARLET_LICHS_IDOL)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_REVENANT_MOON, OutwardItemName.CRACKED_RED_MOON)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_REVENANT_MOON, OutwardItemName.SCARLET_GEM)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_ROTWOOD_STAFF, OutwardEventName.MAIN_QUEST_06_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_ROTWOOD_STAFF, OutwardItemName.COMPASSWOOD_STAFF)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_SANDROSE, OutwardItemName.WARM_AXE)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_SCARLET_GEM, OutwardEventName.MAIN_QUEST_09_COMPLETE)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_SCARLET_GEM, OutwardItemName.RED_LADYS_DAGGER)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_SCARLET_LICHS_IDOL, OutwardItemName.KRYPTEIA_TOMB_KEY)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_SCEPTER_OF_THE_CRUEL_PRIEST, OutwardItemName.SEALED_MACE)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_SEALED_MACE, OutwardItemName.SMELLY_SEALED_BOX)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_SHRIEK, OutwardItemName.RUSTED_SPEAR)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_TOKEBAKICIT, OutwardItemName.UNUSUAL_KNUCKLES)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_WARM_AXE, OutwardItemName.MYRMITAUR_HAVEN_GATE_KEY)

    # tier 2+ skill checks

    for location_name, (_, tier) in world.skill_sanity_location_info.items():
        if tier > 1:
            if world.options.num_breakthough_points.value < len(OutwardLocationGroup.SKILL_TRAINER_INTERACT):
                world.set_location_missable(location_name)
            else:
                world.add_location_item_requirement(location_name, OutwardItemName.BREAKTHROUGH_POINT, count=len(OutwardLocationGroup.SKILL_TRAINER_INTERACT))

    # dreamer halberd

    world.add_location_item_requirement(OutwardLocationName.SPAWN_DREAMER_HALBERD, OutwardEventName.FRIENDLY_IMMACULATE_CHERSONESE)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_DREAMER_HALBERD, OutwardEventName.FRIENDLY_IMMACULATE_ENMERKAR_FOREST)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_DREAMER_HALBERD, OutwardEventName.FRIENDLY_IMMACULATE_ABRASSAR)
    world.add_location_item_requirement(OutwardLocationName.SPAWN_DREAMER_HALBERD, OutwardEventName.FRIENDLY_IMMACULATE_HALLOWED_MARSH)

    # missable locations

    for location in world.get_locations():
        if location.check_missable(world):
            world.set_location_missable(location.name)

    # completion condition

    goal_event_name = OutwardEventGroup.GOALS[world.options.goal.value]
    world.multiworld.completion_condition[world.player] = lambda state: state.has(goal_event_name, world.player)
