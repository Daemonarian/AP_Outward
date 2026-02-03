"""
Contains information about all the regions in Outward and their connections.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

from BaseClasses import Entrance, Region
from worlds.generic.Rules import add_rule

from .templates import OutwardObjectNamespace, OutwardObjectTemplate

if TYPE_CHECKING:
    from BaseClasses import MultiWorld
    from worlds.generic.Rules import CollectionRule

    from . import OutwardWorld

class OutwardRegion(Region):
    def __init__(self, name: str, multiworld: MultiWorld, player: int):
        template = OutwardRegionTemplate.get_template(name)
        super().__init__(template.name, player, multiworld, template.hint)

    def add_to_world(self, world: OutwardWorld) -> None:
        world.multiworld.regions.append(self)

class OutwardEntrance(Entrance):
    def __init__(self, name: str, player: int):
        template = OutwardEntranceTemplate.get_template(name)
        super().__init__(player, template.name)

    def add_to_world(self, world: OutwardWorld):
        template = OutwardEntranceTemplate.get_template(self.name)
        parent = world.get_region(template.from_region)
        self.parent_region = parent
        parent.exits.append(self)
        self.connect(world.get_region(template.to_region))
        
    def add_rule(self, rule: CollectionRule, combine: str = "and") -> None:
        add_rule(self, rule, combine)

class OutwardRegionTemplate(OutwardObjectTemplate):
    _hint: str | None

    def __init__(self, name: str, hint: str | None = None):
        super().__init__(name)
        self._hint = hint

    @property
    def hint(self) -> str | None:
        return self._hint

class OutwardEntranceTemplate(OutwardObjectTemplate):
    _from_region: str
    _to_region: str

    def __init__(self, name: str, from_region: str, to_region: str):
        super().__init__(name)
        self._from_region = from_region
        self._to_region = to_region

    @property
    def from_region(self) -> str:
        return self._from_region

    @property
    def to_region(self) -> str:
        return self._to_region

region = OutwardRegionTemplate.register_template
class OutwardRegionName(OutwardObjectNamespace):
    template = OutwardRegionTemplate

    MAIN_MENU = region("Menu")
    MAIN_GAME_AREA = region("Game Area")
    CALDERA = region("Caldera")
    NEW_SIROCCO = region("New Sirocco")
    
entrance = OutwardEntranceTemplate.register_template
class OutwardEntranceName(OutwardObjectNamespace):
    template = OutwardEntranceTemplate

    ENTER_GAME = entrance("Enter Game", OutwardRegionName.MAIN_MENU, OutwardRegionName.MAIN_GAME_AREA)
    TRAVEL_ROUTE_ENMERKAR_TO_CALDERA = entrance("Travel Route - Enermerkar Forest to the Caldera", OutwardRegionName.MAIN_GAME_AREA, OutwardRegionName.CALDERA)
    DOOR_CALDERA_TO_NEW_SIROCCO = entrance("Door - The Caldera to New Sirocco", OutwardRegionName.CALDERA, OutwardRegionName.NEW_SIROCCO)
