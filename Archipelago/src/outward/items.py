from typing import Dict, List, Self, TypeVar

from BaseClasses import Item, ItemClassification
from worlds.AutoWorld import World

from .common import OUTWARD
from .event_data import EventData, all_event_data
from .item_data import ItemData, all_item_data

OutwardItemData = EventData | ItemData

all_outward_items: List[OutwardItemData] = list(all_event_data) + list(all_item_data)
outward_items_by_name: Dict[str, OutwardItemData] = {item.name: item for item in all_outward_items}
outward_item_name_to_id: Dict[str, int] = {item.name: item.code for item in all_outward_items}

T = TypeVar('T', bound=OutwardItemData)
class OutwardItem[T](Item):
    game = OUTWARD

    @classmethod
    def from_name(cls, name: str, world: World, player: int) -> Self:
        outward_item_data = outward_items_by_name[name]

        factory: Self
        if isinstance(outward_item_data, EventData):
            factory = OutwardEventItem
        elif isinstance(outward_item_data, ItemData):
            factory = OutwardGameItem
        else:
            raise TypeError(f"`data` should be an instance of `OutwardItemData`, not `{outward_item_data.__class__.__name__}`")

        return factory(outward_item_data, world, player)

    def __init__(self, data: T, classification: ItemClassification, world: World, player: int):
        if callable(classification):
            classification = classification(world, player)
        super().__init__(data.name, classification, data.code, player)

class OutwardEventItem(OutwardItem[EventData]):
    @classmethod
    def from_name(cls, name: str, world: World, player: int) -> Self:
        event_data = outward_items_by_name[name]
        if not isinstance(event_data, EventData):
            raise ValueError("`name` should be the name of an event, not an item")
        return cls(event_data, world, player)

    def __init__(self, event_data: EventData, world: World, player: int):
        super().__init__(event_data, ItemClassification.progression, world, player)

class OutwardGameItem(OutwardItem[ItemData]):
    @classmethod
    def from_name(cls, name: str, world: World, player: int) -> Self:
        item_data = outward_items_by_name[name]
        if not isinstance(item_data, ItemData):
            raise ValueError("`name` should be the name of an item, not an event")
        return cls(item_data, world, player)

    def __init__(self, item_data: ItemData, world: World, player: int):
        super().__init__(item_data, item_data.classification, world, player)
