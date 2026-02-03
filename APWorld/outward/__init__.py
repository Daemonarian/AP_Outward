from __future__ import annotations
from typing import TYPE_CHECKING

from worlds.AutoWorld import World

from .common import OUTWARD
from .events import OutwardEvent, OutwardEventName
from .items import OutwardGameItem, OutwardItemName
from .locations import OutwardGameLocation, OutwardLocation, OutwardLocationName
from .options import OutwardOptions
from .regions import OutwardEntrance, OutwardEntranceName, OutwardRegion, OutwardRegionName

if TYPE_CHECKING:
    from collections.abc import Iterable
    from typing import Any

    from worlds.generic.Rules import CollectionRule

class OutwardWorld(World):
    game = OUTWARD
    
    # required maps
    item_name_to_id = OutwardItemName.get_name_to_id()
    location_name_to_id = OutwardLocationName.get_name_to_id()
    
    # options
    options_dataclass = OutwardOptions
    options: OutwardOptions

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

    def add_entrance_item_requirement(self, entrance_name: str, item_name: str, count: int = 1, combine: str = "and") -> None:
        self.add_entrance_access_rule(entrance_name, lambda state: state.has(item_name, self.player, count), combine)

    def add_location_item_requirement(self, location_name: str, item_name: str, count: int = 1, combine: str = "and") -> None:
        self.add_location_access_rule(location_name, lambda state: state.has(item_name, self.player, count), combine)

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
        self.add_item(OutwardItemName.SCARLET_DAGGER)
        self.add_item(OutwardItemName.SCARLET_GEM)
        self.add_item(OutwardItemName.SCARLET_LICHS_IDOL)
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
        self.add_item(OutwardItemName.THRICE_WROUGHT_HALBERD)
        self.add_item(OutwardItemName.THRICE_WROUGHT_HALBERD)
        self.add_item(OutwardItemName.TOKEBAKICIT)
        self.add_item(OutwardItemName.TSAR_ARMOR)
        self.add_item(OutwardItemName.TSAR_BOOTS)
        self.add_item(OutwardItemName.TSAR_FISTS)
        self.add_item(OutwardItemName.TSAR_HELM)
        self.add_item(OutwardItemName.WERLIG_SPEAR)
        self.add_item(OutwardItemName.WILL_O_WISP)
        self.add_item(OutwardItemName.WORLDEDGE_GREATAXE)
        self.add_item(OutwardItemName.ZHORNS_DEMON_SHIELD)
        self.add_item(OutwardItemName.ZHORNS_GLOWSTONE_DAGGER)
        self.add_item(OutwardItemName.ZHORNS_HUNTING_BACKPACK)

        # useful skills

        self.add_item(OutwardItemName.CURSE_HEX)
        self.add_item(OutwardItemName.POSSESSED_SKILL)

        # filler

        filler_count = 30
        for _ in range(filler_count):
            self.add_item(OutwardItemName.SILVER_CURRENCY)

    def create_regions(self):
        for region_name in OutwardRegionName.get_names():
            self.add_region(region_name)
        for entrance_name in OutwardEntranceName.get_names():
            self.add_entrance(entrance_name)
        for event_name in OutwardEventName.get_names():
            self.add_event(event_name)
        for location_name in OutwardLocationName.get_names():
            self.add_location(location_name)

    def set_rules(self):
        self.add_entrance_item_requirement(OutwardEntranceName.TRAVEL_ROUTE_ENMERKAR_TO_CALDERA, OutwardEventName.MAIN_QUEST_07_COMPLETE)

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

        self.multiworld.completion_condition[self.player] = lambda state: state.has(OutwardEventName.MAIN_QUEST_07_COMPLETE, self.player)

    def fill_slot_data(self) -> dict[str, Any]:
        return {
            "death_link": bool(self.options.death_link.value != 0)
        }
