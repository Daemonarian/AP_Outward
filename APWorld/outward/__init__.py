from __future__ import annotations
from typing import TYPE_CHECKING

from worlds.AutoWorld import World

from .common import OUTWARD
from .events import ItemClassification, OutwardEvent, OutwardEventName
from .items import OutwardGameItem, OutwardItem, OutwardItemGroup, OutwardItemName
from .locations import OutwardGameLocation, OutwardLocation, OutwardLocationGroup, OutwardLocationName
from .options import OutwardOptions
from .regions import OutwardEntrance, OutwardEntranceName, OutwardRegion, OutwardRegionName

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

    def get_item(self, item_name) -> OutwardItem:
        for item in self.get_items():
            if item.name == item_name:
                return item
        raise ValueError(f"cannot find the item '{item_name}'")

    def get_region(self, region_name) -> OutwardRegion:
        region = super().get_region(region_name)
        if not isinstance(region, OutwardRegion):
            raise ValueError(f"the region '{region}' is not an Outward region")
        return region

    def get_entrance(self, entrance_name) -> OutwardEntrance:
        entrance = super().get_entrance(entrance_name)
        if not isinstance(entrance, OutwardEntrance):
            raise ValueError(f"the entrance '{entrance_name}' is not an Outward entrance")
        return entrance

    def get_location(self, location_name) -> OutwardLocation:
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

    def add_entrance_access_rule(self, name: str, rule: CollectionRule, combine: str = "and") -> None:
        self.get_entrance(name).add_rule(rule, combine)

    def add_location_access_rule(self, name: str, rule: CollectionRule, combine: str = "and") -> None:
        self.get_location(name).add_rule(rule, combine)

    def add_location_item_rule(self, name: str, rule: ItemRule, combine: str = "and") -> None:
        self.get_location(name).add_item_rule(rule, combine)

    def add_entrance_item_requirement(self, entrance_name: str, item_name: str, count: int = 1, combine: str = "and") -> None:
        self.add_entrance_access_rule(entrance_name, lambda state: state.has(item_name, self.player, count), combine)

    def add_location_item_requirement(self, location_name: str, item_name: str, count: int = 1, combine: str = "and") -> None:
        self.add_location_access_rule(location_name, lambda state: state.has(item_name, self.player, count), combine)

    def set_location_missable(self, location_name: str) -> None:
        self.add_location_item_rule(location_name, self.item_rule_missable)

    def lock_location_item(self, location_name: str, item_name: str):
        location = self.get_location(location_name)
        item = self.get_item(item_name)
        self.multiworld.itempool.remove(item)
        location.place_locked_item(item)

    def item_rule_missable(self, item: Item):
        return (item.player == self.player and ItemClassification.progression not in item.classification) or ItemClassification.filler in item.classification

    def generate_early(self):
        if not bool(self.options.breakthrough_point_checks.value):
            for _ in range(self.options.num_breakthough_points.value):
                self.push_precollected(self.create_item(OutwardItemName.BREAKTHROUGH_POINT))
          
    def create_regions(self):
        for region_name in OutwardRegionName.get_names():
            self.add_region(region_name)
        for entrance_name in OutwardEntranceName.get_names():
            self.add_entrance(entrance_name)
        for event_name in OutwardEventName.get_names():
            self.add_event(event_name)
        for location_name in OutwardLocationName.get_names():
            self.add_location(location_name)

        if not bool(self.options.breakthrough_point_checks.value):
            for location_name in OutwardLocationGroup.SKILL_TRAINER_INTERACT:
                item = self.create_item(OutwardItemName.SILVER_CURRENCY)
                self.get_location(location_name).place_locked_item(item)

    def get_filler_item_name(self) -> str:
        return self.random.choice(tuple(OutwardItemGroup.FILLER))
      
    def create_items(self):
        # key items

        for _ in range(10):
            self.add_item(OutwardItemName.QUEST_LICENSE)

        # useful gear
        
        self.add_item(OutwardItemName.CEREMONIAL_BOW)
        self.add_item(OutwardItemName.COMPASSWOOD_STAFF)
        self.add_item(OutwardItemName.CRACKED_RED_MOON)
        self.add_item(OutwardItemName.DEPOWERED_BLUDGEON)
        self.add_item(OutwardItemName.EXPERIMENTAL_CHAKRAM)
        self.add_item(OutwardItemName.FOSSILIZED_GREATAXE)
        self.add_item(OutwardItemName.KRYPTEIA_TOMB_KEY)
        self.add_item(OutwardItemName.MYRMITAUR_HAVEN_GATE_KEY)
        self.add_item(OutwardItemName.MYSTERIOUS_LONG_BLADE)
        self.add_item(OutwardItemName.RED_LADYS_DAGGER)
        self.add_item(OutwardItemName.RUINED_HALBERD)
        self.add_item(OutwardItemName.RUSTED_SPEAR)
        self.add_item(OutwardItemName.SCARLET_GEM)
        self.add_item(OutwardItemName.SCARLET_LICHS_IDOL)
        self.add_item(OutwardItemName.SCARRED_DAGGER)
        self.add_item(OutwardItemName.SCARRED_DAGGER)
        self.add_item(OutwardItemName.SEALED_MACE)
        self.add_item(OutwardItemName.SLUMBERING_SHIELD)
        self.add_item(OutwardItemName.SMELLY_SEALED_BOX)
        self.add_item(OutwardItemName.STRANGE_RUSTED_SWORD)
        self.add_item(OutwardItemName.UNUSUAL_KNUCKLES)
        self.add_item(OutwardItemName.WARM_AXE)

        self.add_item(OutwardItemName.ANGLER_SHIELD)
        self.add_item(OutwardItemName.ANTIQUE_PLATE_BOOTS)
        self.add_item(OutwardItemName.ANTIQUE_PLATE_GARB)
        self.add_item(OutwardItemName.ANTIQUE_PLATE_SALLET)
        self.add_item(OutwardItemName.BLUE_SAND_ARMOR)
        self.add_item(OutwardItemName.BLUE_SAND_BOOTS)
        self.add_item(OutwardItemName.BLUE_SAND_HELM)
        self.add_item(OutwardItemName.BRAND)
        self.add_item(OutwardItemName.BRASS_WOLF_BACKPACK)
        self.add_item(OutwardItemName.COPAL_ARMOR)
        self.add_item(OutwardItemName.COPAL_BOOTS)
        self.add_item(OutwardItemName.COPAL_HELM)
        self.add_item(OutwardItemName.DISTORTED_EXPERIMENT)
        self.add_item(OutwardItemName.DREAMER_HALBERD)
        self.add_item(OutwardItemName.DUTY)
        self.add_item(OutwardItemName.FABULOUS_PALLADIUM_SHIELD)
        self.add_item(OutwardItemName.GEPS_LONGBLADE)
        self.add_item(OutwardItemName.GHOST_PARALLEL)
        self.add_item(OutwardItemName.GILDED_SHIVER_OF_TRAMONTANE)
        self.add_item(OutwardItemName.GOLD_LICH_ARMOR)
        self.add_item(OutwardItemName.GOLD_LICH_BOOTS)
        self.add_item(OutwardItemName.GOLD_LICH_MASK)
        self.add_item(OutwardItemName.GOLD_LICH_SPEAR)
        self.add_item(OutwardItemName.GRIND)
        self.add_item(OutwardItemName.JADE_LICH_BOOTS)
        self.add_item(OutwardItemName.JADE_LICH_MASK)
        self.add_item(OutwardItemName.JADE_LICH_ROBES)
        self.add_item(OutwardItemName.JADE_LICH_STAFF)
        self.add_item(OutwardItemName.LIGHT_MENDERS_BACKPACK)
        self.add_item(OutwardItemName.LIGHT_MENDERS_LEXICON)
        self.add_item(OutwardItemName.MEFINOS_TRADE_BACKPACK)
        self.add_item(OutwardItemName.MERTONS_FIREPOKER)
        self.add_item(OutwardItemName.MERTONS_FIREPOKER)
        self.add_item(OutwardItemName.MERTONS_RIBCAGE)
        self.add_item(OutwardItemName.MERTONS_SHINBONES)
        self.add_item(OutwardItemName.MERTONS_SKULL)
        self.add_item(OutwardItemName.MURMURE)
        self.add_item(OutwardItemName.MYSTERIOUS_CHAKRAM)
        self.add_item(OutwardItemName.ORNATE_BONE_SHIELD)
        self.add_item(OutwardItemName.PALLADIUM_ARMOR)
        self.add_item(OutwardItemName.PALLADIUM_BOOTS)
        self.add_item(OutwardItemName.PALLADIUM_HELM)
        self.add_item(OutwardItemName.PEARLESCENT_MAIL)
        self.add_item(OutwardItemName.PETRIFIED_WOOD_ARMOR)
        self.add_item(OutwardItemName.PETRIFIED_WOOD_BOOTS)
        self.add_item(OutwardItemName.PETRIFIED_WOOD_HELM)
        self.add_item(OutwardItemName.PILLAR_GREATHAMMER)
        self.add_item(OutwardItemName.PORCELAIN_FISTS)
        self.add_item(OutwardItemName.PORCELAIN_FISTS)
        self.add_item(OutwardItemName.REVENANT_MOON)
        self.add_item(OutwardItemName.ROTWOOD_STAFF)
        self.add_item(OutwardItemName.RUST_LICH_ARMOR)
        self.add_item(OutwardItemName.RUST_LICH_BOOTS)
        self.add_item(OutwardItemName.RUST_LICH_HELMET)
        self.add_item(OutwardItemName.SANDROSE)
        self.add_item(OutwardItemName.SCARLET_BOOTS)
        self.add_item(OutwardItemName.SCARLET_MASK)
        self.add_item(OutwardItemName.SCARLET_ROBES)
        self.add_item(OutwardItemName.SCEPTER_OF_THE_CRUEL_PRIEST)
        self.add_item(OutwardItemName.SHRIEK)
        self.add_item(OutwardItemName.SKYCROWN_MACE)
        self.add_item(OutwardItemName.STARCHILD_CLAYMORE)
        self.add_item(OutwardItemName.SUNFALL_AXE)
        self.add_item(OutwardItemName.TENEBROUS_ARMOR)
        self.add_item(OutwardItemName.TENEBROUS_BOOTS)
        self.add_item(OutwardItemName.TENEBROUS_HELM)
        self.add_item(OutwardItemName.THE_WILL_O_WISP)
        self.add_item(OutwardItemName.THRICE_WROUGHT_HALBERD)
        self.add_item(OutwardItemName.THRICE_WROUGHT_HALBERD)
        self.add_item(OutwardItemName.TOKEBAKICIT)
        self.add_item(OutwardItemName.TSAR_ARMOR)
        self.add_item(OutwardItemName.TSAR_BOOTS)
        self.add_item(OutwardItemName.TSAR_FISTS)
        self.add_item(OutwardItemName.TSAR_HELM)
        self.add_item(OutwardItemName.WERLIG_SPEAR)
        self.add_item(OutwardItemName.WORLDEDGE_GREATAXE)
        self.add_item(OutwardItemName.ZHORNS_DEMON_SHIELD)
        self.add_item(OutwardItemName.ZHORNS_GLOWSTONE_DAGGER)
        self.add_item(OutwardItemName.ZHORNS_HUNTING_BACKPACK)

        # useful skills

        self.add_item(OutwardItemName.BLADE_PUPPY)
        self.add_item(OutwardItemName.BLESSED)
        self.add_item(OutwardItemName.CHILL_HEX)
        self.add_item(OutwardItemName.COOL)
        self.add_item(OutwardItemName.CURSE_HEX)
        self.add_item(OutwardItemName.DOOM_HEX)
        self.add_item(OutwardItemName.ELATTS_INTERVENTION)
        self.add_item(OutwardItemName.EXECUTION)
        self.add_item(OutwardItemName.FLAMETHROWER)
        self.add_item(OutwardItemName.GOLDEN_WATCHER)
        self.add_item(OutwardItemName.HAUNT_HEX)
        self.add_item(OutwardItemName.INFUSE_BLOOD)
        self.add_item(OutwardItemName.INFUSE_MANA)
        self.add_item(OutwardItemName.JUGGERNAUT)
        self.add_item(OutwardItemName.KIROUACS_BREAKTHROUGH)
        self.add_item(OutwardItemName.MACE_INFUSION)
        self.add_item(OutwardItemName.MIST)
        self.add_item(OutwardItemName.MOON_SWIPE)
        self.add_item(OutwardItemName.POMMEL_COUNTER)
        self.add_item(OutwardItemName.POSSESSED)
        self.add_item(OutwardItemName.PRISMATIC_FLURRY)
        self.add_item(OutwardItemName.PUNCTURE)
        self.add_item(OutwardItemName.SCORCH_HEX)
        self.add_item(OutwardItemName.SEVERED_OBSIDIAN)
        self.add_item(OutwardItemName.SIMEONS_GAMBIT)
        self.add_item(OutwardItemName.TALUS_CLEAVER)
        self.add_item(OutwardItemName.WARM)

        # skill trainer skills

        for item, _ in self.skill_sanity_location_info.values():
            self.add_item(item)

        # wind altars

        self.add_item(OutwardItemName.WIND_ALTAR_BOON_CHERSONESE)
        self.add_item(OutwardItemName.WIND_ALTAR_BOON_ENMERKAR_FOREST)
        self.add_item(OutwardItemName.WIND_ALTAR_BOON_ABRASSAR)
        self.add_item(OutwardItemName.WIND_ALTAR_BOON_HALLOWED_MARSH)
        self.add_item(OutwardItemName.WIND_ALTAR_BOON_ANTIQUE_PLATEAU)
        self.add_item(OutwardItemName.WIND_ALTAR_BOON_CALDERA)

        # breakthrough points

        if bool(self.options.breakthrough_point_checks.value):
            for _ in range(self.options.num_breakthough_points.value):
                self.add_item(OutwardItemName.BREAKTHROUGH_POINT)

        # filler items

        location_count = len(tuple(self.get_locations()))
        item_count = len(tuple(self.get_items()))
        filler_count = location_count - item_count
        if filler_count > 0:
            for _ in range(filler_count):
                item_name = self.get_filler_item_name()
                self.add_item(item_name)

    def set_rules(self):
        # main quest events
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_02_COMPLETE, OutwardEventName.MAIN_QUEST_01_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_COMPLETE, OutwardEventName.MAIN_QUEST_02_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_04_COMPLETE, OutwardEventName.MAIN_QUEST_03_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_05_COMPLETE, OutwardEventName.MAIN_QUEST_04_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_06_COMPLETE, OutwardEventName.MAIN_QUEST_05_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_07_COMPLETE, OutwardEventName.MAIN_QUEST_06_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_08_COMPLETE, OutwardEventName.MAIN_QUEST_07_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_09_COMPLETE, OutwardEventName.MAIN_QUEST_08_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_10_COMPLETE, OutwardEventName.MAIN_QUEST_09_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_11_COMPLETE, OutwardEventName.MAIN_QUEST_10_COMPLETE)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_12_COMPLETE, OutwardEventName.MAIN_QUEST_11_COMPLETE)
        
        # quest licenses
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_03_COMPLETE, OutwardItemName.QUEST_LICENSE, 1)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_04_COMPLETE, OutwardItemName.QUEST_LICENSE, 2)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_05_COMPLETE, OutwardItemName.QUEST_LICENSE, 3)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_06_COMPLETE, OutwardItemName.QUEST_LICENSE, 4)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_07_COMPLETE, OutwardItemName.QUEST_LICENSE, 5)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_08_COMPLETE, OutwardItemName.QUEST_LICENSE, 6)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_09_COMPLETE, OutwardItemName.QUEST_LICENSE, 7)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_10_COMPLETE, OutwardItemName.QUEST_LICENSE, 8)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_11_COMPLETE, OutwardItemName.QUEST_LICENSE, 9)
        self.add_location_item_requirement(OutwardEventName.MAIN_QUEST_12_COMPLETE, OutwardItemName.QUEST_LICENSE, 10)

        # quest completion events
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_01, OutwardEventName.MAIN_QUEST_01_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_02, OutwardEventName.MAIN_QUEST_02_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_03, OutwardEventName.MAIN_QUEST_03_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_04, OutwardEventName.MAIN_QUEST_04_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_05, OutwardEventName.MAIN_QUEST_05_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_06, OutwardEventName.MAIN_QUEST_06_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_07, OutwardEventName.MAIN_QUEST_07_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_08, OutwardEventName.MAIN_QUEST_08_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_09, OutwardEventName.MAIN_QUEST_09_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_10, OutwardEventName.MAIN_QUEST_10_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_11, OutwardEventName.MAIN_QUEST_11_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_MAIN_12, OutwardEventName.MAIN_QUEST_12_COMPLETE)

        self.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_BLOOD_UNDER_THE_SUN, OutwardEventName.MAIN_QUEST_04_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_PURIFIER, OutwardEventName.MAIN_QUEST_02_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_VENDAVEL_QUEST, OutwardEventName.MAIN_QUEST_02_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_1, OutwardEventName.MAIN_QUEST_04_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_2, OutwardEventName.MAIN_QUEST_04_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_3, OutwardEventName.MAIN_QUEST_04_COMPLETE)

        # useful items

        self.add_location_item_requirement(OutwardLocationName.SPAWN_ANGLER_SHIELD, OutwardItemName.SLUMBERING_SHIELD)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_BRAND, OutwardItemName.STRANGE_RUSTED_SWORD)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_DISTORTED_EXPERIMENT, OutwardItemName.EXPERIMENTAL_CHAKRAM)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_DUTY, OutwardItemName.RUINED_HALBERD)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_GEPS_BLADE, OutwardItemName.MYSTERIOUS_LONG_BLADE)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_GHOST_PARALLEL, OutwardItemName.DEPOWERED_BLUDGEON)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_GILDED_SHIVER_OF_TRAMONTANE, OutwardItemName.SCARRED_DAGGER)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_GRIND, OutwardItemName.FOSSILIZED_GREATAXE)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_MURMURE, OutwardItemName.CEREMONIAL_BOW)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_PEARLESCENT_MAIL, OutwardEventName.MAIN_QUEST_05_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_RED_LADYS_DAGGER, OutwardItemName.SCARLET_LICHS_IDOL)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_REVENANT_MOON, OutwardItemName.CRACKED_RED_MOON)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_REVENANT_MOON, OutwardItemName.SCARLET_GEM)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_ROTWOOD_STAFF, OutwardEventName.MAIN_QUEST_06_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_ROTWOOD_STAFF, OutwardItemName.COMPASSWOOD_STAFF)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_SANDROSE, OutwardItemName.WARM_AXE)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_SCARLET_GEM, OutwardEventName.MAIN_QUEST_09_COMPLETE)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_SCARLET_GEM, OutwardItemName.RED_LADYS_DAGGER)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_SCARLET_LICHS_IDOL, OutwardItemName.KRYPTEIA_TOMB_KEY)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_SCEPTER_OF_THE_CRUEL_PRIEST, OutwardItemName.SEALED_MACE)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_SEALED_MACE, OutwardItemName.SMELLY_SEALED_BOX)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_SHRIEK, OutwardItemName.RUSTED_SPEAR)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_TOKEBAKICIT, OutwardItemName.UNUSUAL_KNUCKLES)
        self.add_location_item_requirement(OutwardLocationName.SPAWN_WARM_AXE, OutwardItemName.MYRMITAUR_HAVEN_GATE_KEY)

        # tier 2+ skill checks

        for location_name, (_, tier) in self.skill_sanity_location_info.items():
            if tier > 1:
                if self.options.num_breakthough_points.value < len(OutwardLocationGroup.SKILL_TRAINER_INTERACT):
                    self.set_location_missable(location_name)
                else:
                    self.add_location_item_requirement(location_name, OutwardItemName.BREAKTHROUGH_POINT, count=len(OutwardLocationGroup.SKILL_TRAINER_INTERACT))

        # missable locations

        self.set_location_missable(OutwardLocationName.BURAC_FREE_SKILL)
        self.set_location_missable(OutwardLocationName.TRAIN_SAMANTHA_TURNBULL)
        self.set_location_missable(OutwardLocationName.TRAIN_ANTHONY_BERTHELOT)
        self.set_location_missable(OutwardLocationName.TRAIN_PAUL)
        self.set_location_missable(OutwardLocationName.TRAIN_YAN)
        
        # completion condition

        self.multiworld.completion_condition[self.player] = lambda state: state.has(OutwardEventName.MAIN_QUEST_07_COMPLETE, self.player)

    def pre_fill(self):
        for location_name, (item_name, tier) in self.skill_sanity_location_info.items():
            if self.options.skill_sanity.value == self.options.skill_sanity.option_vanilla or (self.options.skill_sanity.value == self.options.skill_sanity.option_tier_one_only and tier > 1):
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
            "skill_sanity": int(self.options.skill_sanity.value),
            "wind_altar_checks": bool(self.options.wind_altar_checks.value),
            "breakthrough_point_checks": bool(self.options.breakthrough_point_checks.value),
        }
