#!python3.13

import sys

python_version = (3, 13)
if (sys.version_info.major, sys.version_info.minor) != python_version:
    raise ValueError("requires Python {0}".format(".".join(python_version)))

from pathlib import Path
import subprocess

here = Path(__file__).parent

subprocess.run([sys.executable, here / "APWorld" / "setup.py"])
