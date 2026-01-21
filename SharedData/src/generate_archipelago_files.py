from common import archipelago_event_file, archipelago_location_file, archipelago_item_file, write_code_file
from schema import load_data

data_schema = load_data()

write_code_file(archipelago_event_file, f"""
from typing import List, NamedTuple

class EventName:
    {'\n    '.join(f"{event.key.upper} = {repr(event.name)}" for event in sorted(data_schema.events, key=lambda x: x.key.upper))}

class EventData(NamedTuple):
    code: int
    name: str

all_event_data: List[EventData] = [EventData(*data) for data in [
    {'\n    '.join(f"({event.code}, EventName.{event.key.upper})," for event in sorted(data_schema.events, key=lambda x: x.code))}
]]
""")

write_code_file(archipelago_location_file, f"""
from typing import List, NamedTuple

class LocationName:
    {'\n    '.join(f"{location.key.upper} = {repr(location.name)}" for location in sorted(data_schema.locations, key=lambda x: x.key.upper))}

class LocationData(NamedTuple):
    code: int
    name: str

all_location_data: List[LocationData] = [LocationData(*data) for data in [
    {'\n    '.join(f"({location.code}, LocationName.{location.key.upper})," for location in sorted(data_schema.locations, key=lambda x: x.code))}
]]
""")

write_code_file(archipelago_item_file, f"""
from typing import List, NamedTuple

from BaseClasses import ItemClassification

class ItemName:
    {'\n    '.join(f"{item.key.upper} = {repr(item.name)}" for item in sorted(data_schema.items, key=lambda x: x.key.upper))}

class ItemData(NamedTuple):
    code: int
    name: str
    classification: ItemClassification
    count: int

all_item_data: List[ItemData] = [ItemData(*data) for data in [
    {'\n    '.join(f"({item.code}, ItemName.{item.key.upper}, ItemClassification.{item.classification}, {item.count})," for item in sorted(data_schema.items, key=lambda x: x.code))}
]]
""")
