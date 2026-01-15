from typing import Dict, List, NamedTuple
from BaseClasses import Location, Region
from .common import OUTWARD

class OutwardLocation(Location):
    game = OUTWARD

    def __init__(self, name: str, parent: Region, player: int):
        location_data = outward_locations_by_name[name]
        super().__init__(player, location_data.name, location_data.code, parent)

class LocationData(NamedTuple):
    code: int
    name: str

class LocationName:
    EVENT_VICTORY = "Victory"
    QUEST_MAIN_1 = "Main Quest 1: Call to Adventure"
    QUEST_MAIN_2 = "Main Quest 2: Join Faction Quest"
    QUEST_MAIN_3 = "Main Quest 3: Faction Quest 1"
    QUEST_MAIN_4 = "Main Quest 4: Faction Quest 2"
    QUEST_MAIN_5 = "Main Quest 5: Faction Quest 3"
    QUEST_MAIN_6 = "Main Quest 6: Faction Quest 4"
    QUEST_MAIN_7 = "Main Quest 7: A Fallen City"
    QUEST_MAIN_8 = "Main Quest 8: Up The Ladder"
    QUEST_MAIN_9 = "Main Quest 9: Stealing Fire"
    QUEST_MAIN_10 = "Main Quest 10: Liberate the Sun"
    QUEST_MAIN_11 = "Main Quest 11: Vengeful Ouroboros"
    QUEST_SIDE_ALCHEMY_COLD_STONE = "Quest: Alchemy: Cold Stone"

outward_locations: List[LocationData] = [LocationData(*data) for data in [
    (1, LocationName.EVENT_VICTORY),
    (2, LocationName.QUEST_MAIN_1),
    (3, LocationName.QUEST_MAIN_2),
    (4, LocationName.QUEST_MAIN_3),
    (5, LocationName.QUEST_MAIN_4),
    (6, LocationName.QUEST_MAIN_5),
    (7, LocationName.QUEST_MAIN_6),
    (8, LocationName.QUEST_MAIN_7),
    (9, LocationName.QUEST_MAIN_8),
    (10, LocationName.QUEST_MAIN_9),
    (11, LocationName.QUEST_MAIN_10),
    (12, LocationName.QUEST_MAIN_11),
    (13, LocationName.QUEST_SIDE_ALCHEMY_COLD_STONE),
]]

outward_locations_by_name: Dict[str, LocationData] = {location.name: location for location in outward_locations}
outward_location_name_to_id: Dict[str, int] = {location.name: location.code for location in outward_locations}
