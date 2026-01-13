from typing import Dict, List, NamedTuple
from BaseClasses import Item, ItemClassification
from worlds.AutoWorld import World
from .common import OUTWARD, BASE_ID

class OutwardGameItem(Item):
    game = OUTWARD

    def __init__(self, name: str, world: World, player: int):
        item_data = outward_items_by_name[name]
        classification = item_data.classification
        if callable(classification):
            classification = classification(world, player)
        super().__init__(item_data.name, classification, item_data.code, player)

class ItemData(NamedTuple):
    code: int
    name: str
    classification: ItemClassification

class EventName:
    VICTORY = "Victory"

class ItemName:
    QUEST_LICENSE = "Progressive Quest License"

outward_items: List[ItemData] = [ItemData(*data) for data in [
    (1, EventName.VICTORY, ItemClassification.progression),
    (2, ItemName.QUEST_LICENSE, ItemClassification.progression),
]]

outward_items_by_name: Dict[str, ItemData] = {item.name: item for item in outward_items}
outward_item_name_to_id: Dict[str, int] = {item.name: item.code for item in outward_items}
