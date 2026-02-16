"""
Defines the internal "events" that we use in our Outward APWorld.

These are actually implemented as a locked location/item pair.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

from BaseClasses import ItemClassification

from .templates import OutwardObjectNamespace, OutwardObjectTemplate
from .factions import OutwardFaction
from .items import OutwardItem
from .locations import OutwardLocation, OutwardRegionName

if TYPE_CHECKING:
    from BaseClasses import Region
    from worlds.generic.Rules import CollectionRule

    from . import OutwardWorld
    
class OutwardEvent:
    _template: OutwardEventTemplate
    _location: OutwardEventLocation
    _item: OutwardEventItem

    def __init__(self, name: str, player: int):
        self._template = OutwardEventTemplate.get_template(name)
        self._location = OutwardEventLocation(self, player)
        self._item = OutwardEventItem(self, player)

    @property
    def template(self) -> OutwardEventTemplate:
        return self._template

    @property
    def faction(self) -> OutwardFaction:
        return self.template.faction

    @property
    def location(self) -> OutwardEventLocation:
        return self._location

    @property
    def item(self) -> OutwardEventItem:
        return self._item

    @property
    def name(self) -> str:
        return self.template.name

    def add_to_world(self, world: OutwardWorld, *, skip_faction_check: bool = False) -> None:
        if skip_faction_check or world.get_allowed_factions() & self.faction != 0:
            parent = world.get_region(self.template.region)
            self.location.parent_region = parent
            parent.locations.append(self.location)
            self.location.place_locked_item(self.item)

    def add_rule(self, rule: CollectionRule, combine: str = "and") -> None:
        self.location.add_rule(rule, combine)

class OutwardEventLocation(OutwardLocation):
    _event: OutwardEvent

    def __init__(self, event: OutwardEvent, player: int):
        super().__init__(player, event.name)
        self._event = event

    @property
    def event(self) -> OutwardEvent:
        return self._event

    @property
    def template(self) -> OutwardEventTemplate:
        return self.event.template

    @property
    def faction(self) -> OutwardFaction:
        return self.event.faction

class OutwardEventItem(OutwardItem):
    _event: OutwardEvent

    def __init__(self, event: OutwardEvent, player: int):
        super().__init__(event.name, ItemClassification.progression, None, player)
        self._event = event

    @property
    def event(self) -> OutwardEvent:
        return self._event
    
    @property
    def template(self) -> OutwardEventTemplate:
        return self.event.template

    @property
    def faction(self) -> OutwardFaction:
        return self.event.faction

class OutwardEventTemplate(OutwardObjectTemplate):
    _region: str
    _faction: OutwardFaction
    def __init__(self, name: str, region: str, *, faction: OutwardFaction = OutwardFaction.AllFactions):
        super().__init__(name)
        self._region = region
        self._faction = faction
    @property
    def region(self) -> str:
        return self._region
    @property
    def faction(self) -> OutwardFaction:
        return self._faction

event = OutwardEventTemplate.register_template
class OutwardEventName(OutwardObjectNamespace):
    template = OutwardEventTemplate

    # quest completion events

    MAIN_QUEST_01_PREREQ = event("Event - Main Quest 1 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_02_PREREQ = event("Event - Main Quest 2 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_03_PREREQ = event("Event - Main Quest 3 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_04_PREREQ = event("Event - Main Quest 4 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_05_PREREQ = event("Event - Main Quest 5 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_06_PREREQ = event("Event - Main Quest 6 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_07_PREREQ = event("Event - Main Quest 7 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_08_PREREQ = event("Event - Main Quest 8 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_09_PREREQ = event("Event - Main Quest 9 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_10_PREREQ = event("Event - Main Quest 10 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_11_PREREQ = event("Event - Main Quest 11 - Prerequisites", OutwardRegionName.UNPLACED)
    MAIN_QUEST_12_PREREQ = event("Event - Main Quest 12 - Prerequisites", OutwardRegionName.UNPLACED)

    MAIN_QUEST_01_START = event("Event - Main Quest 1 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_02_START = event("Event - Main Quest 2 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_03_START = event("Event - Main Quest 3 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_04_START = event("Event - Main Quest 4 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_05_START = event("Event - Main Quest 5 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_06_START = event("Event - Main Quest 6 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_07_START = event("Event - Main Quest 7 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_08_START = event("Event - Main Quest 8 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_09_START = event("Event - Main Quest 9 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_10_START = event("Event - Main Quest 10 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_11_START = event("Event - Main Quest 11 - Start", OutwardRegionName.UNPLACED)
    MAIN_QUEST_12_START = event("Event - Main Quest 12 - Start", OutwardRegionName.UNPLACED)

    MAIN_QUEST_01_COMPLETE = event("Event - Main Quest 1 - Complete", OutwardRegionName.CIERZO)
    MAIN_QUEST_02_COMPLETE = event("Event - Main Quest 2 - Complete", OutwardRegionName.UNPLACED)
    MAIN_QUEST_03_COMPLETE = event("Event - Main Quest 3 - Complete", OutwardRegionName.UNPLACED)
    MAIN_QUEST_04_COMPLETE = event("Event - Main Quest 4 - Complete", OutwardRegionName.UNPLACED)
    MAIN_QUEST_05_COMPLETE = event("Event - Main Quest 5 - Complete", OutwardRegionName.UNPLACED)
    MAIN_QUEST_06_COMPLETE = event("Event - Main Quest 6 - Complete", OutwardRegionName.UNPLACED)
    MAIN_QUEST_07_COMPLETE = event("Event - Main Quest 7 - Complete", OutwardRegionName.MONSOON)
    MAIN_QUEST_08_COMPLETE = event("Event - Main Quest 8 - Complete", OutwardRegionName.NEW_SIROCCO)
    MAIN_QUEST_09_COMPLETE = event("Event - Main Quest 9 - Complete", OutwardRegionName.NEW_SIROCCO)
    MAIN_QUEST_10_COMPLETE = event("Event - Main Quest 10 - Complete", OutwardRegionName.NEW_SIROCCO)
    MAIN_QUEST_11_COMPLETE = event("Event - Main Quest 11 - Complete", OutwardRegionName.NEW_SIROCCO)
    MAIN_QUEST_12_COMPLETE = event("Event - Main Quest 12 - Complete", OutwardRegionName.NEW_SIROCCO)

    PARALLEL_QUEST_BLOOD_UNDER_THE_SUN_COMPLETE = event("Event - Parallel Quest - Blood Under the Sun - Complete", OutwardRegionName.LEVANT)
    PARALLEL_QUEST_PURIFIER_COMPLETE = event("Event - Parallel Quest - Purifier - Complete", OutwardRegionName.ENMERKAR_FOREST)
    PARALLEL_QUEST_VENDAVEL_QUEST_COMPLETE = event("Event - Parallel Quest - Vendavel Quest - Complete", OutwardRegionName.CIERZO)
    PARALLEL_QUEST_RUST_AND_VENGEANCE_COMPLETE = event("Event - Parallel Quest - Rust and Vengeance - Complete", OutwardRegionName.HARMATTAN)

    # Join Faction Quest

    MAIN_QUEST_LOOKING_TO_THE_FUTURE_START = event("Event - Main Quest - Looking to the Future - Start", OutwardRegionName.CIERZO, faction=OutwardFaction.OriginalThree)
    MAIN_QUEST_LOOKING_TO_THE_FUTURE_BC_COMPLETE = event("Event - Main Quest - Looking to the Future - Blue Chamber - Complete", OutwardRegionName.BERG, faction=OutwardFaction.BlueChamber)
    MAIN_QUEST_LOOKING_TO_THE_FUTURE_HK_COMPLETE = event("Event - Main Quest - Looking to the Future - Heroic Kingdom - Complete", OutwardRegionName.LEVANT, faction=OutwardFaction.HeroicKingdom)
    MAIN_QUEST_LOOKING_TO_THE_FUTURE_HM_COMPLETE = event("Event - Main Quest - Looking to the Future - Holy Mission - Complete", OutwardRegionName.MONSOON, faction=OutwardFaction.HolyMission)

    MAIN_QUEST_ENROLLMENT_START = event("Event - Main Quest - Enrollment - Start", OutwardRegionName.HARMATTAN, faction=OutwardFaction.SoroborAcademy)
    MAIN_QUEST_ENROLLMENT_COMPLETE = event("Event - Main Quest - Enrollment - Complete", OutwardRegionName.HARMATTAN, faction=OutwardFaction.SoroborAcademy)

    # friendly immaculate

    FRIENDLY_IMMACULATE_CHERSONESE = event("Event - Immaculate Camp - Chersonese", OutwardRegionName.IMMACULATES_CAMP_CHERSONESE)
    FRIENDLY_IMMACULATE_ENMERKAR_FOREST = event("Event - Immaculate Camp - Enmerkar Forest", OutwardRegionName.IMMACULATES_CAMP_ENMERKAR_FOREST)
    FRIENDLY_IMMACULATE_ABRASSAR = event("Event - Immaculate Camp - Abrassar", OutwardRegionName.IMMACULATES_CAMP_ABRASSAR)
    FRIENDLY_IMMACULATE_HALLOWED_MARSH = event("Event - Immaculate Camp - Hallowed Marsh", OutwardRegionName.IMMACULATES_CAMP_HALLOWED_MARSH)

class OutwardEventGroup:
    GOALS = {
        0: OutwardEventName.MAIN_QUEST_01_COMPLETE,
        1: OutwardEventName.MAIN_QUEST_02_COMPLETE,
        2: OutwardEventName.MAIN_QUEST_03_COMPLETE,
        3: OutwardEventName.MAIN_QUEST_04_COMPLETE,
        4: OutwardEventName.MAIN_QUEST_05_COMPLETE,
        5: OutwardEventName.MAIN_QUEST_06_COMPLETE,
        6: OutwardEventName.MAIN_QUEST_07_COMPLETE,
        7: OutwardEventName.MAIN_QUEST_08_COMPLETE,
        8: OutwardEventName.MAIN_QUEST_09_COMPLETE,
        9: OutwardEventName.MAIN_QUEST_10_COMPLETE,
        10: OutwardEventName.MAIN_QUEST_11_COMPLETE,
        11: OutwardEventName.MAIN_QUEST_12_COMPLETE,

        12: OutwardEventName.PARALLEL_QUEST_BLOOD_UNDER_THE_SUN_COMPLETE,
        13: OutwardEventName.PARALLEL_QUEST_PURIFIER_COMPLETE,
        14: OutwardEventName.PARALLEL_QUEST_VENDAVEL_QUEST_COMPLETE,
        15: OutwardEventName.PARALLEL_QUEST_RUST_AND_VENGEANCE_COMPLETE,
    }