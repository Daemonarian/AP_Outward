"""
This module exports all of this AP world's public bindings.
"""

from __future__ import annotations
from dataclasses import dataclass
import dataclasses
from typing import TYPE_CHECKING

import json
from pathlib import Path

from Utils import __version__ as archipelago_version

from worlds.outward import OutwardWorld
from worlds.outward.items import OutwardItemName
from worlds.outward.locations import OutwardLocationName

if TYPE_CHECKING:
    from typing import TextIO

@dataclass
class APWorldItemInfo:
    id: int
    name: str
    key: str

@dataclass
class APWorldLocationInfo:
    id: int
    name: str
    key: str

@dataclass
class APWorldInfo:
    version: str
    archipelago_version: str
    game: str
    items: list[APWorldItemInfo]
    locations: list[APWorldLocationInfo]

def get_apworld_version() -> str:
     version_file = Path(__file__).parent.parent.parent / 'version.json'
     with open(version_file, 'r') as f:
         data = json.load(f)
     return data['version'].strip()

def get_archipelago_version() -> str:
    return str(archipelago_version).strip()

def get_apworld_game() -> str:
    return str(OutwardWorld.game)

def get_apworld_items() -> list[APWorldItemInfo]:
    name_to_key: dict[str, str] = dict()
    for key, name in vars(OutwardItemName).items():
        if not key.startswith('_') and isinstance(name, str):
            name_to_key[name] = key

    items: list[APWorldItemInfo] = []
    for name, code in OutwardWorld.item_name_to_id.items():
        key = name_to_key[name]
        items.append(APWorldItemInfo(
            id=code,
            name=name,
            key=key))

    return items

def get_apworld_locations() -> list[APWorldLocationInfo]:
    name_to_key: dict[str, str] = dict()
    for key, name in vars(OutwardLocationName).items():
        if not key.startswith('_') and isinstance(name, str):
            name_to_key[name] = key

    locations: list[APWorldLocationInfo] = []
    for name, code in OutwardWorld.location_name_to_id.items():
        key = name_to_key[name]
        locations.append(APWorldLocationInfo(
            id=code,
            name=name,
            key=key))

    return locations

def get_apworld_info() -> APWorldInfo:
    return APWorldInfo(
        version=get_apworld_version(),
        archipelago_version=get_archipelago_version(),
        game=get_apworld_game(),
        items=get_apworld_items(),
        locations=get_apworld_locations())

def export(fp: TextIO) -> None:
    """
    Exports all the public ids for items and locations used in the APWorld along
    with the code identifiers (variable names).
    """

    apworld_info = get_apworld_info()
    apworld_info_dict = dataclasses.asdict(apworld_info)
    return json.dump(apworld_info_dict, fp)

if __name__ == "__main__":
    import argparse

    from .utils import open_special

    parser = argparse.ArgumentParser(description="Export the public AP world bindings.")
    parser.add_argument("-o", "--output", type=str, required=False, default="-", help="The path to the file to write the json output")
    
    args = parser.parse_args()

    with open_special(args.output) as fp:
        export(fp)
