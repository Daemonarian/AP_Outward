from __future__ import annotations
from typing import TYPE_CHECKING

from ..common import OUTWARD
from ..events import OutwardEventLocation

from test.bases import WorldTestBase

if TYPE_CHECKING:
    from collections.abc import Iterable

    from BaseClasses import Item

class OutwardWorldTestBase(WorldTestBase):
    game = OUTWARD

    def complete_by_name(self, names: str | Iterable[str]) -> list[Item]:
        """
        Complete an event by collecting the corresponding item.
        """

        if isinstance(names, str):
            names = (names,)
        event_items = [self.multiworld.get_location(name, self.player) for name in names]
        self.collect(event_items)
        return event_items

    def collect_single_by_name(self, names: str | Iterable[str]) -> list[Item]:
        """
        Collect exactly one item per name.
        """

        if isinstance(names, str):
            names = (names,)
        items = [self.get_item_by_name(name) for name in names]
        self.collect(items)
        return items
