from __future__ import annotations
from typing import TYPE_CHECKING

from worlds.AutoWorld import World

from .common import OUTWARD
from .factions import OutwardFaction
from .events import ItemClassification, OutwardEvent, OutwardEventGroup, OutwardEventName, add_world_events
from .items import OutwardGameItem, OutwardItem, OutwardItemGroup, OutwardItemName, add_world_starting_items, add_world_items
from .locations import OutwardGameLocation, OutwardLocation, OutwardLocationGroup, OutwardLocationName, add_world_locations
from .options import OutwardOptions
from .regions import OutwardEntrance, OutwardEntranceName, OutwardRegion, OutwardRegionName, add_world_regions, add_world_entrances
from .rules import add_world_rules

if TYPE_CHECKING:
    from collections.abc import Iterable
    from typing import Any

    from BaseClasses import Item
    from worlds.generic.Rules import CollectionRule, ItemRule

class OutwardWorld(World):
    game = OUTWARD
    
    # required maps
    item_name_to_id = OutwardItemName.get_name_to_id()
    location_name_to_id = OutwardLocationName.get_name_to_id()
    
    # options
    options_dataclass = OutwardOptions
    options: OutwardOptions

    # skill sanity information
    skill_sanity_location_info = {
        OutwardLocationName.SKILL_TRAINER_ADALBERT_CALL_TO_ELEMENTS: (OutwardItemName.CALL_TO_ELEMENTS, 1),
        OutwardLocationName.SKILL_TRAINER_ADALBERT_MANA_PUSH: (OutwardItemName.MANA_PUSH, 1),
        OutwardLocationName.SKILL_TRAINER_ADALBERT_REVEAL_SOUL: (OutwardItemName.REVEAL_SOUL, 1),
        OutwardLocationName.SKILL_TRAINER_ADALBERT_WEATHER_TOLERANCE: (OutwardItemName.WEATHER_TOLERANCE, 1),
        OutwardLocationName.SKILL_TRAINER_ADALBERT_SHAMANIC_RESONANCE: (OutwardItemName.SHAMANIC_RESONANCE, 2),
        OutwardLocationName.SKILL_TRAINER_ADALBERT_SIGIL_OF_WIND: (OutwardItemName.SIGIL_OF_WIND, 3),
        OutwardLocationName.SKILL_TRAINER_ADALBERT_INFUSE_WIND: (OutwardItemName.INFUSE_WIND, 3),
        OutwardLocationName.SKILL_TRAINER_ADALBERT_CONJURE: (OutwardItemName.CONJURE, 3),

        OutwardLocationName.SKILL_TRAINER_ALEMMON_CHAKRAM_ARC: (OutwardItemName.CHAKRAM_ARC, 1),
        OutwardLocationName.SKILL_TRAINER_ALEMMON_CHAKRAM_PIERCE: (OutwardItemName.CHAKRAM_PIERCE, 1),
        OutwardLocationName.SKILL_TRAINER_ALEMMON_MANA_WARD: (OutwardItemName.MANA_WARD, 1),
        OutwardLocationName.SKILL_TRAINER_ALEMMON_SIGIL_OF_FIRE: (OutwardItemName.SIGIL_OF_FIRE, 1),
        OutwardLocationName.SKILL_TRAINER_ALEMMON_LEYLINE_CONNECTION: (OutwardItemName.LEYLINE_CONNECTION, 2),
        OutwardLocationName.SKILL_TRAINER_ALEMMON_CHAKRAM_DANCE: (OutwardItemName.CHAKRAM_DANCE, 3),
        OutwardLocationName.SKILL_TRAINER_ALEMMON_SIGIL_OF_ICE: (OutwardItemName.SIGIL_OF_ICE, 3),
        OutwardLocationName.SKILL_TRAINER_ALEMMON_FIRE_AFFINITY: (OutwardItemName.FIRE_AFFINITY, 3),

        OutwardLocationName.SKILL_TRAINER_BEA_WARRIORS_VEIN: (OutwardItemName.WARRIORS_VEIN, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_DISPERSION: (OutwardItemName.DISPERSION, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_MOMENT_OF_TRUTH: (OutwardItemName.MOMENT_OF_TRUTH, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_TECHNIQUE: (OutwardItemName.TECHNIQUE, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_SCALP_COLLECTOR: (OutwardItemName.SCALP_COLLECTOR, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_CRESCENDO: (OutwardItemName.CRESCENDO, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_VICIOUS_CYCLE: (OutwardItemName.VICIOUS_CYCLE, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_SPLITTER: (OutwardItemName.SPLITTER, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_VITAL_CRASH: (OutwardItemName.VITAL_CRASH, 1),
        OutwardLocationName.SKILL_TRAINER_BEA_STRAFING_RUN: (OutwardItemName.STRAFING_RUN, 1),

        OutwardLocationName.SKILL_TRAINER_ELLA_JINX: (OutwardItemName.JINX, 1),
        OutwardLocationName.SKILL_TRAINER_ELLA_NIGHTMARES: (OutwardItemName.NIGHTMARES, 1),
        OutwardLocationName.SKILL_TRAINER_ELLA_TORMENT: (OutwardItemName.TORMENT, 1),
        OutwardLocationName.SKILL_TRAINER_ELLA_BLOODLUST: (OutwardItemName.BLOODLUST, 2),
        OutwardLocationName.SKILL_TRAINER_ELLA_BLOOD_SIGIL: (OutwardItemName.BLOOD_SIGIL, 3),
        OutwardLocationName.SKILL_TRAINER_ELLA_RUPTURE: (OutwardItemName.RUPTURE, 3),
        OutwardLocationName.SKILL_TRAINER_ELLA_CLEANSE: (OutwardItemName.CLEANSE, 3),
        OutwardLocationName.SKILL_TRAINER_ELLA_LOCKWELLS_REVELATION: (OutwardItemName.LOCKWELLS_REVELATION, 3),

        OutwardLocationName.SKILL_TRAINER_ETO_FITNESS: (OutwardItemName.FITNESS, 1),
        OutwardLocationName.SKILL_TRAINER_ETO_SHIELD_CHARGE: (OutwardItemName.SHIELD_CHARGE, 1),
        OutwardLocationName.SKILL_TRAINER_ETO_STEADY_ARM: (OutwardItemName.STEADY_ARM, 1),
        OutwardLocationName.SKILL_TRAINER_ETO_SPELLBLADES_AWAKENING: (OutwardItemName.SPELLBLADES_AWAKENING, 2),
        OutwardLocationName.SKILL_TRAINER_ETO_INFUSE_FIRE: (OutwardItemName.INFUSE_FIRE, 3),
        OutwardLocationName.SKILL_TRAINER_ETO_INFUSE_FROST: (OutwardItemName.INFUSE_FROST, 3),
        OutwardLocationName.SKILL_TRAINER_ETO_GONG_STRIKE: (OutwardItemName.GONG_STRIKE, 3),
        OutwardLocationName.SKILL_TRAINER_ETO_ELEMENTAL_DISCHARGE: (OutwardItemName.ELEMENTAL_DISCHARGE, 3),

        OutwardLocationName.SKILL_TRAINER_FLASE_RUNE_DEZ: (OutwardItemName.RUNE_DEZ, 1),
        OutwardLocationName.SKILL_TRAINER_FLASE_RUNE_EGOTH: (OutwardItemName.RUNE_EGOTH, 1),
        OutwardLocationName.SKILL_TRAINER_FLASE_RUNE_FAL: (OutwardItemName.RUNE_FAL, 1),
        OutwardLocationName.SKILL_TRAINER_FLASE_RUNE_SHIM: (OutwardItemName.RUNE_SHIM, 1),
        OutwardLocationName.SKILL_TRAINER_FLASE_WELL_OF_MANA: (OutwardItemName.WELL_OF_MANA, 2),
        OutwardLocationName.SKILL_TRAINER_FLASE_ARCANE_SYNTAX: (OutwardItemName.ARCANE_SYNTAX, 3),
        OutwardLocationName.SKILL_TRAINER_FLASE_INTERNALIZED_LEXICON: (OutwardItemName.INTERNALIZED_LEXICON, 3),
        OutwardLocationName.SKILL_TRAINER_FLASE_RUNIC_PREFIX: (OutwardItemName.RUNIC_PREFIX, 3),

        OutwardLocationName.SKILL_TRAINER_GALIRA_BRACE: (OutwardItemName.BRACE, 1),
        OutwardLocationName.SKILL_TRAINER_GALIRA_FOCUS: (OutwardItemName.FOCUS, 1),
        OutwardLocationName.SKILL_TRAINER_GALIRA_SLOW_METABOLISM: (OutwardItemName.SLOW_METABOLISM, 1),
        OutwardLocationName.SKILL_TRAINER_GALIRA_STEADFAST_ASCETIC: (OutwardItemName.STEADFAST_ASCETIC, 2),
        OutwardLocationName.SKILL_TRAINER_GALIRA_PERFECT_STRIKE: (OutwardItemName.PERFECT_STRIKE, 3),
        OutwardLocationName.SKILL_TRAINER_GALIRA_MASTER_OF_MOTION: (OutwardItemName.MASTER_OF_MOTION, 3),
        OutwardLocationName.SKILL_TRAINER_GALIRA_FLASH_ONSLAUGHT: (OutwardItemName.FLASH_ONSLAUGHT, 3),
        OutwardLocationName.SKILL_TRAINER_GALIRA_COUNTERSTRIKE: (OutwardItemName.COUNTERSTRIKE, 3),

        OutwardLocationName.SKILL_TRAINER_JAIMON_ARMOR_TRAINING: (OutwardItemName.ARMOR_TRAINING, 1),
        OutwardLocationName.SKILL_TRAINER_JAIMON_FAST_MAINTENANCE: (OutwardItemName.FAST_MAINTENANCE, 1),
        OutwardLocationName.SKILL_TRAINER_JAIMON_FROST_BULLET: (OutwardItemName.FROST_BULLET, 1),
        OutwardLocationName.SKILL_TRAINER_JAIMON_SHATTER_BULLET: (OutwardItemName.SHATTER_BULLET, 1),
        OutwardLocationName.SKILL_TRAINER_JAIMON_SWIFT_FOOT: (OutwardItemName.SWIFT_FOOT, 2),
        OutwardLocationName.SKILL_TRAINER_JAIMON_MARATHONER: (OutwardItemName.MARATHONER, 3),
        OutwardLocationName.SKILL_TRAINER_JAIMON_SHIELD_INFUSION: (OutwardItemName.SHIELD_INFUSION, 3),
        OutwardLocationName.SKILL_TRAINER_JAIMON_BLOOD_BULLET: (OutwardItemName.BLOOD_BULLET, 3),

        OutwardLocationName.SKILL_TRAINER_JUSTIN_ACROBATICS: (OutwardItemName.ACROBATICS, 1),
        OutwardLocationName.SKILL_TRAINER_JUSTIN_BRAINS: (OutwardItemName.BRAINS, 1),
        OutwardLocationName.SKILL_TRAINER_JUSTIN_BRAWNS: (OutwardItemName.BRAWNS, 1),
        OutwardLocationName.SKILL_TRAINER_JUSTIN_CRUELTY: (OutwardItemName.CRUELTY, 1),
        OutwardLocationName.SKILL_TRAINER_JUSTIN_PATIENCE: (OutwardItemName.PATIENCE, 1),
        OutwardLocationName.SKILL_TRAINER_JUSTIN_UNSEALED: (OutwardItemName.UNSEALED, 1),

        OutwardLocationName.SKILL_TRAINER_SERGE_EFFICIENCY: (OutwardItemName.EFFICIENCY, 1),
        OutwardLocationName.SKILL_TRAINER_SERGE_METABOLIC_PURGE: (OutwardItemName.METABOLIC_PURGE, 1),
        OutwardLocationName.SKILL_TRAINER_SERGE_PROBE: (OutwardItemName.PROBE, 1),
        OutwardLocationName.SKILL_TRAINER_SERGE_DAREDEVIL: (OutwardItemName.DAREDEVIL, 2),
        OutwardLocationName.SKILL_TRAINER_SERGE_PRIME: (OutwardItemName.PRIME, 3),
        OutwardLocationName.SKILL_TRAINER_SERGE_UNERRING_READ: (OutwardItemName.UNERRING_READ, 3),
        OutwardLocationName.SKILL_TRAINER_SERGE_BLITZ: (OutwardItemName.BLITZ, 3),
        OutwardLocationName.SKILL_TRAINER_SERGE_ANTICIPATION: (OutwardItemName.ANTICIPATION, 3),

        OutwardLocationName.SKILL_TRAINER_SINAI_HAUNTING_BEAT: (OutwardItemName.HAUNTING_BEAT, 1),
        OutwardLocationName.SKILL_TRAINER_SINAI_MIASMIC_TOLERANCE: (OutwardItemName.MIASMIC_TOLERANCE, 1),
        OutwardLocationName.SKILL_TRAINER_SINAI_WELKIN_RING: (OutwardItemName.WELKIN_RING, 1),
        OutwardLocationName.SKILL_TRAINER_SINAI_SACRED_FUMES: (OutwardItemName.SACRED_FUMES, 2),
        OutwardLocationName.SKILL_TRAINER_SINAI_NURTURING_ECHO: (OutwardItemName.NURTURING_ECHO, 3),
        OutwardLocationName.SKILL_TRAINER_SINAI_REVERBERATION: (OutwardItemName.REVERBERATION, 3),
        OutwardLocationName.SKILL_TRAINER_SINAI_HARMONY_AND_MELODY: (OutwardItemName.HARMONY_AND_MELODY, 3),
        OutwardLocationName.SKILL_TRAINER_SINAI_BATTLE_RYTHYM: (OutwardItemName.BATTLE_RHYTHM, 3),

        OutwardLocationName.SKILL_TRAINER_STYX_BACKSTAB: (OutwardItemName.BACKSTAB, 1),
        OutwardLocationName.SKILL_TRAINER_STYX_OPPORTUNIST_STAB: (OutwardItemName.OPPORTUNIST_STAB, 1),
        OutwardLocationName.SKILL_TRAINER_STYX_PRESSURE_PLATE_TRAINING: (OutwardItemName.PRESSURE_PLATE_TRAINING, 1),
        OutwardLocationName.SKILL_TRAINER_STYX_SWEEP_KICK: (OutwardItemName.SWEEP_KICK, 1),
        OutwardLocationName.SKILL_TRAINER_STYX_FEATHER_DODGE: (OutwardItemName.FEATHER_DODGE, 2),
        OutwardLocationName.SKILL_TRAINER_STYX_SERPENTS_PARRY: (OutwardItemName.SERPENTS_PARRY, 3),
        OutwardLocationName.SKILL_TRAINER_STYX_STEALTH_TRAINING: (OutwardItemName.STEALTH_TRAINING, 3),
        OutwardLocationName.SKILL_TRAINER_STYX_PRESSURE_PLATE_EXPERTISE: (OutwardItemName.PRESSURE_PLATE_EXPERTISE, 3),

        OutwardLocationName.SKILL_TRAINER_TURE_ENRAGE: (OutwardItemName.ENRAGE, 1),
        OutwardLocationName.SKILL_TRAINER_TURE_EVASION_SHOT: (OutwardItemName.EVASION_SHOT, 1),
        OutwardLocationName.SKILL_TRAINER_TURE_HUNTERS_EYE: (OutwardItemName.HUNTERS_EYE, 1),
        OutwardLocationName.SKILL_TRAINER_TURE_SNIPER_SHOT: (OutwardItemName.SNIPER_SHOT, 1),
        OutwardLocationName.SKILL_TRAINER_TURE_SURVIVORS_RESILIENCE: (OutwardItemName.SURVIVORS_RESILIENCE, 2),
        OutwardLocationName.SKILL_TRAINER_TURE_PREDATOR_LEAP: (OutwardItemName.PREDATOR_LEAP, 3),
        OutwardLocationName.SKILL_TRAINER_TURE_PIERCING_SHOT: (OutwardItemName.PIERCING_SHOT, 3),
        OutwardLocationName.SKILL_TRAINER_TURE_FERAL_STRIKES: (OutwardItemName.FERAL_STRIKES, 3),
    }

    def get_item(self, item_name: str) -> OutwardItem:
        for item in self.get_items():
            if item.name == item_name:
                return item
        raise ValueError(f"cannot find the item '{item_name}'")

    def get_region(self, region_name: str) -> OutwardRegion:
        region = super().get_region(region_name)
        if not isinstance(region, OutwardRegion):
            raise ValueError(f"the region '{region}' is not an Outward region")
        return region

    def get_entrance(self, entrance_name: str) -> OutwardEntrance:
        entrance = super().get_entrance(entrance_name)
        if not isinstance(entrance, OutwardEntrance):
            raise ValueError(f"the entrance '{entrance_name}' is not an Outward entrance")
        return entrance

    def get_location(self, location_name: str) -> OutwardLocation:
        location = super().get_location(location_name)
        if not isinstance(location, OutwardLocation):
            raise ValueError(f"the location '{location_name}' is not an Outward location")
        return location

    def get_items(self) -> Iterable[OutwardItem]:
        for item in self.multiworld.itempool:
            if item.player == self.player and isinstance(item, OutwardItem):
                yield item
        for loc in self.get_locations():
            item = loc.item
            if item is not None and item.player == self.player and isinstance(item, OutwardItem):
                yield item

    def get_regions(self) -> Iterable[OutwardRegion]:
        return [region for region in super().get_regions() if isinstance(region, OutwardRegion)]

    def get_entrances(self) -> Iterable[OutwardEntrance]:
        return [entrance for entrance in super().get_entrances() if isinstance(entrance, OutwardEntrance)]

    def get_locations(self) -> Iterable[OutwardLocation]:
        return [location for location in super().get_locations() if isinstance(location, OutwardLocation)]

    def create_item(self, name: str) -> OutwardGameItem:
        return OutwardGameItem(name, self.player)

    def create_region(self, name: str) -> OutwardRegion:
        return OutwardRegion(name, self.multiworld, self.player)

    def create_location(self, name: str) -> OutwardGameLocation:
        return OutwardGameLocation(name, self.player)

    def create_event(self, name: str) -> OutwardEvent:
        return OutwardEvent(name, self.player)

    def create_entrance(self, name: str) -> OutwardEntrance:
        return OutwardEntrance(name, self.player)

    def add_item(self, name: str) -> OutwardGameItem:
        item = self.create_item(name)
        item.add_to_world(self)
        return item

    def add_region(self, name: str) -> OutwardRegion:
        region = self.create_region(name)
        region.add_to_world(self)
        return region

    def add_location(self, name: str) -> OutwardGameLocation:
        location = self.create_location(name)
        location.add_to_world(self)
        return location

    def add_event(self, name: str) -> OutwardEvent:
        event = self.create_event(name)
        event.add_to_world(self)
        return event

    def add_entrance(self, name: str) -> OutwardEntrance:
        entrance = self.create_entrance(name)
        entrance.add_to_world(self)
        return entrance

    def add_entrance_access_rule(self, name: str, rule: CollectionRule, combine: str = "and", do_require_exists: bool = True) -> None:
        try:
            entrance = self.get_entrance(name)
        except:
            if do_require_exists:
                raise
            entrance = None
        if entrance is not None:
            entrance.add_rule(rule, combine)

    def add_location_access_rule(self, name: str, rule: CollectionRule, combine: str = "and", do_require_exists: bool = True) -> None:
        try:
            location = self.get_location(name)
        except:
            if do_require_exists:
                raise
            location = None
        if location is not None:
            location.add_rule(rule, combine)

    def add_location_item_rule(self, name: str, rule: ItemRule, combine: str = "and", do_require_exists: bool = True) -> None:
        try:
            location = self.get_location(name)
        except:
            if do_require_exists:
                raise
            location = None
        if location is not None:
            location.add_item_rule(rule, combine)

    def add_entrance_item_requirement(self, entrance_name: str, item_name: str, count: int = 1, combine: str = "and", do_require_exists: bool = True) -> None:
        try:
            item = self.get_item(item_name)
        except:
            if do_require_exists:
                raise
            item = None
        if item is not None:
            self.add_entrance_access_rule(entrance_name, lambda state: state.has(item.name, self.player, count), combine, do_require_exists)

    def add_location_item_requirement(self, location_name: str, item_name: str, count: int = 1, combine: str = "and", do_require_exists: bool = True) -> None:
        try:
            item = self.get_item(item_name)
        except:
            if do_require_exists:
                raise
            item = None
        if item is not None:
            self.add_location_access_rule(location_name, lambda state: state.has(item.name, self.player, count), combine, do_require_exists)

    def set_location_missable(self, location_name: str, do_require_exists: bool = True) -> None:
        self.add_location_item_rule(location_name, self.item_rule_missable, do_require_exists)

    def lock_location_item(self, location_name: str, item_name: str):
        location = self.get_location(location_name)
        item = self.get_item(item_name)
        self.multiworld.itempool.remove(item)
        location.place_locked_item(item)

    def item_rule_missable(self, item: Item) -> bool:
        return item.classification == ItemClassification.filler or (item.player == self.player and ItemClassification.progression not in item.classification)
    
    def get_allowed_factions(self) -> OutwardFaction:
        faction = self.options.faction
        match faction.value:
            case faction.option_blue_chamber:
                return OutwardFaction.BlueChamber
            case faction.option_heroic_kingdom:
                return OutwardFaction.HeroicKingdom
            case faction.option_holy_mission:
                return OutwardFaction.HolyMission
            case faction.option_sorobor_academy:
                return OutwardFaction.SoroborAcademy
        return OutwardFaction.AllFactions

    def get_pact_item_name(self) -> str | None:
        match self.get_allowed_factions():
            case OutwardFaction.BlueChamber:
                return OutwardItemName.FACTION_PACT_BLUE_CHAMBER
            case OutwardFaction.HeroicKingdom:
                return OutwardItemName.FACTION_PACT_HEROIC_KINGDOM
            case OutwardFaction.HolyMission:
                return OutwardItemName.FACTION_PACT_HOLY_MISSION
            case OutwardFaction.SoroborAcademy:
                return OutwardItemName.FACTION_PACT_SOROBOR_ACADEMY
        return None

    def generate_early(self):
        add_world_starting_items(self)

    def create_regions(self):
        add_world_regions(self)
        add_world_entrances(self)
        add_world_events(self)
        add_world_locations(self)

    def get_filler_item_name(self) -> str:
        return self.random.choice(tuple(OutwardItemGroup.FILLER))
      
    def create_items(self):
        add_world_items(self)

        # filler items

        location_count = len(tuple(self.get_locations()))
        item_count = len(tuple(self.get_items()))
        filler_count = location_count - item_count
        if filler_count > 0:
            for _ in range(filler_count):
                item_name = self.get_filler_item_name()
                self.add_item(item_name)

    def set_rules(self):
        add_world_rules(self)

    def pre_fill(self):
        if not bool(self.options.breakthrough_point_checks.value):
            for location_name in OutwardLocationGroup.SKILL_TRAINER_INTERACT:
                self.lock_location_item(location_name, OutwardItemName.SILVER_CURRENCY)

        for location_name, (item_name, tier) in self.skill_sanity_location_info.items():
            if self.options.skillsanity.value == self.options.skillsanity.option_vanilla or (self.options.skillsanity.value == self.options.skillsanity.option_tier_one_only and tier > 1):
                self.lock_location_item(location_name, item_name)

        if self.options.wind_altar_checks.value == 0:
            self.lock_location_item(OutwardLocationName.WIND_ALTAR_CHERSONESE, OutwardItemName.WIND_ALTAR_BOON_CHERSONESE)
            self.lock_location_item(OutwardLocationName.WIND_ALTAR_ENMERKAR_FOREST, OutwardItemName.WIND_ALTAR_BOON_ENMERKAR_FOREST)
            self.lock_location_item(OutwardLocationName.WIND_ALTAR_ABRASSAR, OutwardItemName.WIND_ALTAR_BOON_ABRASSAR)
            self.lock_location_item(OutwardLocationName.WIND_ALTAR_HALLOWED_MARSH, OutwardItemName.WIND_ALTAR_BOON_HALLOWED_MARSH)
            self.lock_location_item(OutwardLocationName.WIND_ALTAR_ANTIQUE_PLATEAU, OutwardItemName.WIND_ALTAR_BOON_ANTIQUE_PLATEAU)
            self.lock_location_item(OutwardLocationName.WIND_ALTAR_CALDERA, OutwardItemName.WIND_ALTAR_BOON_CALDERA)
            
    def fill_slot_data(self) -> dict[str, Any]:
        return {
            "death_link": bool(self.options.death_link.value),
            "goal": int(self.options.goal.value),
            "faction_pact_enabled": bool(self.get_pact_item_name() is not None),
            "skillsanity": int(self.options.skillsanity.value),
            "wind_altar_checks": bool(self.options.wind_altar_checks.value),
            "breakthrough_point_checks": bool(self.options.breakthrough_point_checks.value),
        }
