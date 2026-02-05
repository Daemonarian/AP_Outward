"""
This script provides utilities for more easily writing other files by filling in variables
that appear as {{VERSION}}.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

from .export import get_apworld_info

if TYPE_CHECKING:
    from typing import TextIO

def apply_template(template: TextIO, output: TextIO):
    apworld = get_apworld_info()
    for line in template:
        line = line.replace("{{VERSION}}", apworld.version)
        line = line.replace("{{ARCHIPELAGO_VERSION}}", apworld.archipelago_version)
        line = line.replace("{{GAME}}", apworld.game)
        output.write(line)

if __name__ == '__main__':
    import argparse

    from .utils import open_special

    parser = argparse.ArgumentParser(description="Export the public AP world bindings.")
    parser.add_argument('-o', '--output', type=str, required=False, default='-', help="The path to write the results of the template.")
    parser.add_argument('-i', '--input', type=str, required=False, default='-', help="The path to the template file to apply.")
    
    args = parser.parse_args()

    with open_special(args.output, 'w') as out_fp:
        with open_special(args.input, 'r') as in_fp:
            apply_template(in_fp, out_fp)
