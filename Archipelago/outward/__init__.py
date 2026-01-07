from BaseClasses import Item, ItemClassification, Region, Entrance
from worlds.AutoWorld import World
from worlds.generic.Rules import add_rule
from .common import OUTWARD
from .items import EventName, ItemName, OutwardGameItem, outward_item_name_to_id
from .locations import LocationName, OutwardLocation, outward_location_name_to_id
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
        return OutwardGameItem(name, self, self.player)

    def create_region(self, name: str) -> Region:
        region = Region(name, self.player, self.multiworld)
        self.multiworld.regions.append(region)
        return region

    def create_location(self, name: str, parent: Region) -> OutwardLocation:
        location = OutwardLocation(name, parent, self.player)
        parent.locations.append(location)
        return location

    def create_connection(self, name: str, from_region: Region, to_region: Region) -> Entrance:
        connection = Entrance(self.player, name, from_region)
        from_region.exits.append(connection)
        connection.connect(to_region)
        return connection

    def create_items(self):
        for _ in range(5):
            self.multiworld.itempool.append(self.create_item(ItemName.QUEST_LICENSE))

    def create_regions(self):
        menu = self.create_region("Menu")
        game_area = self.create_region("Game Area")

        for location_name in outward_location_name_to_id.keys():
            location = self.create_location(location_name, game_area)
            if location_name == LocationName.QUEST_6:
                location.place_locked_item(self.create_item(EventName.VICTORY))

        self.create_connection("Enter Game", menu, game_area)

    def set_rules(self):
        access_rules = {
            LocationName.QUEST_2: lambda state: state.has(ItemName.QUEST_LICENSE, self.player, 1),
            LocationName.QUEST_3: lambda state: state.has(ItemName.QUEST_LICENSE, self.player, 2),
            LocationName.QUEST_4: lambda state: state.has(ItemName.QUEST_LICENSE, self.player, 3),
            LocationName.QUEST_5: lambda state: state.has(ItemName.QUEST_LICENSE, self.player, 4),
            LocationName.QUEST_6: lambda state: state.has(ItemName.QUEST_LICENSE, self.player, 5),
        }
        for loc in self.multiworld.get_locations(self.player):
            if loc.name in access_rules:
                add_rule(loc, access_rules[loc.name])

        self.multiworld.completion_condition[self.player] = lambda state: state.has(EventName.VICTORY, self.player)
