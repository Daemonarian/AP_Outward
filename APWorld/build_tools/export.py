"""
This module exports all of this AP world's public bindings.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

import json

from Utils import __version__ as archipelago_version

from outward import OutwardWorld
from outward.items import OutwardItemName
from outward.locations import OutwardLocationName

if TYPE_CHECKING:
    from typing import Any, TextIO

def get_archipelago_version() -> str:
    return str(archipelago_version).strip()

def get_game() -> str:
    return str(OutwardWorld.game)

def get_items():
    name_to_key: dict[str, str] = dict()
    for key, name in vars(OutwardItemName).items():
        if not key.startswith('_') and isinstance(name, str):
            name_to_key[name] = key

    items: list[dict[str, Any]] = []
    for name, code in OutwardWorld.item_name_to_id.items():
        key = name_to_key[name]
        items.append({
            "name": name,
            "id": code,
            "key": key,
        })

    return items

def get_locations():
    name_to_key: dict[str, str] = dict()
    for key, name in vars(OutwardLocationName).items():
        if not key.startswith('_') and isinstance(name, str):
            name_to_key[name] = key

    locations: list[dict[str, Any]] = []
    for name, code in OutwardWorld.location_name_to_id.items():
        key = name_to_key[name]
        locations.append({
            "name": name,
            "id": code,
            "key": key,
        })

    return locations

def get_apworld_info():
    return {
        "archipelago_version": get_archipelago_version(),
        "game": get_game(),
        "items": get_items(),
        "locations": get_locations(),
    }

def export(fp: TextIO) -> None:
    """
    Exports all the public ids for items and locations used in the APWorld along
    with the code identifiers (variable names).
    """

    apworld_info = get_apworld_info()
    return json.dump(apworld_info, fp)

if __name__ == "__main__":
    import argparse
    from contextlib import contextmanager
    import sys

    if TYPE_CHECKING:
        from collections.abc import Iterator

    @contextmanager
    def _open(file: str) -> Iterator[TextIO]:
        fp = sys.stdout if file == "-" else open(file, "w")
        try:
            yield fp
        finally:
            if fp is not None and fp is not sys.stdout:
                fp.close()
            fp = None

    parser = argparse.ArgumentParser(description="Export the public AP world bindings.")
    parser.add_argument("-o", "--output", type=str, required=False, default="-", help="The path to the file to write the json output")
    
    args = parser.parse_args()

    with _open(args.output) as fp:
        export(fp)
