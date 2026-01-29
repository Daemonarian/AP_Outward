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
        return OutwardGameItem(self, name)

    def create_region(self, name: str) -> OutwardRegion:
        return OutwardRegion(self, name)

    def create_location(self, name: str) -> OutwardGameLocation:
        return OutwardGameLocation(self, name)

    def create_event(self, name: str) -> OutwardEvent:
        return OutwardEvent(self, name)

    def create_entrance(self, name: str) -> OutwardEntrance:
        return OutwardEntrance(self, name)

    def add_entrance_access_rule(self, name: str, rule: CollectionRule, combine: str = "and") -> None:
        self.get_entrance(name).add_rule(rule, combine)

    def add_location_access_rule(self, name: str, rule: CollectionRule, combine: str = "and") -> None:
        self.get_location(name).add_rule(rule, combine)

    def create_items(self):
        # key items

        for _ in range(10):
            self.create_item(OutwardItemName.QUEST_LICENSE)

        # useful gear

        self.create_item(OutwardItemName.ANTIQUE_PLATE_BOOTS)
        self.create_item(OutwardItemName.ANTIQUE_PLATE_GARB)
        self.create_item(OutwardItemName.ANTIQUE_PLATE_SALLET)
        self.create_item(OutwardItemName.BLUE_SAND_ARMOR)
        self.create_item(OutwardItemName.BLUE_SAND_BOOTS)
        self.create_item(OutwardItemName.BLUE_SAND_HELM)
        self.create_item(OutwardItemName.CEREMONIAL_BOW)
        self.create_item(OutwardItemName.COPAL_ARMOR)
        self.create_item(OutwardItemName.COPAL_BOOTS)
        self.create_item(OutwardItemName.COPAL_HELM)
        self.create_item(OutwardItemName.DEPOWERED_BLUDGEON)
        self.create_item(OutwardItemName.DREAMER_HALBERD)
        self.create_item(OutwardItemName.FOSSILIZED_GREATAXE)
        self.create_item(OutwardItemName.GOLD_LICH_ARMOR)
        self.create_item(OutwardItemName.GOLD_LICH_BOOTS)
        self.create_item(OutwardItemName.GOLD_LICH_MASK)
        self.create_item(OutwardItemName.GOLD_LICH_SPEAR)
        self.create_item(OutwardItemName.JADE_LICH_BOOTS)
        self.create_item(OutwardItemName.JADE_LICH_MASK)
        self.create_item(OutwardItemName.JADE_LICH_ROBES)
        self.create_item(OutwardItemName.JADE_LICH_STAFF)
        self.create_item(OutwardItemName.LIGHT_MENDERS_BACKPACK)
        self.create_item(OutwardItemName.MERTONS_FIREPOKER)
        self.create_item(OutwardItemName.MYSTERIOUS_CHAKRAM)
        self.create_item(OutwardItemName.MYSTERIOUS_LONG_BLADE)
        self.create_item(OutwardItemName.ORNATE_BONE_SHIELD)
        self.create_item(OutwardItemName.PALLADIUM_ARMOR)
        self.create_item(OutwardItemName.PALLADIUM_BOOTS)
        self.create_item(OutwardItemName.PALLADIUM_HELM)
        self.create_item(OutwardItemName.PETRIFIED_WOOD_ARMOR)
        self.create_item(OutwardItemName.PETRIFIED_WOOD_BOOTS)
        self.create_item(OutwardItemName.PETRIFIED_WOOD_HELM)
        self.create_item(OutwardItemName.PILLAR_GREATHAMMER)
        self.create_item(OutwardItemName.PORCELAIN_FISTS)
        self.create_item(OutwardItemName.POSSESSED_SKILL)
        self.create_item(OutwardItemName.RUINED_HALBERD)
        self.create_item(OutwardItemName.RUSTED_SPEAR)
        self.create_item(OutwardItemName.RUST_LICH_ARMOR)
        self.create_item(OutwardItemName.RUST_LICH_BOOTS)
        self.create_item(OutwardItemName.RUST_LICH_HELMET)
        self.create_item(OutwardItemName.SEALED_MACE)
        self.create_item(OutwardItemName.STRANGE_RUSTED_SWORD)
        self.create_item(OutwardItemName.SUNFALL_AXE)
        self.create_item(OutwardItemName.TENEBROUS_ARMOR)
        self.create_item(OutwardItemName.TENEBROUS_BOOTS)
        self.create_item(OutwardItemName.TENEBROUS_HELM)
        self.create_item(OutwardItemName.THRICE_WROUGHT_HALBERD)
        self.create_item(OutwardItemName.TSAR_ARMOR)
        self.create_item(OutwardItemName.TSAR_BOOTS)
        self.create_item(OutwardItemName.TSAR_FISTS)
        self.create_item(OutwardItemName.TSAR_HELM)
        self.create_item(OutwardItemName.UNUSUAL_KNUCKLES)
        self.create_item(OutwardItemName.WARM_AXE)
        self.create_item(OutwardItemName.WILL_O_WISP)

        # useful skills

        self.create_item(OutwardItemName.CURSE_HEX)

        # filler

        location_count = len(self.multiworld.get_locations(self.player))
        while len(self.multiworld.itempool) < location_count:
            self.create_item(OutwardItemName.SILVER_CURRENCY)

    def create_regions(self):
        for region_name in OutwardRegionName.get_names():
            self.create_region(region_name)
        for entrance_name in OutwardEntranceName.get_names():
            self.create_entrance(entrance_name)
        for event_name in OutwardEventName.get_names():
            self.create_event(event_name)
        for location_name in OutwardLocationName.get_names():
            self.create_location(location_name)

    def set_rules(self):
        self.add_entrance_access_rule(OutwardEntranceName.TRAVEL_ROUTE_ENMERKAR_TO_CALDERA, lambda state: state.has(OutwardEventName.MAIN_QUEST_07_COMPLETE, self.player))

        # main quest events
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_02_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_01_COMPLETE, self.player))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_03_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_02_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 1))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_04_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_03_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 2))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_05_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_04_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 3))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_06_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_05_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 4))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_07_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_06_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 5))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_08_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_07_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 6))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_09_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_08_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 7))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_10_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_09_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 8))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_11_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_10_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 9))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_12_COMPLETE, lambda state: state.has(OutwardEventName.MAIN_QUEST_11_COMPLETE, self.player) and state.has(OutwardItemName.QUEST_LICENSE, self.player, 10))
        
        # quest licenses
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_03_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 1))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_04_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 2))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_05_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 3))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_06_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 4))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_07_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 5))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_08_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 6))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_09_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 7))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_10_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 8))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_11_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 9))
        self.add_location_access_rule(OutwardEventName.MAIN_QUEST_12_COMPLETE, lambda state: state.has(OutwardItemName.QUEST_LICENSE, self.player, 10))

        # quest completion events
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_01, lambda state: state.has(OutwardEventName.MAIN_QUEST_01_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_02, lambda state: state.has(OutwardEventName.MAIN_QUEST_02_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_03, lambda state: state.has(OutwardEventName.MAIN_QUEST_03_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_04, lambda state: state.has(OutwardEventName.MAIN_QUEST_04_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_05, lambda state: state.has(OutwardEventName.MAIN_QUEST_05_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_06, lambda state: state.has(OutwardEventName.MAIN_QUEST_06_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_07, lambda state: state.has(OutwardEventName.MAIN_QUEST_07_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_08, lambda state: state.has(OutwardEventName.MAIN_QUEST_08_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_09, lambda state: state.has(OutwardEventName.MAIN_QUEST_09_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_10, lambda state: state.has(OutwardEventName.MAIN_QUEST_10_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_11, lambda state: state.has(OutwardEventName.MAIN_QUEST_11_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_MAIN_12, lambda state: state.has(OutwardEventName.MAIN_QUEST_12_COMPLETE, self.player))

        self.add_location_access_rule(OutwardLocationName.QUEST_PARALLEL_BLOOD_UNDER_THE_SUN, lambda state: state.has(OutwardEventName.MAIN_QUEST_04_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_PARALLEL_PURIFIER, lambda state: state.has(OutwardEventName.MAIN_QUEST_02_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_PARALLEL_VENDAVEL_QUEST, lambda state: state.has(OutwardEventName.MAIN_QUEST_02_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_1, lambda state: state.has(OutwardEventName.MAIN_QUEST_04_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_2, lambda state: state.has(OutwardEventName.MAIN_QUEST_04_COMPLETE, self.player))
        self.add_location_access_rule(OutwardLocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_3, lambda state: state.has(OutwardEventName.MAIN_QUEST_04_COMPLETE, self.player))

        self.multiworld.completion_condition[self.player] = lambda state: state.has(OutwardEventName.MAIN_QUEST_07_COMPLETE, self.player)
