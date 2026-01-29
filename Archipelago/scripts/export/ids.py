"""
Exports all the public ids for items in locations used in the apworld along with
the code identifiers (variable names).
"""

import json
from typing import TextIO

from outward import OutwardWorld
from outward.items import OutwardItemName
from outward.locations import OutwardLocationName

def export_ids(fp: TextIO) -> None:
    """
    Exports all the public ids for items and locations used in the APWorld along
    with the code identifiers (variable names).
    """

    item_ids: dict[str, int] = dict()
    for key, name in vars(OutwardItemName).items():
        if key.startswith("_") or not isinstance(name, str) or name not in OutwardWorld.item_name_to_id:
            continue
        code = OutwardWorld.item_name_to_id[name]
        item_ids[key] = code
    
    location_ids: dict[str, int] = dict()
    for key, name in vars(OutwardLocationName).items():
        if key.startswith("_") or not isinstance(name, str) or name not in OutwardWorld.location_name_to_id:
            continue
        code = OutwardWorld.location_name_to_id[name]
        location_ids[key] = code

    all_ids = {"items": item_ids, "locations": location_ids}

    return json.dump(all_ids, fp)

if __name__ == "__main__":
    import argparse
    from .utils import _open

    parser = argparse.ArgumentParser(description="Export the public IDs in this APWorld along with the internal code identifiers used for them.")
    parser.add_argument("-o", "--output", type=str, required=False, default="-", help="The path to the file to write the json output")
    
    args = parser.parse_args()

    with _open(args.output) as fp:
        export_ids(fp)
