from typing import Dict, List, NamedTuple
from BaseClasses import Location, Region
from .common import BASE_ID, OUTWARD

class OutwardLocation(Location):
    game = OUTWARD

    def __init__(self, name: str, parent: Region, player: int):
        location_data = outward_locations_by_name[name]
        super().__init__(player, location_data.name, location_data.code, parent)

class LocationData(NamedTuple):
    code: int
    name: str

class LocationName:
    QUEST_1 =  "Main Quest 1: Call to Adventure"
    QUEST_2 =  "Main Quest 2: Join Faction Quest"
    QUEST_3 =  "Main Quest 3: Faction Quest 1"
    QUEST_4 =  "Main Quest 4: Faction Quest 2"
    QUEST_5 =  "Main Quest 5: Faction Quest 3"
    QUEST_6 =  "Main Quest 6: Faction Quest 4"
    QUEST_7 =  "Main Quest 7: A Fallen City"
    QUEST_8 =  "Main Quest 8: Up The Ladder"
    QUEST_9 =  "Main Quest 9: Stealing Fire"
    QUEST_10 = "Main Quest 10: Liberate the Sun"
    QUEST_11 = "Main Quest 11: Vengeful Ouroboros"

outward_locations: List[LocationData] = [LocationData(BASE_ID + i, name) for i, name in enumerate([
    LocationName.QUEST_1,
    LocationName.QUEST_2,
    LocationName.QUEST_3,
    LocationName.QUEST_4,
    LocationName.QUEST_5,
    LocationName.QUEST_6,
    #LocationName.QUEST_7,
    #LocationName.QUEST_8,
    #LocationName.QUEST_9,
    #LocationName.QUEST_10,
    #LocationName.QUEST_11,
])]

outward_locations_by_name: Dict[str, LocationData] = {location.name: location for location in outward_locations}
outward_location_name_to_id: Dict[str, int] = {location.name: location.code for location in outward_locations}
