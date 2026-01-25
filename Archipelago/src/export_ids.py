"""
Exports all the public ids for items in locations used in the apworld along with
the code identifiers used.
"""

import json
import sys
from pathlib import Path

archipelago_path = str(Path(__file__).parent / ".." / ".." / "External" / "Archipelago")
if archipelago_path not in sys.path:
    sys.path.insert(0, archipelago_path)

from outward import OutwardWorld
from outward.item_data import ItemName
from outward.location_data import LocationName

def export_ids(output_path):
    item_ids = dict()
    for key, name in vars(ItemName).items():
        if key.startswith("_") or not isinstance(name, str) or name not in OutwardWorld.item_name_to_id:
            continue
        code = OutwardWorld.item_name_to_id[name]
        item_ids[key] = code
    
    location_ids = dict()
    for key, name in vars(LocationName).items():
        if key.startswith("_") or not isinstance(name, str) or name not in OutwardWorld.location_name_to_id:
            continue
        code = OutwardWorld.location_name_to_id[name]
        location_ids[key] = code

    all_ids = {"items": item_ids, "locations": location_ids}

    with open(output_path, "w") as f:
        json.dump(all_ids, f)

if __name__ == "__main__":
    import argparse

    parser = argparse.ArgumentParser(description="Export the public IDs in this APWorld along with the internal code identifiers used for them.")
    parser.add_argument("-o", "--output", type=Path, required=True, help="The path to the file to write the json output")
    
    args = parser.parse_args()

    export_ids(args.output)
