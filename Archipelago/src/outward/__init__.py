from typing import Tuple

from BaseClasses import ItemClassification, Region, Entrance
from worlds.AutoWorld import World
from worlds.generic.Rules import add_rule

from .common import OUTWARD
from .event_data import EventName, all_event_data
from .location_data import LocationName, all_location_data
from .item_data import ItemName, all_item_data
from .items import OutwardEventItem, OutwardGameItem, outward_item_name_to_id
from .locations import OutwardEventLocation, OutwardGameLocation, outward_location_name_to_id
from .options import OutwardOptions

class OutwardWorld(World):
    game = OUTWARD
    
    # Required maps
    item_name_to_id = outward_item_name_to_id
    location_name_to_id = outward_location_name_to_id
    
    # Options
    options_dataclass = OutwardOptions
    options: OutwardOptions

    def create_item(self, name: str) -> OutwardGameItem:
        item = OutwardGameItem.from_name(name, self, self.player)
        self.multiworld.itempool.append(item)
        return item

    def create_region(self, name: str) -> Region:
        region = Region(name, self.player, self.multiworld)
        self.multiworld.regions.append(region)
        return region

    def create_location(self, name: str, parent: Region) -> OutwardGameLocation:
        location = OutwardGameLocation.from_name(name, parent, self.player)
        parent.locations.append(location)
        return location

    def create_event(self, name: str, parent: Region) -> Tuple[OutwardEventLocation, OutwardEventItem]:
        location = OutwardEventLocation.from_name(name, parent, self.player)
        parent.locations.append(location)
        item = OutwardEventItem.from_name(name, self, self.player)
        location.place_locked_item(item)
        return (location, item)

    def create_connection(self, name: str, from_region: Region, to_region: Region) -> Entrance:
        connection = Entrance(self.player, name, from_region)
        from_region.exits.append(connection)
        connection.connect(to_region)
        return connection

    def create_items(self):
        for item_data in all_item_data:
            if item_data.classification & ItemClassification.filler == 0:
                for _ in range(item_data.count):
                    self.create_item(item_data.name)

        # Fillers
        location_count = len(self.multiworld.get_locations(self.player))
        while len(self.multiworld.itempool) < location_count:
            self.create_item(ItemName.SILVER_CURRENCY)

    def create_regions(self):
        menu = self.create_region("Menu")
        game_area = self.create_region("Game Area")
        self.create_connection("Enter Game", menu, game_area)

        for event in all_event_data:
            self.create_event(event.name, game_area)

        for location in all_location_data:
            self.create_location(location.name, game_area)

    def set_rules(self):
        access_rules = {
            # main quest events
            EventName.MAIN_QUEST_02_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_01_COMPLETE, self.player),
            EventName.MAIN_QUEST_03_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_02_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 1),
            EventName.MAIN_QUEST_04_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_03_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 2),
            EventName.MAIN_QUEST_05_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_04_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 3),
            EventName.MAIN_QUEST_06_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_05_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 4),
            EventName.MAIN_QUEST_07_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_06_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 5),
            EventName.MAIN_QUEST_08_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_07_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 6),
            EventName.MAIN_QUEST_09_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_08_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 7),
            EventName.MAIN_QUEST_10_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_09_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 8),
            EventName.MAIN_QUEST_11_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_10_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 9),
            EventName.MAIN_QUEST_12_COMPLETE: lambda state: state.has(EventName.MAIN_QUEST_11_COMPLETE, self.player) and state.has(ItemName.QUEST_LICENSE, self.player, 10),

            # quest completion events
            LocationName.QUEST_MAIN_01: lambda state: state.has(EventName.MAIN_QUEST_01_COMPLETE, self.player),
            LocationName.QUEST_MAIN_02: lambda state: state.has(EventName.MAIN_QUEST_02_COMPLETE, self.player),
            LocationName.QUEST_MAIN_03: lambda state: state.has(EventName.MAIN_QUEST_03_COMPLETE, self.player),
            LocationName.QUEST_MAIN_04: lambda state: state.has(EventName.MAIN_QUEST_04_COMPLETE, self.player),
            LocationName.QUEST_MAIN_05: lambda state: state.has(EventName.MAIN_QUEST_05_COMPLETE, self.player),
            LocationName.QUEST_MAIN_06: lambda state: state.has(EventName.MAIN_QUEST_06_COMPLETE, self.player),
            LocationName.QUEST_MAIN_07: lambda state: state.has(EventName.MAIN_QUEST_07_COMPLETE, self.player),
            LocationName.QUEST_MAIN_08: lambda state: state.has(EventName.MAIN_QUEST_08_COMPLETE, self.player),
            LocationName.QUEST_MAIN_09: lambda state: state.has(EventName.MAIN_QUEST_09_COMPLETE, self.player),
            LocationName.QUEST_MAIN_10: lambda state: state.has(EventName.MAIN_QUEST_10_COMPLETE, self.player),
            LocationName.QUEST_MAIN_11: lambda state: state.has(EventName.MAIN_QUEST_11_COMPLETE, self.player),
            LocationName.QUEST_MAIN_12: lambda state: state.has(EventName.MAIN_QUEST_12_COMPLETE, self.player),

            LocationName.QUEST_PARALLEL_BLOOD_UNDER_THE_SUN: lambda state: state.has(EventName.MAIN_QUEST_04_COMPLETE, self.player),
            LocationName.QUEST_PARALLEL_PURIFIER: lambda state: state.has(EventName.MAIN_QUEST_02_COMPLETE, self.player),
            LocationName.QUEST_PARALLEL_VENDAVEL_QUEST: lambda state: state.has(EventName.MAIN_QUEST_02_COMPLETE, self.player),
            LocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_1: lambda state: state.has(EventName.MAIN_QUEST_04_COMPLETE, self.player),
            LocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_2: lambda state: state.has(EventName.MAIN_QUEST_04_COMPLETE, self.player),
            LocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_3: lambda state: state.has(EventName.MAIN_QUEST_04_COMPLETE, self.player),

            LocationName.SPAWN_RUINED_HALBERD: lambda state: state.has(EventName.MAIN_QUEST_07_COMPLETE, self.player),
            LocationName.SPAWN_MYSTERIOUS_LONG_BLADE: lambda state: state.has(EventName.MAIN_QUEST_07_COMPLETE, self.player),
            LocationName.SPAWN_DEPOWERED_BLUDGEON: lambda state: state.has(EventName.MAIN_QUEST_07_COMPLETE, self.player),
            LocationName.SPAWN_FOSSILIZED_GREATAXE: lambda state: state.has(EventName.MAIN_QUEST_07_COMPLETE, self.player),
        }
        for loc in self.multiworld.get_locations(self.player):
            if loc.name in access_rules:
                add_rule(loc, access_rules[loc.name])

        self.multiworld.completion_condition[self.player] = lambda state: state.has(EventName.MAIN_QUEST_07_COMPLETE, self.player)
