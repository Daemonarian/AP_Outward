"""
Defines the internal "events" that we use in our Outward APWorld.

These are actually implemented as a locked location/item pair.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

from BaseClasses import ItemClassification

from .templates import OutwardObjectNamespace, OutwardObjectTemplate
from .items import OutwardItem
from .locations import OutwardLocation, OutwardRegionName

if TYPE_CHECKING:
    from BaseClasses import Region
    from worlds.generic.Rules import CollectionRule

    from . import OutwardWorld
    
class OutwardEvent:
    _location: OutwardEventLocation
    _item: OutwardEventItem

    def __init__(self, world: OutwardWorld, name: str):
        template = OutwardEventTemplate.get_template(name)
        player = world.player
        parent = world.get_region(template.region)
        self._location = OutwardEventLocation(player, name, parent)
        self._item = OutwardEventItem(name, player)
        self._location.place_locked_item(self._item)
        parent.locations.append(self._location)

    @property
    def location(self) -> OutwardEventLocation:
        return self._location

    @property
    def item(self) -> OutwardEventItem:
        return self._item

    def add_rule(self, rule: CollectionRule, combine: str = "and") -> None:
        self.location.add_rule(rule, combine)

class OutwardEventLocation(OutwardLocation):
    def __init__(self, player: int, name: str, parent: Region):
        super().__init__(player, name, None, parent)

class OutwardEventItem(OutwardItem):
    def __init__(self, name: str, player: int):
        super().__init__(name, ItemClassification.progression, None, player)

class OutwardEventTemplate(OutwardObjectTemplate):
    _region: str
    def __init__(self, name: str, region: str):
        super().__init__(name)
        self._region = region
    @property
    def region(self) -> str:
        return self._region

event = OutwardEventTemplate.register_template
class OutwardEventName(OutwardObjectNamespace):
    template = OutwardEventTemplate

    # quest completion events

    MAIN_QUEST_01_COMPLETE = event("Event - Main Quest 1 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_02_COMPLETE = event("Event - Main Quest 2 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_03_COMPLETE = event("Event - Main Quest 3 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_04_COMPLETE = event("Event - Main Quest 4 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_05_COMPLETE = event("Event - Main Quest 5 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_06_COMPLETE = event("Event - Main Quest 6 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_07_COMPLETE = event("Event - Main Quest 7 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_08_COMPLETE = event("Event - Main Quest 8 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_09_COMPLETE = event("Event - Main Quest 9 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_10_COMPLETE = event("Event - Main Quest 10 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_11_COMPLETE = event("Event - Main Quest 11 - Complete", OutwardRegionName.MAIN_GAME_AREA)
    MAIN_QUEST_12_COMPLETE = event("Event - Main Quest 12 - Complete", OutwardRegionName.MAIN_GAME_AREA)
