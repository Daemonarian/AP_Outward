from typing import Dict, List, Self, TypeVar

from BaseClasses import Location, Region

from .common import OUTWARD
from .event_data import EventData, all_event_data
from .location_data import LocationData, all_location_data

OutwardLocationData = EventData | LocationData

all_outward_locations: List[OutwardLocationData] = list(all_event_data) + list(all_location_data)
outward_locations_by_name: Dict[str, OutwardLocationData] = {location.name: location for location in all_outward_locations}
outward_location_name_to_id: Dict[str, int] = {location.name: location.code for location in all_outward_locations}

T = TypeVar('T', bound=OutwardLocationData)
class OutwardLocation[T](Location):
    game = OUTWARD

    @classmethod
    def from_name(cls, name: str, parent: Region, player: int) -> Self:
        location = outward_locations_by_name[name]

        factory: type[OutwardLocation]
        if isinstance(location, EventData):
            factory = OutwardEventLocation
        elif isinstance(location, LocationData):
            factory = OutwardGameLocation
        else:
            raise TypeError(f"`data` should be an instance of `OutwardLocationData`, not `{location.__class__.__name__}`")

        return factory(location, parent, player)

    def __init__(self, location_data: T, parent: Region, player: int):
        self._location_data = location_data
        super().__init__(player, location_data.name, location_data.code, parent)

    @property
    def location_data(self) -> T:
        return self._location_data

class OutwardEventLocation(OutwardLocation[EventData]):
    @classmethod
    def from_name(cls, name: str, parent: Region, player: int) -> Self:
        event_data = outward_locations_by_name[name]
        if not isinstance(event_data, EventData):
            raise ValueError("`name` should be the name of an event, not a location")
        return cls(event_data, parent, player)

    def __init__(self, event_data: EventData, parent: Region, player: int):
        super().__init__(event_data, parent, player)

class OutwardGameLocation(OutwardLocation[LocationData]):
    @classmethod
    def from_name(cls, name: str, parent: Region, player: int) -> Self:
        location_data = outward_locations_by_name[name]
        if not isinstance(location_data, LocationData):
            raise ValueError("`name` should be the name of a location, not an event")
        return cls(location_data, parent, player)

    def __init__(self, location_data: LocationData, parent: Region, player: int):
        super().__init__(location_data, parent, player)
