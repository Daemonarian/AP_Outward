"""
Exports the Archipelago version.
"""

from typing import TextIO

def export_archipelago_id(fp: TextIO):
    from Utils import __version__ as version
    fp.write(version)

if __name__ == "__main__":
    import argparse
    from .utils import _open

    parser = argparse.ArgumentParser(description="Export the version of Archipelago we are building against.")
    parser.add_argument("-o", "--output", type=str, required=False, default="-", help="The path to the file to write the json output")
    
    args = parser.parse_args()

    with _open(args.output) as fp:
        export_archipelago_id(fp)
